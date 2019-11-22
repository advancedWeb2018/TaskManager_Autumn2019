using AutoMapper;
using MakeIt.BLL.DTO;
using MakeIt.DAL.EF;

namespace MakeIt.BLL.AutoMapper
{
    public class EntityToDTOMappingProfile : Profile
    {
        public EntityToDTOMappingProfile()
        {
            CreateMap<User, UserAuthDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.PasswordHash))
                .ForAllOtherMembers(x => x.Ignore()); 

            // TODO another maps for another issues
            // depending on the task
        }
    }
}
