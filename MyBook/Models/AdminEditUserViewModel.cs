using System.ComponentModel.DataAnnotations;

namespace MyBook.Models;

public class AdminEditUserViewModel
{
    public string Id { get; set; } = null!;

    [Required]
    public string Name { get; set; } = null!;
    [Required]
    public string LastName { get; set; } = null!;
    [Required]
    public string Email { get; set; } = null!;
}