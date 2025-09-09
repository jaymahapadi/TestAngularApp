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

        [HttpGet]
        //GET   'https://localhost:7228/api/Categories' \

        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await categoryRepository.GetCategoriesAsync();
            var response =categories.Select(c => new CategoryDto
            {
                Id = c.Id,
                Name = c.Name,
                UrlHandle = c.UrlHandle
            });
            return Ok(response);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetCategoryById([FromRoute] Guid id)
        {
            var category = await categoryRepository.GetCategoryByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            var response = new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                UrlHandle = category.UrlHandle
            };
            return Ok(response);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateCategory([FromRoute] Guid id,UpdateCategoryDTO updateCategoryDTO)
        {
            var category = new Category 
            { Id = id
            , Name = updateCategoryDTO.Name
            , UrlHandle = updateCategoryDTO.UrlHandle };

            var updatedCategory = await categoryRepository.UpdateCategoryAsync(category);
            
            if (updatedCategory == null)
            {
                return NotFound();
            }
            var response = new CategoryDto
            {
                Id = updatedCategory.Id,
                Name = updatedCategory.Name,
                UrlHandle = updatedCategory.UrlHandle
            };
            return Ok(response);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteCategory([FromRoute] Guid id)
        {
            var deletedCategory = await categoryRepository.DeleteCategoryAsync(id);
            if (deletedCategory == null)
            {
                return NotFound();
            }
            var response = new CategoryDto
            {
                Id = deletedCategory.Id,
                Name = deletedCategory.Name,
                UrlHandle = deletedCategory.UrlHandle
            };
            return Ok(response);
        }
    }
}
