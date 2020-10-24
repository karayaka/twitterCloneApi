using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using twiterClone.DAL.Classes.PostClasses;
using twiterClone.DAL.Classes.UserClases;

namespace twiterClone.DAL.DataContext
{
    public class CloneDataContext:DbContext
    {
        public CloneDataContext(DbContextOptions<CloneDataContext> options):base(options)
        {

        }

        public DbSet<Post> Posts { get; set; }

        public DbSet<UserClass> Users { get; set; }

        public DbSet<Follower> Followers { get; set; }

        public DbSet<LikeDislike> LikeDislikes { get; set; }

        public DbSet<Comment> Comments { get; set; }



    }
}
