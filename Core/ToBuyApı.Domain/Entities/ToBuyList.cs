using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using ToBuyApı.Domain.Entities.Common;

namespace ToBuyApı.Domain.Entities
{
    public class ToBuyList : BaseEntity
    {
        public string Name { get; set; }
        public DateTime CompletedDate { get; set; }
        public Guid ProductId { get; set; }
        public ICollection<Product> Products { get; set; }
        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; }
        public ToBuyList()
        {
            Products= new HashSet<Product>();
        }
    }
}
