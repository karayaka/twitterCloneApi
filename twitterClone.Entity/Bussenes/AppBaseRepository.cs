using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using twiterClone.DAL.Classes.BaseClases;
using twiterClone.DAL.DataContext;
using twiterClone.DAL.Enums;
using twitterClone.Entity.Interfaces;

namespace twitterClone.Entity.Bussenes
{
    public class AppBaseRepository : IAppBaseRepositoriys
    {
        private readonly CloneDataContext context;

        private int UserID;

        public AppBaseRepository(CloneDataContext _context,IHttpContextAccessor httpContextAccessor)
        {
            context = _context;
            var val = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            if(val!=null)
                UserID = Convert.ToInt32(val.Value);
        }

        public async Task<int> Add<T>(T Entitiy) where T : BaseObject
        {
            try
            {
                Entitiy.CreatedDate = DateTime.Now;
                Entitiy.CreatedBy = UserID;
                Entitiy.LastUpdateBy = UserID;
                Entitiy.LastUpdateDate = DateTime.Now;
                Entitiy.ObjectStatus = ObjectStatus.NonDeleted;
                Entitiy.Status = Status.Active;
                await context.AddAsync(Entitiy);
                return await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            //Takipçi Kayıt Problemli
            
        }

        public void Delete<T>(int ID) where T : BaseObject
        {
            try
            {
                var model = context.Set<T>().FirstOrDefault(t => t.ID == ID);
                model.LastUpdateDate = DateTime.Now;
                model.ObjectStatus = ObjectStatus.Deleted;
                model.LastUpdateBy = UserID;
                model.Status = Status.Pasive;
                context.Update(model);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        public IQueryable<T> Get<T>(Expression<Func<T, bool>> expression) where T : BaseObject
        {
            return context.Set<T>().Where(expression);
        }

        public DbSet<T> GetAllObject<T>() where T : BaseObject
        {
            return context.Set<T>();
        }

        public async Task<T> GetByID<T>(int ID) where T : BaseObject
        {
            return await context.Set<T>().FirstOrDefaultAsync(t => t.ID == ID);
        }

        public IQueryable<T> GetNonDeleted<T>(Expression<Func<T, bool>> expression) where T : BaseObject
        {
            IQueryable<T> models;
            if (expression != null)
            {
                models = context.Set<T>().Where(expression);
            }
            else
            {
                models = context.Set<T>();
            }
            return models.Where(t => t.ObjectStatus == ObjectStatus.NonDeleted);
        }

        public IQueryable<T> GetNonDeletedAndActive<T>(Expression<Func<T, bool>> expression) where T : BaseObject
        {
            IQueryable<T> models;
            if (expression != null)
            {
                models = context.Set<T>().Where(expression);
            }
            else
            {
                models = context.Set<T>();
            }
            return models.Where(t => t.ObjectStatus == ObjectStatus.NonDeleted && t.Status == Status.Active);
        }

        public int Update<T>(T Entitiy) where T : BaseObject
        {
            Entitiy.LastUpdateBy = UserID;
            Entitiy.LastUpdateDate = DateTime.Now;
            Entitiy.ObjectStatus = ObjectStatus.NonDeleted;
            Entitiy.Status = Status.Active;
            context.Update(Entitiy);
            return context.SaveChanges();
        }
        /// <summary>
        /// projectDirectory: Projenin içinde bulunduğu klaosörü buluyor
        /// folderName: Resmin Kayıt Olacağı Dosyı Gösteriyor
        /// </summary>
        public string SaveFile(List<IFormFile> files, string folderName)
        {
            string fileName = null;
            try
            {
                var file = files[0];
                var path = Path.Combine("Resource", folderName);
           
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), path);//TEstamaçlıYAzıldı
                if (file.Length > 0)
                {
                    var fileKey = Guid.NewGuid();
                    var ex = Path.GetExtension(file.FileName);
                    fileName = folderName + "-" + fileKey+ex;
                    var fullPath = Path.Combine(pathToSave, fileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                }
                return fileName;
            }
            catch (Exception ex)
            {
                return fileName;
            }
        }

        public IQueryable<T> GetNonDeletedAndPaginate<T>(int pageID, int PageSize) where T : BaseObject
        {
            pageID--;
            return context.Set<T>().OrderBy(t => t.ID).Skip(pageID * PageSize).Take(PageSize);
        }
    }
}
