using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using twiterClone.DAL.Classes.PostClasses;
using twiterClone.DAL.Classes.UserClases;
using twiterClone.DAL.DataContext;
using twiterClone.DAL.Enums;
using twitterClone.Entity.DataModels.WallModels;
using twitterClone.Entity.Interfaces;

namespace twitterClone.Entity.Bussenes
{
    public class WallRepository:AppBaseRepository,IWallRepository
    {
        private readonly CloneDataContext context;
        int UserID;
        public WallRepository(CloneDataContext _context, IHttpContextAccessor httpContextAccessor):base(_context,httpContextAccessor)
        {
            context = _context;
            var val = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            if (val != null)
                UserID = Convert.ToInt32(val.Value);
        }

        public IQueryable<WallListDataModel> GetWall(int PageID)
        {
            //var fallowers = GetNonDeletedAndActive<Follower>(t=>t.UserID==UserID);
            IQueryable<Post> posts;
            if (PageID == 0)
                posts = GetNonDeleted<Post>(t => t.Status == Status.Active);
            else
                posts = GetNonDeletedAndPaginate<Post>(PageID, 2);

            IQueryable<WallListDataModel> wallPosts = posts.Where(t=> t.User.Followers.Any(f=>f.UserID==t.UserID)).Select(s => new WallListDataModel
            {
                CommnetCount = s.Comments.Count(t => t.ObjectStatus == ObjectStatus.NonDeleted),
                Context = s.PostContent,
                DisLikeCount = s.LikeDislikes.Count(t => t.ObjectStatus == ObjectStatus.NonDeleted && t.LikeDislikeType == LikeDislikeType.Dislike),
                LikeCount = s.LikeDislikes.Count(t => t.ObjectStatus == ObjectStatus.NonDeleted && t.LikeDislikeType == LikeDislikeType.Like),
                UserImage = s.User.UserImage,
                PostImage = s.PostImage,
                ID = s.ID,
                Title = s.PostTitle,
            });
            return wallPosts;
        }

        public async Task<WallDetailModel> PostDetail(int ID)
        {
            var post = await context.Posts
                .Include(c=>c.Comments)
                .Include(l=>l.LikeDislikes)
                .Include(l=>l.User)
                .FirstOrDefaultAsync(t => t.ID == ID);
            var model = new WallDetailModel();


            model.ID = post.ID;
            model.Comments = post.Comments.Where(t => t.ObjectStatus == ObjectStatus.NonDeleted);
            model.DisLikeCount = post.LikeDislikes.Count(t => t.ObjectStatus == ObjectStatus.NonDeleted && t.LikeDislikeType == LikeDislikeType.Dislike);
            model.LikeCount = post.LikeDislikes.Count(t => t.ObjectStatus == ObjectStatus.NonDeleted && t.LikeDislikeType == LikeDislikeType.Like);
            model.PostContent = post.PostContent;
            model.PostImage = post.PostImage;
            model.PostTitle = post.PostTitle;
            model.UserImage = post.User.UserImage;

            model.CommentCount = model.Comments.Count();

            return model;
        }
    }
}
