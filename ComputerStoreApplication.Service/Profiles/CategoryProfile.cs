using AutoMapper;
using ComputerStoreApplication.Data.Entities;
using ComputerStoreApplication.Service.DTOs;

namespace ComputerStoreApplication.Service.Profiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryDTO>().ReverseMap();
        }
    }
}
