using SGE.Cagravol.Domain.POCO.ServiceModels.Identity;
using SGE.Cagravol.Domain.Entities.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.DataProtection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity.EntityFramework;
using SGE.Cagravol.Infrastructure.Utils;
using SGE.Cagravol.Infrastructure.Utils.Definitions;
using SGE.Cagravol.Presentation.Resources.Common;
using SGE.Cagravol.Domain.Repositories.Customers;
using SGE.Cagravol.Application.ServiceModel;

namespace SGE.Cagravol.Application.Services.Identity
{
    public sealed class AccountService : IAccountService, IDisposable
    {
        private readonly IAuthenticationManager authenticationManager;
        private readonly UserManager<User, string> userManager;
        private readonly RoleManager<IdentityRole, string> roleManager;
        private readonly ICustomerRepository customerRepository;
        //private User currentUser = null;

        public AccountService(IAuthenticationManager authenticationManager,
            UserManager<User, string> userManager,
            RoleManager<IdentityRole, string> roleManager,
            ICustomerRepository customerRepository)
        {
            Check.NotNull(authenticationManager, "authenticationManager");
            Check.NotNull<UserManager<User, string>>(userManager, "userManager");
            Check.NotNull(roleManager, "roleManager");

            this.authenticationManager = authenticationManager;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.customerRepository = customerRepository;
        }

        public IDataProtectionProvider DataProtectionProvider
        {
            set
            {
                if (value != null)
                {
                    IDataProtector dataProtector = value.Create("ASP.NET Identity");
                    this.userManager.UserTokenProvider = new DataProtectorTokenProvider<User>(dataProtector);
                }
            }
        }

        public bool IsAuthenticated
        {
            get
            {
                return HttpContext.Current.User.Identity.IsAuthenticated;
            }
        }

        public string UserName
        {
            get { return HttpContext.Current.User.Identity.Name; }
        }
        //public string UserName
        //{
        //	get 
        //	{ 

        //		return HttpContext.Current.User; 
        //	}
        //}

        public bool IsAdmin
        {
            get
            {
                return this.IsUserAdmin(this.UserName);
            }
        }

        public string Id
        {
            get
            {
                var result = "";
                if (this.IsAuthenticated)
                {
                    result = HttpContext.Current.User.Identity.GetUserId();
                }
                return result;
            }
        }

        public long CustomerId
        {
            get
            {
                long result = -1;
                if (this.IsAuthenticated)
                {
                    var user = this.userManager.FindById(this.Id);
                    if (user != null)
                    {
                        if (user.Customers.Any())
                        {
                            result = user.Customers.FirstOrDefault().Id;
                        }
                        else
                        {
                            result = -1;
                        }
                    }

                }
                return result;
            }
        }

        public string Name
        {
            get
            {
                string result = "--";
                if (this.IsAuthenticated)
                {
                    var user = this.userManager.FindById(this.Id);
                    if (user != null)
                    {
                        result = user.Name;
                    }

                }
                return result;
            }
        }

        public string NameSurname
        {
            get
            {
                string result = "--";
                if (this.IsAuthenticated)
                {
                    var user = this.userManager.FindById(this.Id);
                    if (user != null)
                    {
                        result = string.Format("{0} {1}", user.Name, user.Surname).Trim();
                    }

                }
                return result;
            }
        }

        public bool IsUserAdmin(string userName)
        {
            return this.IsUserInRole(userName, RoleDefinitions.Administrator);
        }

        public bool IsUserInRole(string username, string rolename)
        {

            var userLoginName = username.CleanLoginEmail();

            var userId = this.userManager.FindByName(userLoginName).Id;
            var result = this.userManager.IsInRole(userId, rolename);

            return result;
        }
        public bool IsUserInRoleById(string userId, string rolename)
        {
            return this.userManager.IsInRole(userId, rolename);
        }        

