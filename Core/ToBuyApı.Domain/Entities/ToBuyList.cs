using ToBuyApı.Domain.Entities.Common;
using ToBuyApı.Domain.Entities.Identity;

namespace ToBuyApı.Domain.Entities
{
    public class ToBuyList : BaseEntity
    {
        public string Name { get; set; }
        public DateTime? CompletedDate { get; set; }
        public virtual ICollection<Category> Categories { get; set; }
        public string AppUserId { get; set; }
        public virtual AppUser AppUser { get; set; }
        public ToBuyList()
        {
			Categories = new HashSet<Category>();
        }
    }
}
