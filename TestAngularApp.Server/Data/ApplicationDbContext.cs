using Microsoft.EntityFrameworkCore;
using TestAngularApp.Server.Models.Domain;

namespace TestAngularApp.Server.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions options):base(options)
        {
                
        }

        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<Category> Categorys { get; set; }
    }
}
