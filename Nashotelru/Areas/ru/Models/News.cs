using System;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace Nashotelru.Areas.ru.Models
{
  public class News
  {
    public int ID { get; set; }

    public string Title { get; set; }
    public string Description { get; set; }

    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
    public DateTime Date { get; set; }
    public string Text { get; set; }
    public bool IsEnabled { get; set; }
  }

  public class NashotelDBContext : DbContext
  {
    public DbSet<News> News { get; set; }
  }

}