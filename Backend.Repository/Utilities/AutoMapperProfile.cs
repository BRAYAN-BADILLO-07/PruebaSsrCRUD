using AutoMapper;
using Backend.Domain.DTOs;
using Backend.Domain.Models;

namespace Backend.Repository.Utilities
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            #region City
            CreateMap<City, CityDTO>().ReverseMap();
            #endregion

            #region Seller
            CreateMap<Seller, SellerDTO>()
                .ForMember(d => d.NameCity, opt => opt.MapFrom(o => o.City.Description));

            CreateMap<SellerDTO, Seller>()
                .ForMember(d => d.City, opt => opt.Ignore());
            #endregion
        }
    }
}
