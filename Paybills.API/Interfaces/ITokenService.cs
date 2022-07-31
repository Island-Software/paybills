using Paybills.API.Entities;

namespace Paybills.API.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}