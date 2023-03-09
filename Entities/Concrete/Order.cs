using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Order:IEntity
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public ICollection<Product> Products { get; set; }

        public Customer Customer { get; set; }
    }
}
