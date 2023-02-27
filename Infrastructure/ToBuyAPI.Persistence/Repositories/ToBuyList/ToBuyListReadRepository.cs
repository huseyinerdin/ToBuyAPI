using ToBuyApı.Domain.Entities;
using ToBuyAPI.Application.Repositories;
using ToBuyAPI.Persistence.Contexts;

namespace ToBuyAPI.Persistence.Repositories
{
	public class ToBuyListReadRepository : ReadRepository<ToBuyList>, IToBuyListReadRepository
	{
		public ToBuyListReadRepository(ToBuyAPIDbContext context) : base(context)
		{
		}
	}
}
