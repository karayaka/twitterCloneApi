using System;
using System.Collections.Generic;
using System.Text;
using twiterClone.DAL.Classes.BaseClases;
using twiterClone.DAL.Classes.UserClases;

namespace twiterClone.DAL.Classes.PostClasses
{
    public class Post:BaseObject
    {
        public int UserID { get; set; }
        public virtual UserClass User { get; set; }

        public string PostTitle { get; set; }

        public string PostContent { get; set; }

        public string PostImage { get; set; }

        public ICollection<LikeDislike> LikeDislikes { get; set; }

        public ICollection<Comment> Comments { get; set; }

    }
}
