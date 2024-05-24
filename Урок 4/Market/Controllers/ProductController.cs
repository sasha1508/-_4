using System.Text;
using Market.DB;
using Market.DTO;
using Market.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Path = System.IO.Path;

namespace Market.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : Controller
    {
        private readonly IProductRepo _repo;

        public ProductController(IProductRepo repo)
        {
            _repo = repo;
        }

        [HttpPost(template: "addproduct")]
        public ActionResult AddProduct(ProductViewModel productViewModel)
        {
            try
            {
                _repo.AddProduct(productViewModel);
                
                return Ok();
            }
            catch
            {
                return StatusCode(409);
            }
        }

        [HttpGet(template: "getproducts")]
        public ActionResult<IEnumerable<ProductViewModel>> GetProducts()
        {
            try
            {
                return Ok(_repo.GetProducts());
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpDelete(template: "deleteproducts")]
        public ActionResult<int> DeleteProducts()
        {
            try
            {
                using (var ctx = new ProductContext())
                {
                    if (!ctx.Products.Any())
                    {
                        return NotFound();
                    }

                    var list = ctx.Products.ExecuteDelete();
                    ctx.SaveChanges();
                    return Ok(list);
                }
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPut(template: "updateproduct")]
        public ActionResult UpdateProducts(string name, string newName, string newDescription)
        {
            try
            {
                using (var ctx = new ProductContext())
                {
                    var product = ctx.Products.FirstOrDefault(x => x.Name == name);
                    if (product != null)
                    {
                        product.Name = newName;
                        product.Description = newDescription;
                    }
                    else
                    {
                        ctx.Products.Add(new Product { Name = name, Description = newDescription });
                    }

                    ctx.SaveChanges();
                }

                return Ok();
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpGet(template: "getproductscsv")]
        public FileContentResult GetProductsCsv()
        {
            var content =  _repo.GetProductsCsv();
            return File(new UTF8Encoding().GetBytes(content), "text/csv", "report.csv");
        }

        [HttpGet(template: "getproductscsvurl")]
        public ActionResult<string> GetProductsCsvUrl()
        {
            var content = _repo.GetProductsCsv();
            string filename = "products" + DateTime.Now.ToBinary().ToString() + ".csv";

            System.IO.File.WriteAllText(Path.Combine(Directory.GetCurrentDirectory(), "StaticFiles", filename), content);

            return "https://" + Request.Host + "/static/" + filename;
        }

        [HttpGet(template: "checkproduct")]
        public ActionResult<bool> CheckProduct(int productId)
        {
            return Ok(_repo.CheckProduct(productId));
        }
    }
}