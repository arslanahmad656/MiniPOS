using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TileStore.Models;
using System.ComponentModel.DataAnnotations;

namespace TileStore.Controllers
{
    //[Authorize(Roles = "salesperson")]
    public class SalesController : Controller
    {
        private Entities db = new Entities();
        // GET: Sales
        public ActionResult Index()
        {
            ViewBag.ItemId = new SelectList(db.Items, "Id", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult GenerateToken(Invoice model)
        {
            var itemId = Convert.ToInt32(Request.Form["ItemId"]);
            var quantity = Convert.ToInt32(Request.Form["quantity"]);
            var billTo = Request.Form["billto"];
            var paidPrice = Convert.ToDecimal(Request.Form["finalprice"]);
            var date = DateTime.Now;
            var invoice = new Invoice
            {
                BillTo = billTo,
                Date = date,
                ItemId = itemId,
                PaidPrice = paidPrice,
                Quantity = quantity
            };
            db.Invoices.Add(invoice);
            db.SaveChanges();
            ViewBag.ItemName = db.Items.Find(itemId).Name;
            return View(invoice);
        }

        public ActionResult GetInvoiceId()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Bill()
        {
            int invoiceId = Convert.ToInt32(Request.Form["InvoiceId"]);
            var invoice = db.Invoices.Find(invoiceId);
            return View(invoice);
        }

        [HttpPost]
        public ActionResult GenerateBill(Bill model)
        {
            var invoiceId = Convert.ToInt32(Request.Form["InvoiceId"]);
            var bill = new Bill
            {
                InvoiceId = invoiceId,
                ReceivedCash = Convert.ToDecimal(Request.Form["cashpayed"]),
                ReturnedCash = Convert.ToDecimal(Request.Form["cashreturned"])
            };
            db.Bills.Add(bill);
            db.SaveChanges();
            var invoice = db.Invoices.Find(bill.InvoiceId);
            ViewBag.Invoice = invoice;
            ViewBag.ProductName = db.Items.Find(invoice.ItemId).Name;
            return View(bill);
        }

        public JsonResult GetItemDetails(int id)
        {
            var item = db.Items.Find(id);
            if(item == null)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
            return Json(new { item.Name, item.Price }, JsonRequestBehavior.AllowGet);
        }
    }
}