        public async Task<UserLoginResultServiceModel> Login(string userEmail, string userPassword, bool isPersistent)
        {
            var result = new UserLoginResultServiceModel(false, ErrorResources.UserOrEmailNorValid);

            if (!string.IsNullOrWhiteSpace(userEmail)
                && !string.IsNullOrWhiteSpace(userPassword))
            {
                var user = await this.userManager.FindAsync(userEmail.CleanLoginEmail(), userPassword);

                if (user != null)
                {
                    if (!user.IsActive)
                    {
                        result.FailMessage = ErrorResources.UserNoActiveAccount;
                    }
                    else if (!user.EmailConfirmed)
                    {
                        result.FailMessage = string.Format(ErrorResources.UserEmailNotConfirmed, "/Account/ReSendConfirmationEmail?userEmail=" + userEmail);
                    }
                    else
                    {
                        this.SignOut();
                        user.LastAccess = DateTime.UtcNow;
                        await this.SignIn(user, isPersistent);
                        await this.userManager.UpdateAsync(user);
                        result = new UserLoginResultServiceModel(true, "", user);
                    }
                }
            }

            return result;
        }

        public async Task<UserLoginResultServiceModel> LoginAfterConfirmEmail(User user)
        {
            var result = new UserLoginResultServiceModel(false, ErrorResources.UserOrEmailNorValid);

            if (user != null)
            {
                this.SignOut();
                if (!user.IsActive)
                {
                    result.FailMessage = ErrorResources.UserNoActiveAccount;
                }
                else
                {
                    user.LastAccess = DateTime.UtcNow;
                    user.EmailConfirmed = true;
                    await this.SignIn(user, true);
                    await this.userManager.UpdateAsync(user);
                    result = new UserLoginResultServiceModel(true, "", user);
                }
            }

            return result;
        }

        public void SignOut()
        {
            this.authenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
        }

        public async Task SignIn(User user, bool isPersistent)
        {
            var identity = await userManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            this.authenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, identity);
        }

        public async Task<bool> AddToRole(string userId, string roleName)
        {
            var result = await this.userManager
                .AddToRoleAsync(userId, roleName);

            return result.Succeeded;
        }

        public async Task<bool> RemoveFromRole(string userId, string roleName)
        {
            var result = await this.userManager
                .RemoveFromRoleAsync(userId, roleName);

            return result.Succeeded;
        }


        public async Task<UserCreationResultServiceModel> CreateUserAsync(User user, string password)
        {

            User existingUser = await this.FindUserByUserNameOrEmailAsync(user.UserName.CleanLoginEmail());
            UserCreationResultServiceModel result = new UserCreationResultServiceModel(ErrorResources.ErrorOnUserCreation);

            if (existingUser == null)
            {
                var resuser = await this.userManager.CreateAsync(user, password);

                if (resuser.Succeeded)
                {
                    var created = this.userManager.FindByName(user.UserName.CleanLoginEmail());
                    result = new UserCreationResultServiceModel(created);
                }
                else
                {
                    result = new UserCreationResultServiceModel(string.Join(", ", resuser.Errors.Select(s => "\n " + s)));
                }
            }
            else
            {
                result = new UserCreationResultServiceModel(ErrorResources.ErrorOnUserCreation_UserAlreadyExist);
            }
            return result;
        }

        public async Task<UserCreationResultServiceModel> CreateUserWithMainRoleAsync(User user, string password, string mainRoleName = "")
        {
            User existingUser = await this.FindUserByUserNameOrEmailAsync(user.UserName.CleanLoginEmail());
            UserCreationResultServiceModel result = new UserCreationResultServiceModel(ErrorResources.ErrorOnUserCreation);

            try
            {
                if (existingUser == null)
                {
                    var resuser = await this.userManager.CreateAsync(user, password);

                    if (resuser.Succeeded)
                    {
                        if (!string.IsNullOrWhiteSpace(mainRoleName))
                        {
                            this.userManager.AddToRole(user.Id, mainRoleName);
                        }

                        var created = this.userManager.FindByName(user.UserName.CleanLoginEmail());
                        result = new UserCreationResultServiceModel(created);
                    }
                    else
                    {
                        result = new UserCreationResultServiceModel(string.Join(", ", resuser.Errors.Select(s => "\n " + s)));
                    }
                }
                else
                {
                    result = new UserCreationResultServiceModel(ErrorResources.ErrorOnUserCreation_UserAlreadyExist);
                }
            }
            catch (Exception ex)
            {
                result.Succeeded = false;
                result.FailMessage = ex.Message;
            }

            return result;
        }

