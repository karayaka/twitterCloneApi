using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace twitterClone.WepApi.Models.WallModels
{
    public class WallListModel
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public string Context { get; set; }

        public string UserImage { get; set; }

        public string PostImage { get; set; }

        public int CommnetCount { get; set; }

        public int LikeCount { get; set; }

        public int DisLikeCount { get; set; }

    }
}
