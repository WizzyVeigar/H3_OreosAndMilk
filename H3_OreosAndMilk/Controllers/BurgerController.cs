using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace H3_OreosAndMilk.Controllers
{
    [Route("[controller]")]
    public class BurgerController : Controller
    {
        [HttpGet]
        public IActionResult Get()
        {
            CookieOptions co = new CookieOptions()
            {
                MaxAge = TimeSpan.FromMinutes(6)
            };

            Response.Cookies.Append("burger", "Burger with cheese and tomatoes", co);
            return Ok("You have received your burger");
        }
    }
}