        public async Task<IdentityResult> DeleteUserAsync(User user)
        {
            return await this.userManager.DeleteAsync(user);
        }
        public async Task<bool> Remove(User user)
        {
            var result = await this.userManager.DeleteAsync(user);
            return result.Succeeded;
        }

        public Task<User> FindUserByIdAsync(string userId)
        {
            return this.userManager.FindByIdAsync(userId.CleanLoginEmail());
        }

        public Task<User> CheckUserPassword(string userEmail, string password)
        {
            return this.userManager.FindAsync(userEmail.CleanLoginEmail(), password);
        }

        public Task<User> FindUserByUserNameAsync(string userEmail)
        {

            Task<User> result = null;
            if (!string.IsNullOrWhiteSpace(userEmail))
            {
                result = this.userManager.FindByEmailAsync(userEmail);                                
            }
            return result;
        }

        public async Task<User> FindUserByUserNameOrEmailAsync(string userEmail)
        {

            User user = null;
            
            if (!string.IsNullOrWhiteSpace(userEmail))
            {
                user = await this.userManager.FindByNameAsync(userEmail);
                if (user == null)
                {
                    user = await this.userManager.FindByEmailAsync(userEmail);                
                }
            }
            return user;
        }

        public async Task<User> GetCurrentUserAsync()
        {
            Task<User> result = null;

            if (this.IsAuthenticated)
            {
                result = this.userManager.FindByIdAsync(HttpContext.Current.User.Identity.GetUserId());
            }
            return await result;
        }


        public async Task<bool> ConfirmEmail(User user, string token)
        {
            var result = await this.userManager.UserTokenProvider.ValidateAsync("EmailConfirmation", token, this.userManager, user);
            return result;
        }
        public async Task<bool> ConfirmResetToken(User user, string token)
        {
            var result = await this.userManager.UserTokenProvider.ValidateAsync("Changepass", token, this.userManager, user);
            return result;
        }

        public async Task<string> GenerateEmailConfirmationTokenAsync(User user)
        {
            var isvalid = await this.userManager.UserTokenProvider.IsValidProviderForUserAsync(this.userManager, user);
            var working = await this.userManager.UserTokenProvider.GenerateAsync("EmailConfirmation", this.userManager, user);
            return HttpUtility.UrlEncode(working);
        }
        public async Task<string> GenerateChangePassTokenAsync(User user)
        {
            var isvalid = await this.userManager.UserTokenProvider.IsValidProviderForUserAsync(this.userManager, user);
            var working = await this.userManager.UserTokenProvider.GenerateAsync("Changepass", this.userManager, user);
            return HttpUtility.UrlEncode(working);
        }

        public async Task<IdentityResult> ResetPasswordAsync(string userName, string password)
        {
            var PassResetToken = await this.GeneratePasswordResetTokenAsync(userName.CleanLoginEmail());
            var user = this.userManager.FindByName(userName.CleanLoginEmail());
            return await this.userManager.ResetPasswordAsync(user.Id, PassResetToken, password);
        }

        public async Task<IdentityResult> ResetPasswordAsync(User user, string code, string password)
        {
            return await this.userManager.ResetPasswordAsync(user.Id, code, password);
        }

        public IEnumerable<IdentityRole> GetAllRoles()
        {
            return this.roleManager.Roles;
        }

