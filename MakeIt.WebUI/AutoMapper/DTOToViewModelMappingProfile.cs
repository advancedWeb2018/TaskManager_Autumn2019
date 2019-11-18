using AutoMapper;
using MakeIt.BLL.DTO;
using MakeIt.WebUI.ViewModel.Account;

namespace MakeIt.WebUI.AutoMapper
{
    public class DTOToViewModelMappingProfile : Profile
    {
        public DTOToViewModelMappingProfile()
        {
            // create mapping           
            CreateMap<UserAuthDTO, LoginViewModel>();
            CreateMap<UserAuthDTO, RegisterViewModel>();

            // TODO another maps for another issues
            // depending on the task
        }
    }
}