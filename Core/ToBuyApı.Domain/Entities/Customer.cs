using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToBuyApı.Domain.Entities.Common;

namespace ToBuyApı.Domain.Entities
{
    public class Customer : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<ToBuyList> ToBuyLists { get; set; }
        public Customer()
        {
            ToBuyLists= new HashSet<ToBuyList>();
        }
    }
}
