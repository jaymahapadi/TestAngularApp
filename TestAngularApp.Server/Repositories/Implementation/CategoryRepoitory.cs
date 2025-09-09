using Microsoft.EntityFrameworkCore;
using TestAngularApp.Server.Data;
using TestAngularApp.Server.Models.Domain;
using TestAngularApp.Server.Repositories.Interface;

namespace TestAngularApp.Server.Repositories.Implementation
{
    public class CategoryRepoitory : ICategoryRepository
    {
        private readonly ApplicationDbContext dbContext;

        public CategoryRepoitory(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Category> CreateAsync(Category category)
        {
            await dbContext.Categorys.AddAsync(category);
            await dbContext.SaveChangesAsync();
            return category;
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            return await dbContext.Categorys.ToListAsync();
        }

        public async Task<Category?> GetCategoryByIdAsync(Guid id)
        {
            return await dbContext.Categorys.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Category?> UpdateCategoryAsync(Category category)
        {
            var categoryToUpdate = await dbContext.Categorys.FirstOrDefaultAsync(c => c.Id == category.Id);

            if (categoryToUpdate != null)
            {
                dbContext.Entry(categoryToUpdate).CurrentValues.SetValues(category);
                await dbContext.SaveChangesAsync();
                return category;
            }

            return null;
        }
    }
}
