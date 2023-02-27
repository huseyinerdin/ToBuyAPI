using ToBuyApı.Domain.Entities;
using ToBuyAPI.Application.Repositories;
using ToBuyAPI.Persistence.Contexts;

namespace ToBuyAPI.Persistence.Repositories
{
	public class ToBuyListWriteRepository : WriteRepository<ToBuyList>, IToBuyListWriteRepository
	{
		public ToBuyListWriteRepository(ToBuyAPIDbContext context) : base(context)
		{
		}
	}
}
