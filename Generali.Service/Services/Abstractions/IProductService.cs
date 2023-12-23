using Generali.Entity.DTO;
using Generali.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generali.Service.Services.Abstractions
{
    public interface IProductService
    {
        Task<List<ProductDTO>> GetAllProductAsync();
        Task CrateProductAsync(ProductDTO productDTO);
        Task SafeDeleteAsync(Guid productId);
    }
}
