using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToBuyApı.Domain.Entities.Common;

namespace ToBuyApı.Domain.Entities
{
    public class Category :BaseEntity
    {
        public string Name { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<ToBuyList> ToBuyLists { get; set; }
        public Category()
        {
            Products= new HashSet<Product>();
            ToBuyLists = new HashSet<ToBuyList>();
        }
    }
}
