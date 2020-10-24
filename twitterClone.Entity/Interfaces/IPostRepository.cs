using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using twiterClone.DAL.Classes.PostClasses;

namespace twitterClone.Entity.Interfaces
{
    public interface IPostRepository:IAppBaseRepositoriys
    {
        public IQueryable<Post> GetUserPosts(int UserID);

        public IQueryable<Post> GetPostsPagination(int pageID, int PageSize);

        public IQueryable<Post> GetPostsQuery(string q,int PageID,int PageSize);

        
    }
}
