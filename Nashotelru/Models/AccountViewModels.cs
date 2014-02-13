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
    [DataType(DataType.EmailAddress)]
    [Display(Name = "Email", Prompt = "Введите Ваш Email")]
    public string EMail { get; set; }

    [StringLength(20)]
    [Display(Name = "Телефон", Prompt = "Введите Ваш Телефон")]
    public string Phone { get; set; }

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
    [Display(Name = "Старый пароль", Prompt = "Старый пароль")]
    public string OldPassword { get; set; }

    [Required]
    [StringLength(100, ErrorMessage = "{0} должен быть не короче {2} символов", MinimumLength = 6)]
    [DataType(DataType.Password)]
    [Display(Name = "Новый пароль", Prompt = "Новый пароль")]
    public string NewPassword { get; set; }

    [DataType(DataType.Password)]
    [Display(Name = "Подтверждение", Prompt = "Подтверждение")]
    [Compare("NewPassword", ErrorMessage = "Введенные пароли не совпадают.")]
    public string ConfirmPassword { get; set; }
  }

  public class LoginViewModel
  {
    [Required(ErrorMessageResourceName = "YouMustSpecifyUserName", ErrorMessageResourceType = typeof(Resources.Account.Account))]
    [Display(ResourceType = typeof(Resources.Account.Account), Name = "LoginLabel", Prompt = "LoginLabel")]
    public string UserName { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [Display(ResourceType = typeof(Resources.Account.Account), Name = "PasswordLabel", Prompt = "PasswordLabel")]
    public string Password { get; set; }

    [Display(ResourceType = typeof(Resources.Account.Account), Name = "RememberMeLabel")]
    public bool RememberMe { get; set; }
  }

  public class RegisterViewModel
  {
    [Required]
    [Display(ResourceType = typeof(Resources.Account.Account), Name = "LoginLabel", Prompt = "LoginLabel")]
    public string UserName { get; set; }

    [Required]
    [StringLength(100, ErrorMessage = "{0} должен быть не короче {2} символов.", MinimumLength = 6)]
    [DataType(DataType.Password)]
    [Display(Name = "Пароль", Prompt = "Пароль")]
    public string Password { get; set; }

    [DataType(DataType.Password)]
    [Display(Name = "Подтверждение", Prompt = "Подтверждение")]
    [Compare("Password", ErrorMessage = "Введенные пароли не совпадают.")]
    public string ConfirmPassword { get; set; }

    [Required]
    [StringLength(200)]
    [Display(Name = "Email", Prompt = "Введите Ваш Email")]
    [DataType(DataType.EmailAddress)]
    public string EMail { get; set; }
  }

  public class RemindViewModel
  {
    [Required]
    [Display(Name = "Логин или Email", Prompt = "Введите Ваш Email или Логин")]
    public string UserName { get; set; }
  }

  public class RemindConfirmViewModel
  {
    [Required]
    [StringLength(100, ErrorMessage = "{0} должен быть не короче {2} символов.", MinimumLength = 6)]
    [DataType(DataType.Password)]
    [Display(Name = "Новый пароль", Prompt = "Новый пароль")]
    public string Password { get; set; }

    [DataType(DataType.Password)]
    [Display(Name = "Подтверждение", Prompt = "Подтверждение пароля")]
    [Compare("Password", ErrorMessage = "Введенные пароли не совпадают.")]
    public string ConfirmPassword { get; set; }

    [Required]
    public string ReminderToken { get; set; }
  }

}
