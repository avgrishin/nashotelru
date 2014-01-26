using System;
using System.ComponentModel.DataAnnotations;

namespace Nashotelru.Models
{
  public class ExternalLoginConfirmationViewModel
  {
    [Required]
    [Display(Name = "Логин")]
    public string UserName { get; set; }
  }

  public class ParamsUserViewModel
  {
    [StringLength(200)]
    [Display(Name = "Email", Prompt = "Введите Ваш Email")]
    public string EMail { get; set; }

    [StringLength(100)]
    [Display(Name = "Имя", Prompt = "Введите Ваше Имя")]
    public string FirstName { get; set; }

    [StringLength(100)]
    [Display(Name = "Фамилия", Prompt = "Введите Вашу Фамилию")]
    public string LastName { get; set; }
  }

  public class ManageUserViewModel
  {
    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Текущий пароль")]
    public string OldPassword { get; set; }

    [Required]
    [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
    [DataType(DataType.Password)]
    [Display(Name = "Новый парроль")]
    public string NewPassword { get; set; }

    [DataType(DataType.Password)]
    [Display(Name = "Подтвердите новый пароль")]
    [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
    public string ConfirmPassword { get; set; }
  }

  public class LoginViewModel
  {
    [Required]
    [Display(Name = "User name")]
    public string UserName { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string Password { get; set; }

    [Display(Name = "Remember me?")]
    public bool RememberMe { get; set; }
  }

  public class RegisterViewModel
  {
    [Required]
    [Display(Name = "User name")]
    public string UserName { get; set; }

    [Required]
    [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string Password { get; set; }

    [DataType(DataType.Password)]
    [Display(Name = "Confirm password")]
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    public string ConfirmPassword { get; set; }

    [MaxLength(200)]
    [StringLength(200)]
    [Display(Name = "EMail")]
    public string EMail { get; set; }

  }
}
