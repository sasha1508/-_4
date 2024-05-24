using Market.DB;
using Market.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Market.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController: Controller
    {
        [HttpPost(template: "addgroup")]
        public ActionResult AddProduct(string name, string description)
        {
            try
            {
                using (var ctx = new ProductContext())
                {
                    if (ctx.Categories.Count(x => x.Name.ToLower() == name.ToLower()) > 0)
                    {
                        return StatusCode(409);
                    }

                    ctx.Categories.Add(new Category { Name = name, Description = description });
                    ctx.SaveChanges();
                }

                return Ok();
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpGet(template: "getgroups")]
        public ActionResult<IEnumerable<Product>> GetProducts()
        {
            try
            {
                using (var ctx = new ProductContext())
                {
                    var list = ctx.Categories.Select(x => new Product
                        { Id = x.Id, Name = x.Name, Description = x.Description }).ToList();
                    return list;
                }
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpDelete(template: "deletegroups")]
        public ActionResult<int> DeleteProducts()
        {
            try
            {
                using (var ctx = new ProductContext())
                {
                    if (!ctx.Categories.Any())
                    {
                        return NotFound();
                    }

                    var list = ctx.Categories.ExecuteDelete();
                    ctx.SaveChanges();
                    return Ok(list);
                }
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPut(template: "updategroup")]
        public ActionResult UpdateProducts(string name, string newName, string newDescription)
        {
            try
            {
                using (var ctx = new ProductContext())
                {
                    var category = ctx.Categories.FirstOrDefault(x => x.Name == name);
                    if (category != null)
                    {
                        category.Name = newName;
                        category.Description = newDescription;
                    }
                    else
                    {
                        ctx.Categories.Add(new Category { Name = name, Description = newDescription });
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
    }
}