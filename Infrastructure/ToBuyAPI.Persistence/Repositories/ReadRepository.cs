using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using ToBuyApı.Domain.Entities.Common;
using ToBuyAPI.Application.Repositories;
using ToBuyAPI.Persistence.Contexts;

namespace ToBuyAPI.Persistence.Repositories
{
    public class ReadRepository<T> : IReadRepository<T> where T : BaseEntity
    {
        private readonly ToBuyAPIDbContext _context;

        public ReadRepository(ToBuyAPIDbContext context)
        {
            _context = context;
        }

        public DbSet<T> Table => _context.Set<T>();

        public IQueryable<T> GetAll(bool isTrack = true)
        {
            var query = Table.AsQueryable();
            if (!isTrack)
                query = query.AsNoTracking();
            return query;
        }

        public IQueryable<T> GetWhere(Expression<Func<T, bool>> expression, bool isTrack = true)
        {
            var query = Table.Where(expression);
            if (!isTrack)
                query = query.AsNoTracking();
            return query;
        }

        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> expression, bool isTrack = true)
        {
            var query = Table.AsQueryable();
            if (!isTrack)
                query = query.AsNoTracking();
            return await query.FirstOrDefaultAsync(expression);
        }

        public async Task<T> GetByIdAsync(string id, bool isTrack = true)
        {
            var query = Table.AsQueryable();
            if (!isTrack)
                query = query.AsNoTracking();
            return await query.FirstOrDefaultAsync(data => data.Id == Guid.Parse(id));
        }
    }
}
