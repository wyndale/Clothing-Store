using System.ComponentModel.DataAnnotations;

namespace ClothingStore.IdentityService.Model;

public class RegisterModel
{
    [Required]
    [StringLength(25, MinimumLength = 6)]
    public string Username { get; set; } = string.Empty;

    [Required]
    [DataType(DataType.Password)]
    [StringLength(25, MinimumLength = 8)]
    public string Password { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
}
