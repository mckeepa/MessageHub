using System;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace coreWeb.Models
{
    public class NewSystemContext : DbContext
    {
        public NewSystemContext(DbContextOptions<NewSystemContext> options) : base(options)
        {
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            DetectChanges();
            return (await base.SaveChangesAsync(true, cancellationToken));
        }

        public override int SaveChanges(){
            DetectChanges();
            return base.SaveChanges();
        }

        private void DetectChanges(){
           ChangeTracker.DetectChanges();

            var modified = ChangeTracker.Entries().Where(x => x.State == EntityState.Added || x.State == EntityState.Modified || x.State == EntityState.Deleted);
            var updatingUser = "Test User";//_userService.GetUserName();

            Publisher publisher = new Publisher();

            foreach (var item in modified)
            {

                // item.CurrentValues.Properties[2].Name
                // item.CurrentValues["FamilyName"]
                publisher.Publish("DB Update" + item.State, updatingUser, JsonSerializer.Serialize(item.CurrentValues.ToObject()));
            }
        }
       
    //    private string GetJsonFromItem(PropertyValues values)
    //    {   
           
    //        var T = values.GetType();
    //        var entity = values.GetValue<typeof(T)>("FamilyName");  
    //         return "";
    //    }

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
