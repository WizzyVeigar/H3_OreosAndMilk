using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace H3_OreosAndMilk.Controllers
{
    [Route("Shopping/Edit")]
    public class CartDeleteController : Controller
    {
        //[HttpDelete]
        [HttpGet]
        [Route("Delete/{productName}/{productPrice}")]
        public IActionResult DeleteFromCart(string productName, int productPrice)
        {
            List<Product> cart = HttpContext.Session.GetObjectFromJson<List<Product>>("Cart");

            if (cart != null)
            {
                if (cart.Count > 0)
                {
                    for (int i = 0; i < cart.Count; i++)
                    {
                        if (cart[i].Name.ToLower() == productName.ToLower()
                            && cart[i].Price == productPrice)
                        {
                            cart.RemoveAt(i);
                            HttpContext.Session.SetObjectAsJson("Cart", cart);
                            return Ok(productName + " has been removed successfully");
                        }
                    }
                }
                else
                    return NotFound("No items are in your cart");
            }
            return NotFound("Cart has not been created yet");
        }
    }
}
