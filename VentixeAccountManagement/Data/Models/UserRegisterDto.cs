namespace VentixeAccountManagement.Data.Models;

public class UserRegisterDto
{
    public string Email { get; set; } = null!;
    public string DisplayName { get; set; } = null!;
    public string Password { get; set; } = null!;
}