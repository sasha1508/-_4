using AutoMapper;
using Castle.Components.DictionaryAdapter.Xml;
using Microsoft.Extensions.Caching.Memory;
using Storage.Abstraction;
using Storage.DB;
using Storage.DTO;
using Storage.Models;

namespace Storage.Repo;

public class ProductStorageRepo : IProductStorageRepo
{
    private IMapper _mapper;
    private ProductStorageContext _context;

    public ProductStorageRepo(IMapper mapper, ProductStorageContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public void AddProductStorage(ProductStorageViewModel productStorageViewModel)
    {
        _context.Add(_mapper.Map<ProductStorage>(productStorageViewModel));
        _context.SaveChanges();
    }

    public IEnumerable<int?> GetProductsIds(int? storageId)
    {
        return _context.ProductStorages.Where(x => x.StorageId == storageId).Select(x => x.ProductId).ToList();
    }

    public void RemoveProductStorage(int? productId, int? storageId)
    {
        var productStorage = _context.ProductStorages.First(x => x.ProductId == productId && x.StorageId == storageId);
        _context.ProductStorages.Remove(productStorage);
        _context.SaveChanges();
    }
}