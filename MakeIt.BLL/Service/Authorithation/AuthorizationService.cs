using AutoMapper;
using MakeIt.BLL.Common;
using MakeIt.BLL.DTO;
using MakeIt.BLL.Helper;
using MakeIt.BLL.IdentityConfig;
using MakeIt.DAL.EF;
using MakeIt.Repository.UnitOfWork;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Threading.Tasks;

namespace MakeIt.BLL.Service.Authorithation
{
    public interface IAuthorizationService : IEntityService<User>
    {
        Task<AsyncOutResult<SignInStatus, bool>> GetSignInStatus(UserAuthDTO userDto);
        void ResetAccessFailedCount(UserAuthDTO userDto);
        Task<AsyncOutResult<IdentityResult, int>> GetIdentityResult(UserAuthDTO userDto);
        Task<string> GenerateEmailConfirmationToken(int userId);
        System.Threading.Tasks.Task SendEmailConfirmationToken(int userId, string callbackUrl);
    }

    public class AuthorizationService : EntityService<User>, IAuthorizationService
    {
        private ApplicationSignInManager _signInManager; 
        private ApplicationUserManager _userManager;
        private IAuthenticationManager _authenticationManager;
        private IUnitOfWork _unitOfWork;

        public AuthorizationService(IMapper mapper, IUnitOfWork uow, ApplicationUserManager userManager, ApplicationSignInManager signInManager, IAuthenticationManager authenticationManager)
            : base(mapper, uow)
        {
            _unitOfWork = uow;
            _userManager = userManager;
            _signInManager = signInManager;
            _authenticationManager = authenticationManager;
        }

        #region Login methods
        public async Task<AsyncOutResult<SignInStatus, bool>> GetSignInStatus(UserAuthDTO userDto)
        {
            var result = SignInStatus.Failure; // start authorization result

            var user = await _userManager.FindByEmailAsync(userDto.Email);
            bool isEmailConfirmed = false;
            if (user != null)
            {
                isEmailConfirmed = await _userManager.IsEmailConfirmedAsync(user.Id);
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, change to shouldLockout: true
                result = await _signInManager.PasswordSignInAsync(user.UserName, userDto.Password, userDto.RememberMe, shouldLockout: true);
            }
            return new AsyncOutResult<SignInStatus, bool>(result, isEmailConfirmed);
        }

        public async void ResetAccessFailedCount(UserAuthDTO userDto)
        {
            var user = await _userManager.FindByEmailAsync(userDto.Email);
            if (user != null)
                await _userManager.ResetAccessFailedCountAsync(user.Id);
        }
        #endregion

        #region Register methods
        public async Task<AsyncOutResult<IdentityResult, int>> GetIdentityResult(UserAuthDTO userDto)
        {
            var user = new User
            {
                UserName = userDto.UserName,
                Email = userDto.Email
            };

            var result = await _userManager.CreateAsync(user, userDto.Password);
            int userId = user.Id;
            return new AsyncOutResult<IdentityResult, int>(result, userId);
        }

        public async Task<string> GenerateEmailConfirmationToken(int userId)
        {
            string code = string.Empty;
            code = await _userManager.GenerateEmailConfirmationTokenAsync(userId);
            return code;
        }

        public async System.Threading.Tasks.Task SendEmailConfirmationToken(int userId, string callbackUrl)
        {
            await _userManager.SendEmailAsync(userId,
                   "Email Verification",
                   "To confirm your email address, please click on the link:\n" + callbackUrl);
        }
        #endregion
    }
}
