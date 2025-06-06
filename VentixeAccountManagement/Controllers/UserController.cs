using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VentixeAccountManagement.Data.Entities;
using VentixeAccountManagement.Data.Models;
using VentixeAccountManagement.Services;

namespace VentixeAccountManagement.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, JwtService jwtService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Register([FromBody] UserRegisterDto model)
    {
        var user = new ApplicationUser
        {
            UserName = model.Email,
            Email = model.Email,
            DisplayName = model.DisplayName,
        };

        var result = await userManager.CreateAsync(user, model.Password);

        if (!result.Succeeded)
            return BadRequest(result.Errors);

        user.EmailConfirmed = true;
        var update = await userManager.UpdateAsync(user);
        
        if (!update.Succeeded)
        {
            return BadRequest(update.Errors);
        }
        
        return Ok(new { user.Id, user.Email, user.DisplayName });
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserLoginDto model)
    {
        var result = await signInManager.PasswordSignInAsync(
            model.Email,
            model.Password,
            false,
            lockoutOnFailure: false);

        if (!result.Succeeded)
            return Unauthorized(new { Message = "Invalid credentials." });
        
        var user = await userManager.FindByEmailAsync(model.Email);
        
        if (user == null)
        {
            return Unauthorized(new { Message = "User not found after successful login." });
        }
        
        var token = jwtService.GenerateJwt(user!);
        
        return Ok(new
        {
            token
        });
    }
    
}