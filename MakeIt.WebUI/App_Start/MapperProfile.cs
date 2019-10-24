using AutoMapper;
using MakeIt.BLL.DTO;
using MakeIt.WebUI.ViewModel.Account;

namespace MakeIt.WebUI.App_Start
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            // create mapping           
            CreateMap<UserAuthDTO, LoginViewModel>()
            .ReverseMap(); // if necessary both ways
        }
    }
}