using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToBuyApı.Domain.Entities.Common;

namespace ToBuyApı.Domain.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<ProductImageFile> ProductImageFiles { get; set; }
        public virtual ICollection<Category> Categories { get; set; }
        public Product()
        {
            Categories= new HashSet<Category>();
            ProductImageFiles= new HashSet<ProductImageFile>();
        }
    }
}
