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
            CreateMap<LoginViewModel, UserAuthDTO>();

            // TODO another maps for another issues
            // depending on the task
        }
    }
}