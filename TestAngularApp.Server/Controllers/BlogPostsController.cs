using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestAngularApp.Server.Models.Domain;
using TestAngularApp.Server.Models.DTO;
using TestAngularApp.Server.Repositories.Implementation;
using TestAngularApp.Server.Repositories.Interface;

namespace TestAngularApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostsController : ControllerBase
    {
        private readonly IBlogPostRepository blogPostRepository;
        private readonly ICategoryRepository categoryRepository;

        public BlogPostsController(IBlogPostRepository blogPostRepository
            ,ICategoryRepository categoryRepository)
        {
            this.blogPostRepository = blogPostRepository;
            this.categoryRepository = categoryRepository;
        }


        [HttpPost]
        public async Task<IActionResult> CreateBlogPost([FromBody] BlogPostsDTO blogPostsDTO)
        {
            // Logic to create a new blog post
            var blogpost=new BlogPost
            {
                Title = blogPostsDTO.Title,
                ShortDescription = blogPostsDTO.ShortDescription,
                Content = blogPostsDTO.Content,
                FeaturedImageUrl = blogPostsDTO.FeaturedImageUrl,
                UrlHandle = blogPostsDTO.UrlHandle,
                PublishedDate = blogPostsDTO.PublishedDate,
                Author = blogPostsDTO.Author,
                IsVisible = blogPostsDTO.IsVisible,
                Categories = new List<Category>()
            };
            
            if (blogPostsDTO.Categories != null && blogPostsDTO.Categories.Any())
            {
                foreach (var categoryId in blogPostsDTO.Categories)
                {
                    var category = await categoryRepository.GetCategoryByIdAsync(categoryId);
                    if (category != null)
                    {
                        blogpost.Categories.Add(category);
                    }
                }
            }
            blogpost = await blogPostRepository.CreateAsync(blogpost);

            var response = new NewBlogPostDto { 
                Id = blogpost.Id,
                Title = blogpost.Title,
                ShortDescription = blogpost.ShortDescription,
                Content = blogpost.Content,
                FeaturedImageUrl = blogpost.FeaturedImageUrl,
                UrlHandle = blogpost.UrlHandle,
                PublishedDate = blogpost.PublishedDate,
                Author = blogpost.Author,
                IsVisible = blogpost.IsVisible,
                Categories = blogpost.Categories.Select(c => new CategoryDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    UrlHandle = c.UrlHandle
                }).ToList()
            };

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBlogPosts()
        {
            var blogPosts = await blogPostRepository.GetAllAsync();
            var response= blogPosts.Select(blogpost => new NewBlogPostDto
            {
                Id = blogpost.Id,
                Title = blogpost.Title,
                ShortDescription = blogpost.ShortDescription,
                Content = blogpost.Content,
                FeaturedImageUrl = blogpost.FeaturedImageUrl,
                UrlHandle = blogpost.UrlHandle,
                PublishedDate = blogpost.PublishedDate,
                Author = blogpost.Author,
                IsVisible = blogpost.IsVisible,
                Categories = blogpost.Categories.Select(c => new CategoryDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    UrlHandle = c.UrlHandle
                }).ToList()
            }).ToList();
            return Ok(response);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetBlogPostById([FromRoute] Guid id)
        {
            var blogpost = await blogPostRepository.GetBlogPostByIdAsync(id);
            if (blogpost == null)
            {
                return NotFound();
            }
            var response = new NewBlogPostDto
            {
                Id = blogpost.Id,
                Title = blogpost.Title,
                ShortDescription = blogpost.ShortDescription,
                Content = blogpost.Content,
                FeaturedImageUrl = blogpost.FeaturedImageUrl,
                UrlHandle = blogpost.UrlHandle,
                PublishedDate = blogpost.PublishedDate,
                Author = blogpost.Author,
                IsVisible = blogpost.IsVisible,
                Categories= blogpost.Categories.Select(c => new CategoryDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    UrlHandle = c.UrlHandle
                }).ToList()
            };
            return Ok(response);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateBlogPost([FromRoute] Guid id, UpdateBlogPostDto updateBlogPostDto)
        {
            var blogpost = new BlogPost
            {
                Id = id,
                Title = updateBlogPostDto.Title,
                ShortDescription = updateBlogPostDto.ShortDescription,
                Content = updateBlogPostDto.Content,
                FeaturedImageUrl = updateBlogPostDto.FeaturedImageUrl,
                UrlHandle = updateBlogPostDto.UrlHandle,
                PublishedDate = updateBlogPostDto.PublishedDate,
                Author = updateBlogPostDto.Author,
                IsVisible = updateBlogPostDto.IsVisible,
                Categories = new List<Category>()
            };
            // Logic to update the blog post
            foreach (var categoryId in updateBlogPostDto.Categories)
            {
                var category = await categoryRepository.GetCategoryByIdAsync(categoryId);
                if (category != null)
                {
                    blogpost.Categories.Add(category);
                }
            }

            var blogPost=await blogPostRepository.UpdateBlogPostAsync(blogpost);

            if (blogPost==null)
            {
                return NotFound();
            }

            var updatedBlogPost= new NewBlogPostDto
            {
                Id = blogPost.Id,
                Title = blogPost.Title,
                ShortDescription = blogPost.ShortDescription,
                Content = blogPost.Content,
                FeaturedImageUrl = blogPost.FeaturedImageUrl,
                UrlHandle = blogPost.UrlHandle,
                PublishedDate = blogPost.PublishedDate,
                Author = blogPost.Author,
                IsVisible = blogPost.IsVisible,
                Categories = blogPost.Categories.Select(c => new CategoryDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    UrlHandle = c.UrlHandle
                }).ToList()
            };

            return Ok(updatedBlogPost);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteBlogPost([FromRoute] Guid id)
        {
            var blogpost = await blogPostRepository.DeleteBlogPostAsync(id);
            if (blogpost == null)
            {
                return NotFound();
            }
            var response = new NewBlogPostDto
            {
                Id = blogpost.Id,
                Title = blogpost.Title,
                ShortDescription = blogpost.ShortDescription,
                Content = blogpost.Content,
                FeaturedImageUrl = blogpost.FeaturedImageUrl,
                UrlHandle = blogpost.UrlHandle,
                PublishedDate = blogpost.PublishedDate,
                Author = blogpost.Author,
                IsVisible = blogpost.IsVisible,
                Categories = blogpost.Categories.Select(c => new CategoryDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    UrlHandle = c.UrlHandle
                }).ToList()
            };
            return Ok(response);
        }

        [HttpGet]
        [Route("urlHandle/{urlHandle}")]
        public async Task<IActionResult> GetBlogPostByUrlHandle([FromRoute] string urlHandle)
        {
            var blogpost = await blogPostRepository.GetBlogPostByUrlHandleAsync(urlHandle);
            if (blogpost == null)
            {
                return NotFound();
            }
            var response = new NewBlogPostDto
            {
                Id = blogpost.Id,
                Title = blogpost.Title,
                ShortDescription = blogpost.ShortDescription,
                Content = blogpost.Content,
                FeaturedImageUrl = blogpost.FeaturedImageUrl,
                UrlHandle = blogpost.UrlHandle,
                PublishedDate = blogpost.PublishedDate,
                Author = blogpost.Author,
                IsVisible = blogpost.IsVisible,
                Categories = blogpost.Categories.Select(c => new CategoryDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    UrlHandle = c.UrlHandle
                }).ToList()
            };
            return Ok(response);
        }
    }
}
