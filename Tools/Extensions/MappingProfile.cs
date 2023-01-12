using AutoMapper;
using Tools.Models;

namespace Academy.Extensions
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Customer, CustomerDTO>().ReverseMap();
        }
    }
}