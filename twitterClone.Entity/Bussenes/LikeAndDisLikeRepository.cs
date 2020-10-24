using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using twiterClone.DAL.Classes.PostClasses;
using twiterClone.DAL.DataContext;
using twiterClone.DAL.Enums;
using twitterClone.Entity.Interfaces;

namespace twitterClone.Entity.Bussenes
{
    public class LikeAndDisLikeRepository : AppBaseRepository, ILikeAndDisLikeRepository
    {
        private readonly CloneDataContext contex;
        public LikeAndDisLikeRepository(CloneDataContext _contex, IHttpContextAccessor httpContextAccessor) :base(_contex, httpContextAccessor)
        {
            contex = _contex;
        }

        public async Task<int> TogleDislike(int PostID, int UserID)
        {
            try
            {
                var retVal = await contex.LikeDislikes.FirstOrDefaultAsync(t => t.PostID == PostID && t.UserID == UserID);

                if (retVal != null)
                {
                    Delete<LikeDislike>(retVal.ID);
                }
                else
                {
                    var like = new LikeDislike()
                    {
                        CreatedBy = UserID,
                        UserID = UserID,
                        CreatedDate = DateTime.Now,
                        LastUpdateBy = UserID,
                        LastUpdateDate = DateTime.Now,
                        LikeDislikeType = LikeDislikeType.Dislike,
                        ObjectStatus = ObjectStatus.NonDeleted,
                        PostID = PostID,
                        Status = Status.Active,

                    };
                }
                return 1;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public async Task<int> TogleLike(int PostID, int UserID)
        {
            try
            {
                var retVal = await contex.LikeDislikes.FirstOrDefaultAsync(t => t.PostID == PostID && t.UserID == UserID);

                if (retVal != null)
                {
                    Delete<LikeDislike>(retVal.ID);
                }
                else
                {
                    var like = new LikeDislike() 
                    {
                        CreatedBy=UserID,
                        UserID=UserID,
                        CreatedDate=DateTime.Now,
                        LastUpdateBy=UserID,
                        LastUpdateDate=DateTime.Now,
                        LikeDislikeType=LikeDislikeType.Like,
                        ObjectStatus=ObjectStatus.NonDeleted,
                        PostID=PostID,
                        Status=Status.Active,
                    };
                }
                return 1;
            }
            catch (Exception)
            {
                return -1;
            }
        }
    }
}
