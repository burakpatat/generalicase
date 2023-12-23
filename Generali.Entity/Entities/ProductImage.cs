using Generali.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generali.Entity.Entities
{
    public class ProductImage: EntityBase, IEntityBase
    {
        public string FileName { get; set; }   
        public string FileType { get; set; }
        public ICollection<Product> Products { get; set; }   
    }
}
