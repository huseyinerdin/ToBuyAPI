using System.ComponentModel.DataAnnotations;

namespace ToBuyAPI.Application.DTOs.Category
{
	public class UpdateCategory
    {
		[Required]
		public string Id { get; set; }
        [MinLength(2)]
		[Required]
		public string Name { get; set; }
    }
}
