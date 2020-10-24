using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using twiterClone.DAL.Classes.PostClasses;
using twiterClone.DAL.Classes.UserClases;
using twiterClone.DAL.Enums;
using twitterClone.Entity.Interfaces;
using twitterClone.WepApi.Models.WallModels;

namespace twitterClone.WepApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WallController : ControllerBase
    {
        private readonly IUnitOfWork work;

        private int UserID;

        public WallController(IUnitOfWork _work, IHttpContextAccessor httpContextAccessor)
        {
            work = _work;
            UserID = Convert.ToInt32(httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
        }

        [Authorize]
        [HttpGet("getwall/{PageID?}")]
        public async Task<IActionResult> GetWall(int PageID)
        {

            var a = work.WallRepository.GetWall(PageID);

            return Ok(a);
        }
        [Authorize]
        [HttpPost("fallow/{FollowerID?}")]
        public async Task<IActionResult> Fallow(int FollowerID)
        {//benım takip ettiklerim!!
            var follower = new Follower()
            {
                FollowedID = FollowerID,
                GetMessage = false,
                GetNofication = false,
                UserID=UserID
            };
            var retVal = work.BaseRepositoriys.Add<Follower>(follower);

            return Ok();
        }
        [Authorize]
        [HttpPost("unfallow/{FollowerID?}")]
        public async Task<IActionResult> UnFallow(int FollowerID)
        {
          
            work.BaseRepositoriys.Delete<Follower>(FollowerID);

            return Ok();
        }

        [Authorize]
        [HttpGet("getpostdetail/{ID}")]
        public async Task<IActionResult> PostDetail(int ID)
        {

            var postDetail = await  work.WallRepository.PostDetail(ID);

            return Ok(postDetail);
        }
    }
}
