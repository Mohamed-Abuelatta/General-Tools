using AutoMapper;
using Tools.Models;

namespace Academy.Extensions
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Customer, CustomerDTO>().ReverseMap().ForMember(member => member.CustPic = member.CustPic);
            CreateMap<City, CityDTO>().ReverseMap();
        }
    }
}