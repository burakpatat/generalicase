using AutoMapper;
using Generali.Entity.DTO;
using Generali.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generali.Service.AutoMapper
{
    public class ProductCodeProfile: Profile
    {
        public ProductCodeProfile() 
        {
            CreateMap<ProductCodeDTO, ProductCode>().ReverseMap();
        }
    }
}
