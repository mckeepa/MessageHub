using System;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;


namespace coreWeb.Models
{
    public class NewSystemContext : DbContext
    {

         public NewSystemContext(DbContextOptions<NewSystemContext> options) : base(options)
        {
        }

        public DbSet<Person> Persons { get; set; }
        public DbSet<Alert> Alerts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("Data Source=newSystem.db");
    }
    public class Blog
    {
        public int BlogId { get; set; }
        public string Url { get; set; }

        public List<Post> Posts { get; } = new List<Post>();
    }

    public class Post
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public int BlogId { get; set; }
        public Blog Blog { get; set; }
    }

}
