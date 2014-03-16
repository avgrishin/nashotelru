using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nashotelru.Models
{
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

  public enum Rooms : int
  {
    one = 1,
    two = 2,
    three = 3,
    four = 4
  }
  public enum Adults : int
  {
    one = 1,
    two = 2,
    three = 3
  }
  public enum Children : int
  {
    one = 1,
    two = 2,
    three = 3,
    four = 4
  }
  public class BookingViewModel
  {
    [Display(ResourceType = typeof(Resources.Shared.Menu), Name = "BK_CheckIn")]
    [DataType(DataType.Date)]
    [Required]
    [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
    public DateTime? arrivalDate { get; set; }
    [Display(ResourceType = typeof(Resources.Shared.Menu), Name = "BK_CheckOut")]
    [DataType(DataType.Date)]
    [Required]
    [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
    public DateTime? departureDate { get; set; }
    [Display(ResourceType = typeof(Resources.Shared.Menu), Name = "BK_Rooms")]
    public Rooms? rooms { get; set; }
    [Display(ResourceType = typeof(Resources.Shared.Menu), Name = "BK_Adults")]
    public Adults? adults { get; set; }
    [Display(ResourceType = typeof(Resources.Shared.Menu), Name = "BK_Children")]
    public Children? children { get; set; }
    [Display(ResourceType = typeof(Resources.Shared.Menu), Name = "BK_Promocode")]
    public string promoText { get; set; }
  }

  public class PageViewModel
  {
    public int ID { get; set; }
    [MaxLength(2)]
    public string Language { get; set; }
    [MaxLength(120)]
    public string Name { get; set; }
  }
}