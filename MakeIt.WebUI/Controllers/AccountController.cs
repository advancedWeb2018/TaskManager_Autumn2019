using AutoMapper;
using MakeIt.BLL.DTO;
using MakeIt.BLL.Service.Authorithation;
using MakeIt.WebUI.ViewModel.Account;
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;
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
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userDTO = _mapper.Map<UserAuthDTO>(model);
                var result = (await _authorizationService.GetSignInStatus(userDTO)).Result(out bool isEmailConfirmed);

                if (!isEmailConfirmed)
                {
                    ModelState.AddModelError("Email", "Email isn`t confirmed");
                    return View(model);
                }

                switch (result)
                {
                    case SignInStatus.Success:
                        _authorizationService.ResetAccessFailedCount(userDTO);

                        if (true) // TODO check role
                            return Redirect("/Cabinet/Index");

                        //ModelState.AddModelError("Password", "Not enough access rights!");
                        //return View(model);

                    case SignInStatus.LockedOut:
                        return View(model); //TODO BlockedAccount page

                    case SignInStatus.Failure:
                        ModelState.AddModelError("Password", "Wrong password");
                        return View(model);

                    default:
                        ModelState.AddModelError("Password", "Login Failed");
                        return View(model);
                }
            }
            else
            {
                ModelState.AddModelError("Email", "Wrong LogIn");
            }
            // If we got this far, something failed, redisplay form
            return View(model);
        }
    }
}