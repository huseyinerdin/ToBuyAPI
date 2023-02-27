using Microsoft.AspNetCore.Identity;

namespace ToBuyApı.Domain.Entities.Identity
{
	public class AppUser : IdentityUser<string>
	{
		public string FullName { get; set; }
		public string Country { get; set; }
		public virtual ICollection<ToBuyList> ToBuyLists { get; set; }
		public AppUser()
		{
			ToBuyLists = new HashSet<ToBuyList>();
		}
	}
}
