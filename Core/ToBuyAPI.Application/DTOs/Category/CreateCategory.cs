using System.ComponentModel.DataAnnotations;

namespace ToBuyAPI.Application.DTOs.Category
{
    public class CreateCategory
    {
        [MinLength(2)]
        [Required]
        public string Name { get; set; }
    }
}
