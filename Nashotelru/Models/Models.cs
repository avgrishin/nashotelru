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
    [UIHint("ckeditor"), AllowHtml]
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

  public class Response
  {
    public int ID { get; set; }

    [Display(ResourceType = typeof(Resources.Response.Response), Name = "Date")]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy HH:mm}")]
    public DateTime Date { get; set; }

    [Required]
    [MaxLength(200)]
    [StringLength(200)]
    [Display(ResourceType = typeof(Resources.Response.Response), Name = "Name", Prompt = "NamePrompt")]
    public string Name { get; set; }

    [Required]
    [MaxLength(200)]
    [StringLength(200)]
    [DataType(DataType.EmailAddress)]
    [Display(ResourceType = typeof(Resources.Response.Response), Name = "EmailAddress", Prompt = "EmailAddressPrompt")]
    public string Mail { get; set; }

    [Required]
    [Display(ResourceType = typeof(Resources.Response.Response), Name = "Text")]
    [DataType(DataType.MultilineText)]
    [AllowHtml]
    public string Text { get; set; }

    [Display(ResourceType = typeof(Resources.Response.Response), Name = "Text2")]
    [AllowHtml]
    public string Text2 { get; set; }

    [MaxLength(15)]
    public string IP { get; set; }
    [Display(ResourceType = typeof(Resources.Response.Response), Name = "IsVisible")]
    public bool IsVisible { get; set; }
  }
  public class NashotelDBContext : DbContext
  {
    public DbSet<News> News { get; set; }
    public DbSet<Response> Response { get; set; }
    public DbSet<Page> Page { get; set; }
  }
}