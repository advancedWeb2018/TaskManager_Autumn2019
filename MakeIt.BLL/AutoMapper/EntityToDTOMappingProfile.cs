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
                 .ForMember(dest => dest.Password, a => a.MapFrom(src => src.PasswordHash));

            // TODO another maps for another issues
            // depending on the task
        }
    }
}
