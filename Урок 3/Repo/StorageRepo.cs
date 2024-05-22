using AutoMapper;
using MarketQL.Abstractions;

using MarketQL.DB;
using MarketQL.DTO;
using MarketQL.Model;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;

namespace MarketQL.Repo
{


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

        public IEnumerable<StorageViewModel> GetStorages()
        {
            if (_memoryCache.TryGetValue("storages", out List<StorageViewModel>? storagesCache))
            {
                return storagesCache ?? new List<StorageViewModel>();
            }
            using (var context = new ProductContext())
            {
                var storages = context.Storages.Select(_mapper.Map<StorageViewModel>).ToList();
                _memoryCache.Set("storages", storages, TimeSpan.FromMinutes(30));
                return storages ?? new List<StorageViewModel>();
            }
        }
    }
}

