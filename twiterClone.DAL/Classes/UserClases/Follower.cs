using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using twiterClone.DAL.Classes.BaseClases;

namespace twiterClone.DAL.Classes.UserClases
{
    public class Follower:BaseObject
    {
        public int UserID { get; set; }
        [NotMapped]
        public virtual UserClass User { get; set; }

        public int FollowedID { get; set; }
        public virtual UserClass Followed { get; set; }

        public bool GetMessage { get; set; }

        public bool GetNofication { get; set; }

    }
}
