using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Generali.Entity.DTO;
using Generali.Entity.Entities;

namespace Generali.Service.AutoMapper
{
    public class ProductProfile: Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductDTO, Product>().ReverseMap();
        }
    }
}
