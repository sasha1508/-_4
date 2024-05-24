using AutoMapper;
using StorageGraphQL.Abstraction;
using StorageGraphQL.DB;
using StorageGraphQL.DTO;
using StorageGraphQL.Models;

namespace StorageGraphQL.Repo;

public class ProductStorageRepo : IProductStorageRepo
{
    private IMapper _mapper;
    private ProductStorageContext _context;

    public ProductStorageRepo(IMapper mapper, ProductStorageContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public int? AddProductStorage(ProductStorageViewModel productStorageViewModel)
    {
        var entity = _mapper.Map<ProductStorage>(productStorageViewModel);
        _context.Add(entity);
        _context.SaveChanges();
        return entity.ProductId;
    }

    public IEnumerable<int?> GetProductsIds(int? storageId)
    {
        return _context.ProductStorages.Where(x => x.StorageId == storageId).Select(x => x.ProductId).ToList();
    }
    public IEnumerable<ProductStorageViewModel> GetProducts()
    {
        return _context.ProductStorages.Select(_mapper.Map<ProductStorageViewModel>).ToList();
    }
    public void RemoveProductStorage(int? productId, int? storageId)
    {
        var productStorage = _context.ProductStorages.First(x => x.ProductId == productId && x.StorageId == storageId);
        _context.ProductStorages.Remove(productStorage);
        _context.SaveChanges();
    }
}