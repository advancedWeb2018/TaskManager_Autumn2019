using AutoMapper;
using MakeIt.BLL.DTO;
using MakeIt.BLL.Service.Authorithation;
using MakeIt.WebUI.ReCaptchaV3;
using MakeIt.WebUI.ViewModel.Account;
using Microsoft.AspNet.Identity.Owin;
using System.Configuration;
using System.Linq;
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
        public async Task<ActionResult> Login()
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

        // GET: /Account/Register
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> Register(string returnUrl)
        {
            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Cabinet");
            }
            return View(new RegisterViewModel(ConfigurationManager.AppSettings["RecaptchaPublicKey"]));
        }

        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateRecaptcha]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var userDTO = _mapper.Map<UserAuthDTO>(model);
                var result = (await _authorizationService.GetIdentityResult(userDTO)).Result(out int userId);

                if (result.Succeeded)
                {
                    // TODO add to role

                    var code = await _authorizationService.GenerateEmailConfirmationToken(userId);

                    var callbackUrl = Url.Action("ConfirmUserEmail", "Account",
                                      new { userId = userId, code = code }, protocol: Request.Url.Scheme);


                    await _authorizationService.SendEmailConfirmationToken(userId, callbackUrl);

                    return RedirectToAction("ConfirmEmail", "Account", new { email = model.Email });
                }
                ModelState.AddModelError("Email", result.Errors.ElementAt(0));
            }
            return View(new RegisterViewModel(ConfigurationManager.AppSettings["RecaptchaPublicKey"])); ;
        }

        // GET: /Account/ConfirmEmail
        [AllowAnonymous]
        public ActionResult ConfirmEmail(string email)
        {
            ViewBag.Email = email;
            return View();
        }

        [AllowAnonymous]
        public async Task<ActionResult> ConfirmUserEmail(string userId, string code)
        {
           // TODO
            return View();
        }

        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }
    }
}