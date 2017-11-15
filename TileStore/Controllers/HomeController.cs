using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TileStore.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [HttpPost]
        public ActionResult Login()
        {
            var email = Request.Form["email"];
            var password = Request.Form["password"];

            if(email.Equals("salesperson@leemutech.com", StringComparison.OrdinalIgnoreCase) && password.Equals("Updating@1234"))
            {
                return RedirectToAction("Index", "Sales");
            }

            if (email.Equals("cashier@leemutech.com", StringComparison.OrdinalIgnoreCase) && password.Equals("Updating@1234"))
            {
                return RedirectToAction("GetInvoiceId", "Sales");
            }

            else return Content("Email and/or password was not valid");
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}