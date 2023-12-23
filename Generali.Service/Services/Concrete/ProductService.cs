using AutoMapper;
using Generali.Data.UnitOfWorks;
using Generali.Entity.DTO;
using Generali.Entity.Entities;
using Generali.Service.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generali.Service.Services.Concrete
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWorks _unitOfWorks;
        private readonly IMapper _mapper;

        public int SameProductCount = 1;
        public int SameProductCount2 = 1;
        public ProductService(IUnitOfWorks unitOfWorks, IMapper mapper)
        {
            this._unitOfWorks = unitOfWorks;
            this._mapper = mapper;
        }
        public async Task<List<ProductDTO>> GetAllProductAsync()
        {
            var products = await _unitOfWorks.GetRepository<Product>().GetAllAsync(x => x.IsDeleted == false, x => x.ProductCode);

            var Same = products.GroupBy(x => new { x.ProductCode}).Select(g => new { Key = g.Key, Count = g.Count() });
            foreach (var item in Same) 
            {
                SameProductCount = Same.Where(p=>p.Key.ProductCode.Code == item.Key.ProductCode.Code).Count();
               
            }

            var map = _mapper.Map<List<ProductDTO>>(products);
            return map;
        }
        public async Task CrateProductAsync(ProductDTO productDTO)
        {
            var products = await _unitOfWorks.GetRepository<Product>().GetAllAsync(x => x.IsDeleted == false, x => x.ProductCode);
            var Same = products.GroupBy(x => new { x.ProductCode }).Select(g => new { Key = g.Key, Count = g.Count() });
            foreach (var item in Same)
            {
                SameProductCount = Same.Where(p => p.Key.ProductCode.Code == item.Key.ProductCode.Code).Count() + 1;
            }


            var _product = new Product
            {

                Name = productDTO.Name,
                Description = productDTO.Description,
                Price = productDTO.Price,
                ImageId = Guid.NewGuid(),
                ProductCodeId = Guid.NewGuid()
            };
            var _productCode = new ProductCode
            {
                Id = _product.ProductCodeId,
                Code = productDTO.Code,
                CreatedDate = DateTime.Now,
                Stock = SameProductCount,
                IsDeleted = false,

            };
            var _Image = new ProductImage
            {
                Id = _product.ImageId,
                FileName = "koltuk",
                FileType = "png",
                CreatedDate = DateTime.Now,
                IsDeleted = false,
            };

            await _unitOfWorks.GetRepository<Product>().AddAsync(_product);
            await _unitOfWorks.GetRepository<ProductCode>().AddAsync(_productCode);
            await _unitOfWorks.GetRepository<ProductImage>().AddAsync(_Image);
            await _unitOfWorks.SaveAsync();

            SameProductCount = 1;
        }
        public async Task SafeDeleteAsync(Guid productId)
        {
            var deleteproducts = await _unitOfWorks.GetRepository<Product>().GetByGuid(productId);

            await _unitOfWorks.GetRepository<Product>().DeleteAsync(deleteproducts);
            await _unitOfWorks.SaveAsync();
        }
    }
}
