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
    public class CommentRepositoriys:AppBaseRepository,ICommentRepositoriys
    {
        private readonly CloneDataContext contex;
  
        public CommentRepositoriys(CloneDataContext _contex,IHttpContextAccessor httpContextAccessor) :base(_contex, httpContextAccessor)
        {
            contex = _contex;
        }

        public IQueryable<Comment> GetCommentByUserID(int UserID)
        {
            return contex.Comments.Where(t => t.ObjectStatus == ObjectStatus.NonDeleted && t.UserID==UserID).OrderByDescending(t=>t.LastUpdateBy);
        }

        public IQueryable<Comment> GetCommentPagination(int pageID, int PageSize)
        {
            pageID--;
            return contex.Comments.Where(t=>t.ObjectStatus==ObjectStatus.NonDeleted)
                .OrderByDescending(t => t.LastUpdateBy).Skip(pageID * PageSize).Take(PageSize);
        }

        public IQueryable<Comment> GetCommentQuery(string q, int PageID, int PageSize)
        {
            PageID--;
            return contex.Comments
                .Where(t => t.ObjectStatus == ObjectStatus.NonDeleted &&
                t.CommentTitle == q || t.CommentText == q).Skip(PageID * PageSize).Take(PageSize);
        }
    }
}
