using Market.Abstraction;
using Market.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Market.Controllers;

[ApiController]
[Route("[controller]")]
public class StorageController(IStorageRepo repo) : Controller
{
    private IStorageRepo _repo = repo;
    [HttpPost(template: "addstorage")]
    public ActionResult AddStorage(StorageViewModel storageViewModel)
    {
        try
        {
            _repo.AddStorage(storageViewModel);

            return Ok();
        }
        catch
        {
            return StatusCode(409);
        }
    }
    [HttpGet(template: "getstorages")]
    public ActionResult<IEnumerable<StorageViewModel>> GetStorages()
    {
        try
        {
            return Ok(repo.GetStorages());
        }
        catch
        {
            return StatusCode(500);
        }
    }

    [HttpGet(template: "checkstorage")]
    public ActionResult<bool> CheckStorage(int storageId)
    {
        return Ok(_repo.CheckStorage(storageId));
    }
}