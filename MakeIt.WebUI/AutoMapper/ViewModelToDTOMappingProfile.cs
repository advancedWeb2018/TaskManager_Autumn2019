using AutoMapper;
using MakeIt.BLL.DTO;
using MakeIt.WebUI.ViewModel.Account;

namespace MakeIt.WebUI.AutoMapper
{
    public class ViewModelToDTOMappingProfile : Profile
    {
        public ViewModelToDTOMappingProfile()
        {
            // create mapping           
            CreateMap<LoginViewModel, UserAuthDTO>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password))
                .ForMember(dest => dest.RememberMe, opt => opt.MapFrom(src => src.RememberMe))
                .ForAllOtherMembers(x => x.Ignore()); 

            CreateMap<RegisterViewModel, UserAuthDTO>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password))
                .ForMember(dest => dest.ConfirmPassword, opt => opt.MapFrom(src => src.ConfirmPassword))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
                .ForAllOtherMembers(x => x.Ignore()); ;

            // TODO another maps for another issues
            // depending on the task
        }
    }
}