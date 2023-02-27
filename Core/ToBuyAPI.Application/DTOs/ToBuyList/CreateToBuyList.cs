using System.ComponentModel.DataAnnotations;

namespace ToBuyAPI.Application.DTOs.ToBuyList
{
	public class CreateToBuyList
	{
		public string Name { get; set; }
		[MinLength(2)]
		public List<string> CategoryIds { get; set; }
		public string AppUserId { get; set; }
	}
}
