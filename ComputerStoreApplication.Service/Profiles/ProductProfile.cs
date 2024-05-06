using AutoMapper;
using ComputerStoreApplication.Data.Entities;
using ComputerStoreApplication.Service.DTOs;

namespace ComputerStoreApplication.Service.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDTO>().ReverseMap();
        }
    }
}
