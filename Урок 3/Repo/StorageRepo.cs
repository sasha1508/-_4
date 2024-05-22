namespace MarketQL.Repo;

using AutoMapper;
using MarketQL.Abstractions;

using MarketQL.DB;
using MarketQL.DTO;
using MarketQL.Model;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;

public class StorageRepo : IStorageRepo
{
    private readonly IMapper _mapper;
    private IMemoryCache _memoryCache;

    public StorageRepo(IMapper mapper, IMemoryCache memoryCache)
    {
        _mapper = mapper;
        _memoryCache = memoryCache;
    }

    int IStorageRepo.AddStorage(StorageViewModel storageViewModel)
    {

        using (var context = new ProductContext())
        {

            var entityStorage = context.Storages.FirstOrDefault(x => x.Name.ToLower().Equals(storageViewModel.Name.ToLower()));
            if (entityStorage == null)
            {
                var entity = _mapper.Map<Storage>(storageViewModel);
                context.Storages.Add(entity);
                context.SaveChanges();
                _memoryCache.Remove("storages");
                storageViewModel.Id = entity.Id;
            }
            else
            {
                throw new Exception("Такой склад уже создан");
            }
        }
        return storageViewModel.Id; 
    }

    IEnumerable<StorageViewModel> IStorageRepo.GetStorage()
    {
        throw new NotImplementedException();
    }
}

