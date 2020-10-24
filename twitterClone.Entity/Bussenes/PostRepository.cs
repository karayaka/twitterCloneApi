using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using twiterClone.DAL.Classes.PostClasses;
using twiterClone.DAL.DataContext;
using twiterClone.DAL.Enums;
using twitterClone.Entity.Interfaces;

namespace twitterClone.Entity.Bussenes
{
    public class PostRepository : AppBaseRepository, IPostRepository
    {
        private readonly CloneDataContext contex;
        public PostRepository(CloneDataContext _contex, IHttpContextAccessor httpContextAccessor) :base(_contex, httpContextAccessor)
        {
            contex = _contex;
        }

        public IQueryable<Post> GetUserPosts(int UserID)
        {
            return contex.Posts.Where(t => t.ObjectStatus == ObjectStatus.NonDeleted && t.Status == Status.Active);
        }
        public IQueryable<Post> GetPostsPagination(int pageID,int PageSize)
        {
            pageID--;
            return contex.Posts.OrderBy(t=>t.ID).Skip(pageID * PageSize).Take(PageSize);
        }
        public IQueryable<Post> GetPostsQuery(string q, int PageID, int PageSize)
        {
            PageID--;
            return contex.Posts
                .Where(t => t.ObjectStatus == ObjectStatus.NonDeleted &&
                t.PostTitle == q || t.PostContent == q).Skip(PageID*PageSize).Take(PageSize);
        }
    }
}
