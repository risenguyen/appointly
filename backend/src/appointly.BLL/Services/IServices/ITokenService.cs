using System.Security.Claims;

namespace appointly.BLL.Services.IServices;

public interface ITokenService
{
    string CreateToken(IEnumerable<Claim> claims);
}
