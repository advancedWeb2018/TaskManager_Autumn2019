using MakeIt.DAL.EF;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using System;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MakeIt.BLL.Identity
{
    public class EmailService : IIdentityMessageService
    {
        public System.Threading.Tasks.Task SendAsync(IdentityMessage message)
        {
            // Plug in your email service here to send an email.
            SmtpClient client = new SmtpClient();
            client.Port = 587;
            client.Host = "smtp.gmail.com";
            client.EnableSsl = true;
            client.Timeout = 10000;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential("umtshemet@gmail.com", "viva1313");
            return client.SendMailAsync("umtshemet@gmail.com", message.Destination, message.Subject, message.Body);
        }
    }

    // Настройка диспетчера пользователей приложения. UserManager определяется в ASP.NET Identity и используется приложением.
    public class ApplicationUserManager : UserManager<User, int>
    {
        public ApplicationUserManager(IUserStore<User, int> store)
            : base(store)
        {

        }

        public override Task<IdentityResult> CreateAsync(User user)
        {
            user.CreateDateTime = DateTime.UtcNow;
            return base.CreateAsync(user);
        }

        public override Task<IdentityResult> CreateAsync(User user, string password)
        {
            user.CreateDateTime = DateTime.UtcNow;
            return base.CreateAsync(user, password);
        }

        public override Task<IdentityResult> UpdateAsync(User user)
        {
            user.EditDateTime = DateTime.UtcNow;
            return base.UpdateAsync(user);
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {
            var manager = new ApplicationUserManager(new UserStore(new MakeItContext()));
            // Настройка логики проверки имен пользователей
            manager.UserValidator = new UserValidator<User, int>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Настройка логики проверки паролей
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 8,
                RequireNonLetterOrDigit = true,
                RequireDigit = false,
                RequireLowercase = false,
                RequireUppercase = false,
            };

            // Настройка параметров блокировки по умолчанию
            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            manager.MaxFailedAccessAttemptsBeforeLockout = 3;

            // Регистрация поставщиков двухфакторной проверки подлинности. Для получения кода проверки пользователя в данном приложении используется телефон и сообщения электронной почты
            // Здесь можно указать собственный поставщик и подключить его.
            manager.RegisterTwoFactorProvider("Код из сообщения", new EmailTokenProvider<User, int>
            {
                Subject = "Код безопасности",
                BodyFormat = "Ваш код безопасности: {0}"
            });
            manager.EmailService = new EmailService();
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider =
                       new DataProtectorTokenProvider<User, int>
                          (dataProtectionProvider.Create("ASP.NET Identity"))
                       {
                           TokenLifespan = TimeSpan.FromMinutes(15) // sets the tokens to expire in 15 minutes
                       };
            }
            return manager;
        }
        public async Task<User> GetUserNameAsync(User user)
        {
            return await Store.FindByNameAsync(user.UserName);
        }
    }

    public class ApplicationRoleManager : RoleManager<Role, int>
    {
        public ApplicationRoleManager(IRoleStore<Role, int> store) : base(store) { }

        public override Task<IdentityResult> CreateAsync(Role role)
        {
            role.CreateDateTime = DateTime.UtcNow;
            return base.CreateAsync(role);
        }

        public override Task<IdentityResult> UpdateAsync(Role role)
        {
            role.EditDateTime = DateTime.UtcNow;
            return base.UpdateAsync(role);
        }
    }
    // Настройка диспетчера входа для приложения.
    public class ApplicationSignInManager : SignInManager<User, int>
    {
        public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(User user)
        {
            return user.GenerateUserIdentityAsync((ApplicationUserManager)UserManager);
        }

        public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context)
        {
            return new ApplicationSignInManager(context.GetUserManager<ApplicationUserManager>(), context.Authentication);
        }
    }
}
