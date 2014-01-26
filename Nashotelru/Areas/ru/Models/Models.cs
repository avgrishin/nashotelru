﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Web.Mvc;

namespace Nashotelru.Areas.ru.Models
{
  public class News
  {
    public int ID { get; set; }

    [Display(Name = "Дата")]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
    public DateTime Date { get; set; }

    [Display(Name = "Заголовок", Prompt = "Заголовок")]
    [AllowHtml]
    public string Title { get; set; }

    [Display(Name = "Описание", Prompt = "Описание")]
    [AllowHtml]
    public string Description { get; set; }

    [Display(Name = "Содержание", Prompt = "Содержание")]
    [AllowHtml]
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
    [Display(Name = "Ваше имя", Prompt = "Ваше имя")]
    public string Name { get; set; }

    [Required]
    [MaxLength(200)]
    [StringLength(200)]
    [Display(Name = "Координаты для обратной связи")]
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
  }
}