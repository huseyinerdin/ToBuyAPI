using Microsoft.EntityFrameworkCore;
using ToBuyApı.Domain.Entities.Common;

namespace ToBuyAPI.Application.Repositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        DbSet<T> Table { get; }
    }
}
