using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Web.Mvc;

namespace Nashotelru.Models
{
  public class Page
  {
    public int ID { get; set; }
    [MaxLength(2)]
    public string Language { get; set; }
    [MaxLength(120)]
    public string Name { get; set; }
    public string Content { get; set; }
  }
  public class News
  {
    public int ID { get; set; }
    public DateTime Date { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Text { get; set; }
    public bool IsEnabled { get; set; }
  }

  public class NewsViewModel
  {
    public int ID { get; set; }

    [Display(Name = "Дата")]
    [DataType(DataType.Date)]
    [Required]
    [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
    public DateTime Date { get; set; }

    [Display(Name = "Заголовок", Prompt = "Заголовок")]
    [Required]
    [AllowHtml]
    public string Title { get; set; }

    [Display(Name = "Описание", Prompt = "Описание")]
    [AllowHtml]
    [DataType(DataType.MultilineText)]
    public string Description { get; set; }

    [Display(Name = "Содержание", Prompt = "Содержание")]
    //[UIHint("tinymce_jquery_full"), AllowHtml]
    [UIHint("ckeditor"), AllowHtml]
    [DataType(DataType.MultilineText)]
    public string Text { get; set; }

    [Display(Name = "Показывать?")]
    public bool IsEnabled { get; set; }
  }

  public class Response
  {
    public int ID { get; set; }

    [Display(Name = "Дата")]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy HH:mm}")]
    public DateTime Date { get; set; }

    [Required]
    [MaxLength(200)]
    [StringLength(200)]
    [Display(Name = "Ваше имя", Prompt = "Введите Ваше имя")]
    public string Name { get; set; }

    [Required]
    [MaxLength(200)]
    [StringLength(200)]
    [DataType(DataType.EmailAddress)]
    [Display(Name = "Координаты для обратной связи", Prompt = "Введите Ваш EMail")]
    public string Mail { get; set; }

    [Required]
    [Display(Name = "Ваше сообщение")]
    [DataType(DataType.MultilineText)]
    [AllowHtml]
    public string Text { get; set; }

    [Display(Name = "Ответ")]
    [AllowHtml]
    public string Text2 { get; set; }

    [MaxLength(15)]
    public string IP { get; set; }
    [Display(Name = "Показывать?")]
    public bool IsVisible { get; set; }
  }
  public class NashotelDBContext : DbContext
  {
    public DbSet<News> News { get; set; }
    public DbSet<Response> Response { get; set; }
    public DbSet<Page> Page { get; set; }
  }
}