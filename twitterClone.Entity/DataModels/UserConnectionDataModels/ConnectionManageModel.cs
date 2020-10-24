using System;
using System.Collections.Generic;
using System.Text;

namespace twitterClone.Entity.DataModels.UserConnectionDataModels
{
    public class ConnectionManageModel
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string SurName { get; set; }

        public string Email { get; set; }

        public string ConnectionID { get; set; }
    }
}
