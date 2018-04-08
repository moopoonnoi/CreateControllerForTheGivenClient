using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebServer.Models;

namespace WebServer.Controllers
{
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
        // GET api/values
        [HttpGet]
        public ActionResult GetAllProducts()
        {
            if (FakeData.Products != null)
            {
                return Ok(FakeData.Products.Values.ToArray());
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("{id}")]
        public ActionResult GetProductByID(int id)
        {
            if (FakeData.Products.ContainsKey(id))
            {
                return Ok(FakeData.Products[id]);
            }
            else
            {
                return NotFound();
            }

        }

        [HttpGet("price/{low}/{high}")]
        public ActionResult GetProductsByPrice(int low, int high)
        {
            var products = FakeData.Products.Values.Where(p => p.Price >= low && p.Price <= high).ToArray();
            if (products.Length > 0)
            {
                return Ok(products);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteProductByID(int id)
        {
            if(FakeData.Products.ContainsKey(id))
            {
                FakeData.Products.Remove(id);
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public ActionResult CreateNewProduct([FromBody]Product product)
        {
            product.ID = FakeData.Products.Keys.Max() + 1;
            FakeData.Products.Add(product.ID, product);
            return Created($"api/products/{product.ID}", product);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateProductByID(int id,[FromBody]Product product)
        {
            if(FakeData.Products.ContainsKey(id))
            {
                //FakeData.Products.Remove(id);
                //FakeData.Products.Add(id, product);

                var target = FakeData.Products[id];
                target.ID = product.ID;
                target.Name = product.Name;
                target.Price = product.Price;

                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPut("raise/{val}")]
        public ActionResult UpdateAllProductPriceByVal(int val)
        {
            var total = FakeData.Products.Keys.Max();

            for (int i = 0; i <= total; i++)
            {
                var target = FakeData.Products[i];
                target.Price = target.Price * val;
            }

            return Ok();
        }

    }
}
