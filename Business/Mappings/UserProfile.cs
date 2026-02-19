using AutoMapper;
using PruebaTecnica.Business.DataContracts.Requests;
using PruebaTecnica.Business.DataContracts.Responses;
using PruebaTecnica.BDO.Entities;

namespace PruebaTecnica.Business.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile() {
            CreateMap<AddUserContract, User>()
                .ForMember(dest => dest.ID, opt => opt.Ignore())
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src.BirthDate))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender));

            CreateMap<UpdateUserContract, User>()
                .ForMember(dest => dest.ID, opt => opt.Ignore())
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src.BirthDate))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender));

            CreateMap<User, UserDto>()
                .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src.BirthDate))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender));
        }
    }
}