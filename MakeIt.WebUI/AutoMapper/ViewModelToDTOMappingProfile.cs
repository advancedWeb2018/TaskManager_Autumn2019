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
                 .ForMember(x => x.UserName, opt => opt.Ignore())
                 .ForMember(x => x.ConfirmPassword, opt => opt.Ignore());
            CreateMap<RegisterViewModel, UserAuthDTO>()
                 .ForMember(x => x.RememberMe, opt => opt.Ignore())
                 .ForMember(x => x.ConfirmPassword, opt => opt.Ignore());

            // TODO another maps for another issues
            // depending on the task
        }
    }
}