using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace H3_OreosAndMilk.Controllers
{
    [Route("Shopping")]
    public class ShoppingCartController : Controller
    {
        //static int counter = 0;

        //List of products to choose from
        //Static so it won't re-instantiate each time the controller is called
        private static List<Product> products = new List<Product>()
        {
            new Product("Bread",20),
            new Product("Bread",20),
            new Product("Bread",20),
            new Product("Milk", 40),
            new Product("Milk", 40),
            new Product("Laser jet", 3),
            new Product("Team Hex Banner", 1000),
        };

        //public ShoppingCartController()
        //{
        //    counter++;
        //    Debug.WriteLine(counter);
        //}

        [HttpGet]
        public IEnumerable<Product> Get()
        {
            return products;
        }


        /// <summary>
        /// Method to use when you want to see the stock amount of a specific product
        /// </summary>
        /// <param name="productName">Name of the product</param>
        /// <param name="productPrice">Price of the product</param>
        /// <returns>Returns an integer value of the stock amount</returns>
        [HttpGet]
        [Route("GetStock/{productName}/{productPrice}")]
        public int GetProductStock(string productName = "", int? productPrice = null)
        {

            int productCount = 0;

            if (productName == "" || productPrice == null)
            {
                return 0;
            }

            if (products.Any())
            {
                for (int i = 0; i < products.Count; i++)
                {
                    Product product = products.ElementAt(i);
                    if (product.Name.ToLower() == productName.ToLower() &&
                        product.Price == productPrice)
                    {
                        productCount++;
                    }
                }
            }
            return productCount;
        }

        /// <summary>
        /// Removes an item from <see cref="products"/> and adds it to your cart in session
        /// </summary>
        /// <param name="productName"></param>
        /// <param name="productPrice"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Buy/{productName}/{productPrice}")]
        public IActionResult BuyProduct(string productName, int productPrice)
        {
            List<Product> cart = HttpContext.Session.GetObjectFromJson<List<Product>>("Cart");

            if (cart == null)
                cart = new List<Product>();

            for (int i = 0; i < products.Count; i++)
            {
                Product product = products.ElementAt(i);
                if (product.Name.ToLower() == productName.ToLower() && product.Price == productPrice)
                {
                    cart.Add(product);
                    products.Remove(product);
                    HttpContext.Session.SetObjectAsJson("Cart", cart);
                    return Ok(product.Name + " has been added to your cart successfully");
                }
            }
            return NotFound("Product not found");
        }

        /// <summary>
        /// show what items you have currently in your cart
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Show")]
        public IActionResult ShowCart()
        {
            List<Product> cart = HttpContext.Session.GetObjectFromJson<List<Product>>("Cart");
            if (cart.Count > 0)
            {
                return Ok(cart);
            }
            return Ok("No items are in the cart");
        }
    }
}
