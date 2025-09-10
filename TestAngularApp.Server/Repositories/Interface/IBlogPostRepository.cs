using TestAngularApp.Server.Models.Domain;

namespace TestAngularApp.Server.Repositories.Interface
{
    public interface IBlogPostRepository
    {
        Task<BlogPost> CreateAsync(BlogPost blogPost);

        Task<IEnumerable<BlogPost>> GetAllAsync();

        Task<BlogPost?> GetBlogPostByIdAsync(Guid id);

        Task<BlogPost?> UpdateBlogPostAsync(BlogPost blogPost);
        Task<BlogPost?> DeleteBlogPostAsync(Guid id);
    }
}
