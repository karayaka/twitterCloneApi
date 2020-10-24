using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using twitterClone.Entity.DataModels.UserConnectionDataModels;
using twitterClone.Entity.Interfaces;

namespace twitterClone.Entity.Bussenes
{
    public class UserConnectionRepository : IUserConnectionRepository
    {
        private List<ConnectionManageModel> ConnectedUsers = new List<ConnectionManageModel>();

        public List<ConnectionManageModel> AddConnectedUsers(ConnectionManageModel model)
        {
            ConnectedUsers.Add(model);
            return ConnectedUsers;
        }

        public List<ConnectionManageModel> GetAllConnectedUsers(int UserID)
        {
            return ConnectedUsers;
        }

        public ConnectionManageModel GetConnectedUsers(int UserID)
        {
            return ConnectedUsers.FirstOrDefault(t => t.ID == UserID);
        }

        public List<ConnectionManageModel> RemoveConnectedUsers(string ConnectionID)
        {
            var user = ConnectedUsers.FirstOrDefault(t => t.ConnectionID == ConnectionID);
            ConnectedUsers.Remove(user);
            return ConnectedUsers;
        }
    }
}
