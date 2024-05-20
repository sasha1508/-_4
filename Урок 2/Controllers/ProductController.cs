using Market.Abstractions;
using Market.DB;
using Market.DTO;
using Market.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using System.Text;
using Autofac;

namespace Market.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepo _repo;
       // private ProductContext _context;

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
            return Ok(_repo.GetProducts());
            //return Ok("Ok");
        }

        private string GetCsv(IEnumerable<ProductViewModel> products) 
        {
            StringBuilder sb = new();

            foreach (var product in products)
            {
                sb.AppendLine(product.Name + ";" + product.Description);
            }
            return sb.ToString();
        }

        [HttpGet(template:"GetProductCSV")]
        public FileContentResult GetProductCSV()
        {
            using (var _context = new ProductContext())
            {
                var produkt = _context.Products.Select(x => new ProductViewModel {Name = x.Name, Description = x.Description }).ToList();
                var content = GetCsv(produkt);
                return File (new System.Text.UTF8Encoding().GetBytes(content), "text/csv", "report.csv");
            }


   
        }


        [HttpDelete(template: "deleteproducts")]
        public void DeleteProducts(string name)
        {
            using (var ctx = new ProductContext())
            {
                var productToDelete = ctx.Products.FirstOrDefault(x => x.Name.ToLower() == name.ToLower());

                if (productToDelete != null)
                {
                    ctx.Products.Remove(productToDelete);
                    ctx.SaveChanges();
                }
            }
        }


        [HttpDelete(template: "deletegroupproducts")]
        public void DeleteGroupProducts(string name)
        {


            using (var ctx = new ProductContext())
            {
                var groupToDelete = ctx.ProductGroup.FirstOrDefault(x => x.Name.ToLower() == name.ToLower());

                if (groupToDelete != null)
                {
                    ctx.ProductGroup.Remove(groupToDelete);
                    ctx.SaveChanges();
                }
            }
        }


        [HttpPut(template: "setprice")]
        public void UpdatePrice(string name, int price)
        {
            using (var ctx = new ProductContext())
            {
                if (ctx.Products.Count(x => x.Name.ToLower() == name.ToLower()) > 0)
                {
                    var product = ctx.Products.FirstOrDefault(x => x.Name.ToLower() == name.ToLower());
                    product.Price = price;

                    ctx.SaveChanges();
                }
            }
        }


        [HttpPost(template: "addproductgroup")]
        public ActionResult AddGroup(string name, string description)
        {
            try
            {
                using (var ctx = new ProductContext())
                {
                    if (ctx.ProductGroup.Count(x => x.Name.ToLower() == name.ToLower()) > 0)
                    {
                        return StatusCode(409);
                    }
                    else
                    {
                        ctx.ProductGroup.Add(new ProductGroup { Name = name, Description = description });
                        ctx.SaveChanges();
                    }
                }

                return Ok();
            }
            catch
            {
                return StatusCode(500);
            }
        }
        [HttpGet(template: "getproductgroup")]
        public ActionResult<IEnumerable<ProductGroup>> GetProductGroup()
        {
            try
            {
                using (var ctx = new ProductContext())
                {
                    var list = ctx.ProductGroup.Select(x => new ProductGroup { Id = x.Id, Name = x.Name, Description = x.Description }).ToList();
                    return list;
                }
            }
            catch
            {
                return StatusCode(500);
            }
        }

    }


}
