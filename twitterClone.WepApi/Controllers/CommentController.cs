using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using twiterClone.DAL.Classes.PostClasses;
using twiterClone.DAL.Enums;
using twitterClone.Entity.Interfaces;

namespace twitterClone.WepApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly IUnitOfWork work;

        private int UserID;

        public CommentController(IUnitOfWork _work, IHttpContextAccessor httpContextAccessor)
        {
            work = _work;
            UserID = Convert.ToInt32(httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
        }

        [Authorize]
        [HttpPost("createcomment"), DisableRequestSizeLimit]
        public async Task<IActionResult> CreateComment(Comment comment)
        {
            var retVal = await work.BaseRepositoriys.Add(comment);
            if (retVal == 0)
                return BadRequest();
            return Ok();
        }

        [Authorize]
        [HttpDelete("deletecomment/{ID}")]
        public async Task<IActionResult> DeleteComment(int ID)
        {
            try
            {
                work.BaseRepositoriys.Delete<Comment>(ID);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
            
        }

        [Authorize]
        [HttpPost("like/{ID?}")]
        public async Task<IActionResult> Like(int ID)
        {
            var retVal= await work.LikeAndDisLikeRepository.TogleLike(ID, UserID);
            
            if (retVal == 0)
                return BadRequest();
            return Ok();
        }
        [Authorize]
        [HttpPost("dislike/{ID?}")]
        public async Task<IActionResult> DisLike(int ID)
        {
            var retVal = await work.LikeAndDisLikeRepository.TogleDislike(ID, UserID);

            if (retVal == 0)
                return BadRequest();
            return Ok();
        }

    }
}
