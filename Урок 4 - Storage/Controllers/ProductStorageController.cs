using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Storage.Abstraction;
using Storage.Clients;
using Storage.DTO;

namespace Storage.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductStorageController(IProductStorageRepo repo) : Controller
    {
        [HttpPost(template: "addproductstorage")]
        public async Task<ProductStorageResultDto> AddProductStorage(ProductStorageViewModel productStorageViewModel)
        {
            var productExistTask = new ProductClient().Exists(productStorageViewModel.ProductId);
            var storageExistTask = new StorageClient().Exists(productStorageViewModel.StorageId);

            var productExist = await productExistTask;
            var storageExist = await storageExistTask;

            if (productExist && storageExist)
            {
                try
                {
                    repo.AddProductStorage(productStorageViewModel);
                    return new ProductStorageResultDto() { Success = true };
                }
                catch (Exception ex)
                {
                    if (ex is DbUpdateException && ex.InnerException is PostgresException &&
                        ex?.InnerException?.Message?.Contains("duplicate") == true)
                    {
                        return new ProductStorageResultDto() { Error = "Такая запись уже существует" };
                    }

                    throw;
                }
            }

            if (!productExist)
            {
                return new ProductStorageResultDto() { Error = "Продукт не найден!" };
            }

            return new ProductStorageResultDto() { Error = "Склад не найден!" };
        }

        [HttpPost(template: "removeproductstorage")]
        public ActionResult RemoveProductStorage(ProductStorageViewModel productStorageViewModel)
        {
            repo.RemoveProductStorage(productStorageViewModel.ProductId, productStorageViewModel.StorageId);
            return Ok();
        }

        [HttpPost(template: "listproducts")]
        public ActionResult<IEnumerable<int>> GetProductsIds(ProductStorageViewModel productStorageViewModel)
        {
            var products = repo.GetProductsIds(productStorageViewModel.StorageId);
            return Ok(products);
        }
    }
}