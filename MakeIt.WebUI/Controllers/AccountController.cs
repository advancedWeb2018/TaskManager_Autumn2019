using AutoMapper;
using MakeIt.BLL.DTO;
using MakeIt.BLL.Service.Authorithation;
using MakeIt.WebUI.ViewModel.Account;
using System.Web.Mvc;

namespace MakeIt.WebUI.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IAuthorizationService _authorizationService;
        public AccountController(IAuthorizationService authorizationService, IMapper mapper) : base(mapper)
        {
            _authorizationService = authorizationService;
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            var modelDTO = _mapper.Map<UserAuthDTO>(model);

            var test = _authorizationService.MappingTest(modelDTO);

            var reverse = _mapper.Map<LoginViewModel>(test);

            return View();
        }
    }
}