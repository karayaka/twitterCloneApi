using System;
using System.Collections.Generic;
using System.Text;
using twitterClone.Entity.DataModels.UserConnectionDataModels;

namespace twitterClone.Entity.Interfaces
{
    public interface IUserConnectionRepository
    {
        ConnectionManageModel GetConnectedUsers(int UserID);

        List<ConnectionManageModel> GetAllConnectedUsers(int UserID);

        List<ConnectionManageModel> RemoveConnectedUsers(string ConnectionID);

        List<ConnectionManageModel> AddConnectedUsers(ConnectionManageModel model);
    }
}
