using Microsoft.EntityFrameworkCore;
using TestAngularApp.Server.Models.Domain;

namespace TestAngularApp.Server.Data
{
    public class ApplicationDbContext: DbContext
    {
        //mention dbcontext in dbcontext options in case of multiple dbcontext
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {
                
        }

        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<Category> Categorys { get; set; }
        public DbSet<BlogImage> BlogImages { get; set; }
    }
}
