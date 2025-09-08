using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestAngularApp.Server.Data;
using TestAngularApp.Server.Models.Domain;
using TestAngularApp.Server.Models.DTO;
using TestAngularApp.Server.Repositories.Interface;

namespace TestAngularApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository categoryRepository;

        public CategoriesController(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
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
            var response = await categoryRepository.CreateAsync(category);
                       
            return Ok(response);
        }
    }
}
