using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToBuyAPI.Application.DTOs.Category
{
    public class DeleteCategory
    {
		[Required]
		public string Id { get; set; }
    }
}
