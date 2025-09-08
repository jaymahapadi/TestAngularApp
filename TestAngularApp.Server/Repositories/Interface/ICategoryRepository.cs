using TestAngularApp.Server.Models.Domain;

namespace TestAngularApp.Server.Repositories.Interface
{
    public interface ICategoryRepository
    {
        public Task<Category> CreateAsync(Category category);
    }
}
