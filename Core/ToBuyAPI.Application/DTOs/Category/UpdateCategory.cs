using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToBuyAPI.Application.DTOs.Product;

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
