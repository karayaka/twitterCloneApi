using System;
using System.Collections.Generic;
using System.Text;
using twiterClone.DAL.Classes.BaseClases;
using twiterClone.DAL.Classes.UserClases;
using twiterClone.DAL.Enums;

namespace twiterClone.DAL.Classes.PostClasses
{
    public class LikeDislike:BaseObject
    {
        public int UserID { get; set; }
        public virtual UserClass User { get; set; }

        public int PostID { get; set; }
        public virtual Post Post { get; set; }

        public LikeDislikeType LikeDislikeType { get; set; }
    }
}
