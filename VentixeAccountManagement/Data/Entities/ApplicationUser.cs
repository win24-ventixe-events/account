using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace VentixeAccountManagement.Data.Entities;

public class ApplicationUser : IdentityUser
{
    [PersonalData]
    [Column(TypeName="nvarchar(50)")]
    public string DisplayName { get; set; } = null!;
}