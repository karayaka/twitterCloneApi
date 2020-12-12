using System;
using System.Collections.Generic;
using System.Text;

namespace twitterClone.Entity.DataModels.WallModels
{
    public class WallListDataModel
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public string Context { get; set; }

        public string UserImage { get; set; }

        public string UserName { get; set; }

        public string UserSurName { get; set; }

        public string PostImage { get; set; }

        public int CommnetCount { get; set; }

        public int LikeCount { get; set; }

        public int DisLikeCount { get; set; }
    }
}
