using AutoMapper;
using MakeIt.BLL.DTO;
using MakeIt.DAL.EF;

namespace MakeIt.BLL.AutoMapper
{
    public class DTOToEntityMappingProfile : Profile
    {
        public DTOToEntityMappingProfile()
        {
            CreateMap<UserAuthDTO, User>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.Password))
                .ForAllOtherMembers(x => x.Ignore()); 

            // TODO another maps for another issues
            // depending on the task
        }
    }
}
