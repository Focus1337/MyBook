﻿using System.ComponentModel.DataAnnotations;

namespace MyBook.Models;

public class LoginViewModel
{
    [Required(ErrorMessage = "Укажите почту")]
    public string Email { get; set; }
    [Required(ErrorMessage = "Укажите пароль")]
    public string Password { get; set; }
}