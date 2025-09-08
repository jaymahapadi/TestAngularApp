using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestAngularApp.Server.Data;
using TestAngularApp.Server.Models.Domain;
using TestAngularApp.Server.Models.DTO;

namespace TestAngularApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public CategoriesController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryRequestDto addCategoryDto)
        {
            // map DTO to domain model
            var category = new Category
            {
                Name = addCategoryDto.Name,
                UrlHandle = addCategoryDto.UrlHandle
            };
            
            // save to database
            await dbContext.Categorys.AddAsync(category);
            await dbContext.SaveChangesAsync();

            // return data in the respose in DTo
            CategoryDto response = new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                UrlHandle = category.UrlHandle
            };

           
            return Ok(response);
        }
    }
}
