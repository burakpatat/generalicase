using Generali.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generali.Entity.Entities
{
    public class Product : EntityBase, IEntityBase
    {
        public string Name { get; set; }
        public string Description { get; set; } 
        public float Price { get; set; }
        public Guid ImageId { get; set; }
        public ProductImage Image { get; set; } 
        public Guid ProductCodeId { get; set; }
        public ProductCode ProductCode { get; set; }
    }
}
