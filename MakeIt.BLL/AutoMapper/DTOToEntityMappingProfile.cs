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
                .ForMember(dest => dest.PasswordHash, a => a.MapFrom(src => src.Password))
                .ForMember(x => x.AssignedTasks, opt => opt.Ignore())
                .ForMember(x => x.CreatedTasks, opt => opt.Ignore())
                .ForMember(x => x.AccessFailedCount, opt => opt.Ignore())
                .ForMember(x => x.Claims, opt => opt.Ignore())
                .ForMember(x => x.EmailConfirmed, opt => opt.Ignore())
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ForMember(x => x.IsFired, opt => opt.Ignore())
                .ForMember(x => x.LockoutEnabled, opt => opt.Ignore())
                .ForMember(x => x.LockoutEndDateUtc, opt => opt.Ignore())
                .ForMember(x => x.Logins, opt => opt.Ignore())
                .ForMember(x => x.PhoneNumber, opt => opt.Ignore())
                .ForMember(x => x.PhoneNumberConfirmed, opt => opt.Ignore())
                .ForMember(x => x.Roles, opt => opt.Ignore())
                .ForMember(x => x.SecurityStamp, opt => opt.Ignore())
                .ForMember(x => x.TwoFactorEnabled, opt => opt.Ignore())
                .ForMember(x => x.UserName, opt => opt.Ignore());

            // TODO another maps for another issues
            // depending on the task
        }
    }
}
