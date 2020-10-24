using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using twiterClone.DAL.Classes.PostClasses;

namespace twitterClone.Entity.Interfaces
{
    public interface ICommentRepositoriys: IAppBaseRepositoriys
    {
        public IQueryable<Comment> GetCommentPagination(int pageID, int PageSize);

        public IQueryable<Comment> GetCommentQuery(string q, int PageID, int PageSize);

        public IQueryable<Comment> GetCommentByUserID( int UserID);
    }
}
