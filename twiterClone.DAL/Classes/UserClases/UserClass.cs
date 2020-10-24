using System;
using System.Collections.Generic;
using System.Text;
using twiterClone.DAL.Classes.BaseClases;
using twiterClone.DAL.Classes.PostClasses;

namespace twiterClone.DAL.Classes.UserClases
{
    public class UserClass:BaseObject
    {
        public string UserName { get; set; }

        public string UserImage { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string EmailConfirmeKey { get; set; }

        public ICollection<LikeDislike> LikeDislikes { get; set; }

        public ICollection<Comment> Comments { get; set; }

        public ICollection<Post> Posts { get; set; }

        public ICollection<Follower> Followers { get; set; }
    }
}
