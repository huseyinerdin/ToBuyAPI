using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ToBuyApı.Domain.Entities.Common;
using ToBuyAPI.Application.Repositories;
using ToBuyAPI.Persistence.Contexts;

namespace ToBuyAPI.Persistence.Repositories
{
    public class WriteRepository<T> : IWriteRepository<T> where T : BaseEntity
    {
        private readonly ToBuyAPIDbContext _context;

        public WriteRepository(ToBuyAPIDbContext context)
        {
            _context = context;
        }

        public DbSet<T> Table => _context.Set<T>();

        public async Task<bool> AddAsync(T model)
        {
            EntityEntry<T> entityEntry = await Table.AddAsync(model);
            return entityEntry.State == EntityState.Added;
        }

        public async Task<bool> AddRangeAsync(List<T> models)
        {
            foreach (T model in models)
            {
                bool result = await AddAsync(model);
                if(!result) return false;
            }
            return true;
        }

        public bool Remove(T model)
        {
            EntityEntry<T> entityEntry = Table.Remove(model);
            return entityEntry.State == EntityState.Deleted;
        }

        public async Task<bool> RemoveAsync(string id)
        {
            T model = await Table.FirstOrDefaultAsync(model => model.Id == Guid.Parse(id));
            return Remove(model);
        }

        public bool Remove(List<T> models)
        {
            Table.RemoveRange(models);
            foreach (T model in models)
            {
                bool result = Remove(model);
                if(!result) return false;
            }
            return true;
        }

        public bool Update(T model)
        {
            EntityEntry<T> entityEmtry = Table.Update(model);
            return entityEmtry.State == EntityState.Modified;
        }

        public async Task<int> SaveAsync() => await _context.SaveChangesAsync();
    }
}
