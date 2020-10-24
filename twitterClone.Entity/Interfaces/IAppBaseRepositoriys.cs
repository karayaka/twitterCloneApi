using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Text;
using twiterClone.DAL.Classes.BaseClases;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace twitterClone.Entity.Interfaces
{
    public interface IAppBaseRepositoriys
    {
        Task<int> Add<T>(T Entitiy) where T : BaseObject;

        int Update<T>(T Entitiy) where T : BaseObject;

        void Delete<T>(int ID) where T : BaseObject;

        IQueryable<T> Get<T>(Expression<Func<T, bool>> expression) where T : BaseObject;

        IQueryable<T> GetNonDeleted<T>(Expression<Func<T, bool>> expression) where T : BaseObject;

        IQueryable<T> GetNonDeletedAndActive<T>(Expression<Func<T, bool>> expression) where T : BaseObject;

        IQueryable<T> GetNonDeletedAndPaginate<T>(int pageID, int PageSize) where T : BaseObject;

        Task<T> GetByID<T>(int ID) where T : BaseObject;

        DbSet<T> GetAllObject<T>() where T : BaseObject;

        string SaveFile(List<IFormFile> files, string folderName);

    }
}
