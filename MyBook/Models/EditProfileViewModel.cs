using System.ComponentModel.DataAnnotations;

namespace MyBook.Models;

public class EditProfileViewModel
{
    [Required]
    public string Name { get; set; } = null!;
    [Required]
    public string LastName { get; set; } = null!;
    [Required]
    public string Email { get; set; } = null!;

    public string Image { get; set; } = null!;
}