        public async Task<bool> RemoveUsersByCustomer(long customerId)
        {
            try
            {


                var list = this.userManager.Users.Where(w => w.Customers.Select(s => s.Id).Contains(customerId)).ToList();
                foreach (var u in list)
                {
                    await this.DeleteUserAsync(u);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IResultModel> RemoveListOfUsersByProject(IEnumerable<string> userIdList)
        {
            IResultModel rm = new ResultModel();

            try
            {
                var list = this.userManager.Users.Where(w => userIdList.Contains(w.Id)).ToList();
                foreach (var u in list)
                {
                    await this.DeleteUserAsync(u);
                }
                rm.OnSuccess();
            }
            catch (Exception ex)
            {
                rm.OnException(ex);
            }

            return rm;
        }



        public IEnumerable<User> GetUsersByCustomer(long customerId, bool orderByCreationDate = true)
        {
            try
            {

                var list = this.userManager
                    .Users
                    .Where(w => w.Customers.Select(s => s.Id).Contains(customerId));


                if (orderByCreationDate)
                {
                    return list
                    .OrderBy(o => o.CreatedOn)
                    .ToList();
                }
                else
                {
                    return list
                        .OrderByDescending(o => o.CreatedOn)
                        .ToList();
                }

            }
            catch (Exception)
            {
                return Enumerable.Empty<User>();
            }
        }

        public IEnumerable<User> GetHomeUsers()
        {

            IEnumerable<User> list = Enumerable.Empty<User>();

            try
            {

                var orgRole = this.roleManager.FindByName(RoleDefinitions.Organizer);
                if (orgRole == null)
                {
                    return list;
                }

                list = this.userManager
                    .Users
                    .Where(w => w.Roles.Where(ww => ww.RoleId == orgRole.Id).Any());

                return list
                .OrderBy(o => o.CreatedOn)
                .ToList();

            }
            catch (Exception)
            {
                return Enumerable.Empty<User>();
            }
        }

        public IEnumerable<string> GetRolesForUser(string userId)
        {
            var result = userManager.GetRoles(userId);

            return result;
        }


        public string GetRolesKeyFlagsForUser(string userId)
        {
            IEnumerable<string> roles = userManager.GetRoles(userId);

            string key = string.Join("", roles.Select(s => s[0].ToString().ToUpper()));

            return key;

        }

        public async Task<bool> UpdateUser(User user)
        {

            IdentityResult result = await this.userManager.UpdateAsync(user);

            return (result != null);
        }

        public IEnumerable<User> GetUsersInRole(string roleName)
        {
            try
            {
                var role = this.roleManager.FindByName(roleName);
                if (role != null)
                {
                    var list = this.userManager
                        .Users
                        .Where(w => w.Roles.Where(r => r.RoleId == role.Id).Any());

                    if (list != null)
                    {
                        return list.ToList();
                    }
                    else
                    {
                        return Enumerable.Empty<User>();
                    }
                }
                else
                {
                    return Enumerable.Empty<User>();
                }

            }
            catch (Exception)
            {
                return Enumerable.Empty<User>();
            }
        }

        public IEnumerable<string> GetUserEmailsInRole(string roleName)
        {
            try
            {
                var role = this.roleManager.FindByName(roleName);
                if (role != null)
                {
                    var list = this.userManager
                        .Users
                        .Where(w => w.Roles.Where(r => r.RoleId == role.Id).Any());

                    if (list.Any())
                    {
                        return list.Select(s => s.Email).ToList();
                    }
                    else
                    {
                        return Enumerable.Empty<string>();
                    }
                }
                else
                {
                    return Enumerable.Empty<string>();
                }
            }
            catch (Exception)
            {
                return Enumerable.Empty<string>();
            }
        }


        public async Task<IResultServiceModel<bool>> UserIsOnlyCustomer(User user)
        {
            IResultServiceModel<bool> rm = new ResultServiceModel<bool>();

            try
            {
                var roleCustomer = await this.roleManager.FindByNameAsync(RoleDefinitions.Customer);

                if (roleCustomer != null)
                {
                    if (user.Roles.Where(w => w.RoleId != roleCustomer.Id).Any())
                    {
                        rm.OnSuccess(false);
                    }
                    else
                    {
                        rm.OnSuccess(true);
                    }
                }
                else 
                {
                    rm.OnError();
                }
            }
            catch (Exception ex)
            {
                rm.OnException(ex);
            }

            return rm;
        }

        public void Dispose()
        {
            this.userManager.Dispose();
            this.roleManager.Dispose();
        }

        private async Task<string> GeneratePasswordResetTokenAsync(string userName)
        {
            var user = this.userManager.FindByName(userName.CleanLoginEmail());
            return await this.userManager.GeneratePasswordResetTokenAsync(user.Id);
        }



    }
}
