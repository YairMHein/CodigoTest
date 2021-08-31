using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMSAdmin.Business;
using CMSAdmin.Models;

namespace CMSAdmin.Controllers
{
    public class EVoucherController : Controller
    {
        public ActionResult Authenticate()
        {
            EVoucherRepository repository = new EVoucherRepository();
            ResponseMessage model =  repository.Authenticate("test1","password1");
            HttpContext.Session.SetString("_token", model.Token);
            return Ok();
        }

        [HttpGet]
        public ActionResult Index()
        {
            EVoucherRepository repository = new EVoucherRepository();
            string token = HttpContext.Session.GetString("_token");
            List<EVoucher> lst = repository.GetAll(token);
            return View(lst);
        }

        
        public ActionResult GetAllList()
        {
            EVoucherRepository repository = new EVoucherRepository();
            string token = HttpContext.Session.GetString("_token");
            List<EVoucher> lst = repository.GetAll(token);

            return Json(lst);
        }

        [HttpPost]
        public ActionResult Save()
        {
            string token = HttpContext.Session.GetString("_token");
            EVoucherRepository repository = new EVoucherRepository();
            ResponseMessage message = new ResponseMessage();
            EVoucher obj = new EVoucher();
            obj.amount = 10;
            obj.maxlimitbuy = 50;
            obj.maxlimitgift = 10;
            obj.buytype = 1;
            obj.createdate = DateTime.Now;
            obj.description = "This is description";
            obj.discountpercentage = null;
            obj.expirydate = DateTime.Now.AddYears(1);
            obj.image = "";
            obj.isactive = true;
            obj.paymentmethod = "VISA";
            obj.quantity = 1;
            obj.title = "Title";
            message = repository.Save(obj,token);
            return Ok(message);
        }
    }
}
