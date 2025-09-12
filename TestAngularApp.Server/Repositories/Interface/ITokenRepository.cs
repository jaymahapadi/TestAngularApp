using Microsoft.AspNetCore.Identity;

namespace TestAngularApp.Server.Repositories.Interface
{
    public interface ITokenRepository
    {
        string CreateJwtToken(IdentityUser user, List<string> roles);
    }
}
