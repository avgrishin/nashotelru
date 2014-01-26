using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nashotelru.Controllers
{
  public class ErrorsController : Controller
  {
    public ActionResult NotFound()
    {
      object model = Request.Url.PathAndQuery;
      if (!Request.IsAjaxRequest())
        return View(model);
      else
        return PartialView("_NotFound", model);
    }
  }
}