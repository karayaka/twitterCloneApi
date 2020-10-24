using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using twiterClone.DAL.Classes.PostClasses;

namespace twitterClone.Entity.DataModels.WallModels
{
    public class WallDetailModel
    {
        public int ID { get; set; }

        public string PostTitle { get; set; }

        public string PostContent { get; set; }

        public string PostImage { get; set; }

        public string UserImage { get; set; }

        public int LikeCount { get; set; }

        public int DisLikeCount { get; set; }

        public int CommentCount { get; set; }

        public IEnumerable<Comment> Comments { get; set; }
    }
}
