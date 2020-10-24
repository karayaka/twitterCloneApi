using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using twiterClone.DAL.Classes.PostClasses;
using twiterClone.DAL.Enums;
using twitterClone.Entity.Interfaces;
using twitterClone.WepApi.Models.AppModel;
using twitterClone.WepApi.Models.PostModels;

namespace twitterClone.WepApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IUnitOfWork work;

        private int UserID;

        public PostController(IUnitOfWork _work, IHttpContextAccessor httpContextAccessor)
        {
            work = _work;
            UserID = Convert.ToInt32(httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
        }
        [Authorize]
        [HttpGet("getallposts/{PageID?}")]
        public async Task<IActionResult> GetAllPosts(int PageID)
        {
            //Sayfalama işlemleri ve sayfalama stadartları üzerine düşünülecek
            IQueryable<Post> posts;
            if (PageID == 0)
                posts = work.BaseRepositoriys.GetNonDeleted<Post>(t => t.Status == Status.Active);
            else
                posts = work.PostRepository.GetPostsPagination(PageID, 2);
            return Ok(posts);
        }
        [Authorize]
        [HttpGet("getqueryposts/{PageID?}")]
        public async Task<IActionResult> GetQueryPosts(int PageID)
        {
            
            IQueryable<Post> posts;
            if (PageID == 0)
                posts = work.BaseRepositoriys.GetNonDeleted<Post>(t => t.Status == Status.Active);
            else
                posts = work.PostRepository.GetPostsPagination(PageID, 2);
            return Ok(posts);
        }
        [Authorize]
        [HttpPost("createpost"),DisableRequestSizeLimit]
        public async Task<IActionResult> CreatePost([FromForm]PostCreateModel model)
        {
            try
            {
                var post = new Post()
                {
                    LastUpdateBy = UserID,
                    UserID = UserID,
                    PostImage = work.BaseRepositoriys.SaveFile(model.postFiles,"PostIMage"),
                    PostContent = model.PostContent,
                    PostTitle = model.PostTitle,
                    CreatedBy = UserID,
                };
                var retVal = await work.BaseRepositoriys.Add<Post>(post);
                var resuld = new ResuldModel();

                if (retVal != 0)
                {
                    resuld.ResuldStatus = ResuldStatus.Succes;
                    resuld.Title = "Kayıt Başarılı";
                    resuld.Content = "Kayıt Başarılı";
                }
                else
                {
                    resuld.ResuldStatus = ResuldStatus.Erorr;
                    resuld.Title = "Hata";
                    resuld.Content = "Kayıt Sırasında Hata Oluştu";
                }
                return Ok(resuld);
            }
            catch (Exception)
            {

                return BadRequest();
            }

           
            
        }
        [Authorize]
        [HttpPost("updatepost")]
        public async Task<IActionResult> UpdatePost(PostCreateModel model)
        {

            var post = await work.BaseRepositoriys.GetByID<Post>(model.ID);
            var resuld = new ResuldModel();

            post.PostTitle = model.PostTitle;
            post.PostContent = model.PostContent;
            post.PostImage = model.PostImage;
            post.LastUpdateBy = UserID;
            var retVal= work.PostRepository.Update(post);

            if (retVal != 0)
            {
                resuld.ResuldStatus = ResuldStatus.Succes;
                resuld.Title = "Kayıt Başarılı";
                resuld.Content = "Kayıt Başarılı";
            }
            else
            {
                resuld.ResuldStatus = ResuldStatus.Erorr;
                resuld.Title = "Hata";
                resuld.Content = "Kayıt Sırasında Hata Oluştu";
            }
            return Ok(resuld);
        }
        [Authorize]
        [HttpDelete("deletepost/{ID}")]
        public async Task<IActionResult> DeletePost(int ID)
        {
            var resuld = new ResuldModel();
            try
            {
                work.PostRepository.Delete<Post>(ID);
                resuld.ResuldStatus = ResuldStatus.Succes;
                resuld.Title = "İşlem Başarılı";
                resuld.Content = "İşlem Başarılı";
            }
            catch (Exception)
            {
                resuld.ResuldStatus = ResuldStatus.Erorr;
                resuld.Title = "İşlem Başarısız";
                resuld.Content = "İşlem Başarısız";
            }
            return Ok(resuld);
        }
        

    }
}
