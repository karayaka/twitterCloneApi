using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace twitterClone.Entity.Interfaces
{
    public interface ILikeAndDisLikeRepository:IAppBaseRepositoriys
    {
        Task<int> TogleLike(int PostID, int UserID);

        Task<int> TogleDislike(int PostID, int UserID);
    }
}
