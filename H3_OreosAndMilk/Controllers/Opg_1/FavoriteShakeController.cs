using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace H3_OreosAndMilk.Controllers
{
    [Route("[controller]")]
    public class FavoriteShakeController : Controller
    {
        [HttpGet]
        [Route("ShowShake")]
        public IActionResult ShowFavoriteShake(string shakeName)
        {
            //Response.Cookies.Append("Favorite_Shake", shakeName);


            CookieOptions co = new CookieOptions()
            {
                MaxAge = TimeSpan.FromMinutes(5)
            };

            Response.Cookies.Append("Favorite_Shake", shakeName, co);
            return Ok("Din favorit milkshake er " + shakeName);
        }


        [HttpGet]
        [Route("[action]")]
        public IActionResult GetFavoriteShake()
        {
            string returnString = "Your favorite shake is ";
            returnString += Request.Cookies["Favorite_Shake"] == null ? "Unknown" : Request.Cookies["Favorite_Shake"];

            return Ok(returnString);
        }
    }
}
