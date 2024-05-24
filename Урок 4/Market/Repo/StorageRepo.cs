using AutoMapper;
using Market.Abstraction;
using Market.DB;
using Market.DTO;
using Market.Models;
using Microsoft.Extensions.Caching.Memory;

namespace Market.Repo
{
    public class StorageRepo(IMapper mapper, IMemoryCache memoryCache, ProductContext context) : IStorageRepo
    {
        public void AddStorage(StorageViewModel storageViewModel)
        {
            var entityStorage = context.Storages.FirstOrDefault(x =>
                x.Name.ToLower().Equals(storageViewModel.Name.ToLower()));
            if (entityStorage == null)
            {
                var entity = mapper.Map<Storage>(storageViewModel);
                context.Storages.Add(entity);
                context.SaveChanges();
                memoryCache.Remove("storages");
            }
            else
            {
                throw new Exception("There is storage's dublicate in database");
            }
        }

        public IEnumerable<StorageViewModel> GetStorages()
        {
            if (memoryCache.TryGetValue("storages", out List<StorageViewModel> storagesCashe))
            {
                return storagesCashe;
            }

            var storages = context.Storages.Select(mapper.Map<StorageViewModel>).ToList();
            memoryCache.Set("storages", storages, TimeSpan.FromMinutes(30));
            return storages;
        }

        public bool CheckStorage(int storageId)
        {
            return context.Storages.Any(x => x.Id == storageId);
        }
    }
}
