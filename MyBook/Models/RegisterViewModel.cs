using System.ComponentModel.DataAnnotations;

namespace MyBook.Models;

public class RegisterViewModel
{
    [Required(ErrorMessage = "Укажите имя")]
    public string Name { get; set; }
    [Required(ErrorMessage = "Укажите фамилию")]
    public string Lastname { get; set; }
    [Required(ErrorMessage = "Укажите пароль")]
    public string Password { get; set; }
    [Required(ErrorMessage = "Укажите почту")]
    [EmailAddress]
    public string Email { get; set; }
}