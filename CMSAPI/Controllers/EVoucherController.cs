using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMSAPI.Models;
using CMSAPI.Business;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CMSAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EVoucherController : ControllerBase
    {
        private readonly IJWTAuthenticationManager jWTAuthenticationManager;

        public EVoucherController(IJWTAuthenticationManager jWTAuthenticationManager)
        {
            this.jWTAuthenticationManager = jWTAuthenticationManager;
        }

        [AllowAnonymous]
        [HttpGet("authenticate")]
        public IActionResult Authenticate(string username, string password)
        {
            var token = jWTAuthenticationManager.Authenticate(username, password);

            if (token == null)
                return Unauthorized();
            ResponseMessage message = new ResponseMessage();
            message.MessageType = 1;
            message.Token = token;
            return Ok(message);
        }

        // GET: api/<EVoucherController>
        [HttpGet]
        public IActionResult Get()
        {
            List<EVoucher> list = new List<EVoucher>();
            EVoucherRepository repository = new EVoucherRepository();
            list = repository.GetAll();
            //return new string[] { "value1", "value2" };
            return Ok(list);
        }

        // GET api/<EVoucherController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            EVoucher model = new EVoucher();
            EVoucherRepository repository = new EVoucherRepository();
            model = repository.Get(id);
            //return new string[] { "value1", "value2" };
            return Ok(model);
        }

        // POST api/<EVoucherController>
        [HttpPost]
        public IActionResult Post(EVoucher obj)
        {
            EVoucherRepository repository = new EVoucherRepository();
            ResponseMessage response = repository.Save(obj);
            return Ok(response);
        }

        // PUT api/<EVoucherController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<EVoucherController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        [Route("api/EVoucher/GetAllPaymentMethod")]
        [HttpGet]
        public IActionResult GetAllPaymentMethod()
        {
            List<EVoucher> list = new List<EVoucher>();
            EVoucherRepository repository = new EVoucherRepository();
            list = repository.GetAllPaymentMethod();
            //return new string[] { "value1", "value2" };
            return Ok(list);
        }

        [Route("api/EVoucher/MakePayment")]
        [HttpPost]
        public IActionResult MakePayment(EVoucher obj)
        {
            EVoucherRepository repository = new EVoucherRepository();
            ResponseMessage response = repository.MakePayment(obj);
            return Ok(response);
        }

        [Route("api/EVoucher/VerifyPromoCode")]
        [HttpPost]
        public IActionResult VerifyPromoCode(EVoucher obj)
        {
            EVoucherRepository repository = new EVoucherRepository();
            ResponseMessage response = repository.VerifyPromoCode(obj);
            return Ok(response);
        }

        [Route("api/EVoucher/PurchaseHistory")]
        [HttpGet]
        public IActionResult PurchaseHistory(bool isused)
        {
            List<Purchase> list = new List<Purchase>();
            EVoucherRepository repository = new EVoucherRepository();
            list = repository.PurchaseHistory(isused);
            //return new string[] { "value1", "value2" };
            return Ok(list);
        }
    }
}
