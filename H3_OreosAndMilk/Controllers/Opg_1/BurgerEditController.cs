using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace H3_OreosAndMilk.Controllers
{
    [Route("Edit")]
    public class BurgerEditController : Controller
    {
        [HttpGet]
        [Route("burger")]
        public IActionResult EditBurger()
        {
            string cookieValue = Request.Cookies["burger"];
            cookieValue = cookieValue.Replace("and tomatoes", "");
            Response.Cookies.Append("burger", cookieValue,
                new CookieOptions() { MaxAge = TimeSpan.FromMinutes(5) });

            return Ok(cookieValue);
        }

        [HttpGet]
        [Route("Remove")]
        public IActionResult RemoveCookie()
        {
            CookieOptions co = new CookieOptions()
            {
                Expires = DateTime.Now.AddDays(-1)
            };
            Response.Cookies.Append("burger", "", co);

            return Ok("Cookie removed");
        }
    }
}
