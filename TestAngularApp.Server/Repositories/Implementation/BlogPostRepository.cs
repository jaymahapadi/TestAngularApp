using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using TestAngularApp.Server.Data;
using TestAngularApp.Server.Models.Domain;
using TestAngularApp.Server.Repositories.Interface;

namespace TestAngularApp.Server.Repositories.Implementation
{
    public class BlogPostRepository : IBlogPostRepository
    {
        private readonly ApplicationDbContext applicationDbContext;

        public BlogPostRepository(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }
        public async Task<BlogPost> CreateAsync(BlogPost blogPost)
        {
            await applicationDbContext.BlogPosts.AddAsync(blogPost);
            await applicationDbContext.SaveChangesAsync();
            
            return blogPost;

        }

        public async Task<IEnumerable<BlogPost>> GetAllAsync()
        {
            var response=await applicationDbContext.BlogPosts.Include(x=>x.Categories).ToListAsync();
            return response;
        }

        public async Task<BlogPost?> GetBlogPostByIdAsync(Guid id)
        {
            var response=await applicationDbContext.BlogPosts.Include(x => x.Categories).FirstOrDefaultAsync(x=>x.Id==id);
            return response;
        }

        public async Task<BlogPost?> UpdateBlogPostAsync(BlogPost blogPost)
        {
            var blogPostToUpdate=await applicationDbContext.BlogPosts.Include(x => x.Categories).FirstOrDefaultAsync(x=>x.Id==blogPost.Id);
            if (blogPostToUpdate==null)
            {
                return null;
            }
            
            applicationDbContext.Entry(blogPostToUpdate).CurrentValues.SetValues(blogPost);

            blogPostToUpdate.Categories=blogPost.Categories;

            await applicationDbContext.SaveChangesAsync();

            return blogPost;
        }

        public async Task<BlogPost?> DeleteBlogPostAsync(Guid id)
        {
            var deletedBlogPost=await applicationDbContext.BlogPosts.Include(x => x.Categories).FirstOrDefaultAsync(x=>x.Id==id);

            if (deletedBlogPost==null)
            {
                return null;
            }

            applicationDbContext.BlogPosts.Remove(deletedBlogPost);
            await applicationDbContext.SaveChangesAsync();

            return deletedBlogPost;
        }

        public async Task<BlogPost?> GetBlogPostByUrlHandleAsync(string urlHandle)
        {
            var response=await applicationDbContext.BlogPosts.Include(x => x.Categories).FirstOrDefaultAsync(x=>x.UrlHandle==urlHandle);
            return response;
        }
    }
}
