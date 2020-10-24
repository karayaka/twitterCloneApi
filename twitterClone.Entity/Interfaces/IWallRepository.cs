using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using twitterClone.Entity.DataModels.WallModels;

namespace twitterClone.Entity.Interfaces
{

    public interface IWallRepository:IAppBaseRepositoriys
    {
        IQueryable<WallListDataModel> GetWall(int PageID);

        Task<WallDetailModel> PostDetail(int ID);
    }
}
