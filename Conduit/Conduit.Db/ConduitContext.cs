using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conduit.Db
{
    public  class ConduitContext: DbContext
    {
        public ConduitContext(DbContextOptions<ConduitContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Follower>().HasOne<User>().WithMany().HasForeignKey(p=>p.UserId);
            modelBuilder.Entity<Article>().HasOne<User>().WithMany().HasForeignKey(p=>p.UserId);
            modelBuilder.Entity<Comment>().HasOne<User>().WithMany().HasForeignKey(p=>p.UserId);
            modelBuilder.Entity<Comment>().HasOne<Article>().WithMany().HasForeignKey(p=>p.ArticleId);
            modelBuilder.Entity<Favourite>().HasOne<Article>().WithMany().HasForeignKey(p=>p.ArticleId);
            modelBuilder.Entity<Favourite>().HasOne<User>().WithMany().HasForeignKey(p=>p.UserId);
        }

        public DbSet<Article> Articles { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Favourite> Favourites { get; set; }
        public DbSet<Follower> Followers { get; set; }
        public DbSet<User> Users { get; set; }
      
    }
}
