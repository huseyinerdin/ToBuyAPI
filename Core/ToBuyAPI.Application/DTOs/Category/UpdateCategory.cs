using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToBuyAPI.Application.DTOs.Product;

namespace ToBuyAPI.Application.DTOs.Category
{
    public class UpdateCategory
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
