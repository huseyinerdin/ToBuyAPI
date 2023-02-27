using System.ComponentModel.DataAnnotations;
using ToBuyAPI.Application.DTOs.Category;

namespace ToBuyAPI.Application.DTOs.ToBuyList
{
	public class UpdateToBuyList
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public bool IsCompleted { get; set; }
		public DateTime? CompletedDate { get; set; }
		[MinLength(2)]
		public List<string> CategoryIds { get; set; }
		public string AppUserId { get; set; }

	}
}
