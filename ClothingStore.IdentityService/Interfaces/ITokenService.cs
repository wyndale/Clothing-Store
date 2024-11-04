using Microsoft.AspNetCore.Identity;

namespace ClothingStore.IdentityService.Interfaces;

public interface ITokenService
{
    Task<string> GenerateTokenAsync(IdentityUser user);
}
