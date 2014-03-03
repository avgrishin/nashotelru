using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nashotelru.Models
{
  public class HomeIndexViewModel
  {
    public Page Page { get; set; }
    public IEnumerable<Nashotelru.Models.News> News { get; set; }
  }
}