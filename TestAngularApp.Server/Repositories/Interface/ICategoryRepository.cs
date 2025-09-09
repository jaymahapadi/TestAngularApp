using TestAngularApp.Server.Models.Domain;

namespace TestAngularApp.Server.Repositories.Interface
{
    public interface ICategoryRepository
    {
        Task<Category> CreateAsync(Category category);

        Task<IEnumerable<Category>> GetCategoriesAsync();

        Task<Category?> GetCategoryByIdAsync(Guid id);

        Task<Category?> UpdateCategoryAsync(Category category);
    }
}
