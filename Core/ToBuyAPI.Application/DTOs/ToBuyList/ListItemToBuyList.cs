using ToBuyAPI.Application.DTOs.Category;

namespace ToBuyAPI.Application.DTOs.ToBuyList
{
	public class ListItemToBuyList
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public DateTime? CompletedDate { get; set; }
		public List<ListItemCategory> Categories { get; set; }
		public string AppUserId { get; set; }
	}
}
