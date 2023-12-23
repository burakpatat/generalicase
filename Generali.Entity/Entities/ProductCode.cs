using Generali.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generali.Entity.Entities
{
    public class ProductCode : EntityBase, IEntityBase
    {
        public string Code { get; set; }
        public int Stock { get; set; }
        public ICollection<Product> Products { get; set;}
    }
}
