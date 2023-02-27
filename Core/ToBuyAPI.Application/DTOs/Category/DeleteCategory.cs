using System.ComponentModel.DataAnnotations;

namespace ToBuyAPI.Application.DTOs.Category
{
    public class DeleteCategory
    {
		[Required]
		public string Id { get; set; }
    }
}
