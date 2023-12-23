using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generali.Entity.DTO
{
    public class ProductCodeDTO
    {
        public string Code { get; set; }
        public int Stock { get; set; }
        public DateTime? CreatedDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
