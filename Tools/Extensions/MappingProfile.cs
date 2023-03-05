using AutoMapper;
using Tools.Models;

namespace Academy.Extensions
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Customer, CustomerDTO>().ReverseMap().ForMember(member => member.CustPic, option => option.Condition(x => x.CustPic.Keys.Count() == 20));
            CreateMap<City, CityDTO>().ReverseMap();
        }
    }
}