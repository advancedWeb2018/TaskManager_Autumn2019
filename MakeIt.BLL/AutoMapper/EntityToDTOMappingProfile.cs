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
                 .ForMember(dest => dest.Password, a => a.MapFrom(src => src.PasswordHash))
                 .ForMember(x => x.RememberMe, opt => opt.Ignore())
                 .ForMember(x => x.ConfirmPassword, opt => opt.Ignore());

            // TODO another maps for another issues
            // depending on the task
        }
    }
}
