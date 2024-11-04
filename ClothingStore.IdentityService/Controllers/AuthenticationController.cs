using ClothingStore.IdentityService.Interfaces;
using ClothingStore.IdentityService.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ClothingStore.IdentityService.Controllers;

public class AuthenticationController : BaseController
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly ITokenService _tokenService;

    public AuthenticationController(
        UserManager<IdentityUser> userManager,
        SignInManager<IdentityUser> signInManager,
        ITokenService tokenService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenService = tokenService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterModel model)
    {
        var user = new IdentityUser
        {
            UserName = model.Username,
            Email = model.Email
        };

        var result = await _userManager.CreateAsync(user, model.Password);

        if (result.Succeeded)
        {
            return Ok(new { result = "User registered successfully." });
        }

        return BadRequest(result.Errors);
    }

    // Jwt-based login
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginModel model)
    {
        var user = await _userManager.FindByNameAsync(model.Username);

        if (user == null) return Unauthorized("Invalid credentials");

        var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);

        if (result.Succeeded)
        {
            var token = await _tokenService.GenerateTokenAsync(user);
            return Ok(new { Token = token });
        }

        return Unauthorized("Invalid credentials");
    }
}
