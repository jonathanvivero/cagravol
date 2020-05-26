using SGE.Cagravol.Domain.POCO.ServiceModels.Identity;
using SGE.Cagravol.Domain.Entities.Identity;
//using SGE.Cagravol.Domain.Entities.Users;
//using SGE.Cagravol.Domain.Entities.Users.Enums;
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
using Microsoft.AspNet.Identity.EntityFramework;
using SGE.Cagravol.Application.ServiceModel;
namespace SGE.Cagravol.Application.Services.Identity
{
    public interface IAccountService: IDisposable
    {

        bool IsAuthenticated { get; }

        //AccountService Create(IdentityFactoryOptions<AccountService> options, IOwinContext context);

        IDataProtectionProvider DataProtectionProvider { set; }
        string UserName { get; }
        string Name { get; }
        string NameSurname { get; }

        string Id { get; }

        long CustomerId { get; }

        bool IsAdmin { get; }

        Task<UserLoginResultServiceModel> Login(string userEmail, string userPassword, bool isPersistent);
        Task<UserLoginResultServiceModel> LoginAfterConfirmEmail(User user);
        void SignOut();

        Task<bool> AddToRole(string userId, string roleName);
        Task<bool> RemoveFromRole(string userId, string roleName);
        //Task<bool> DeleteFromRole(long userId, string roleName);

        //Task<bool> AddRole(Role roleName);

        //Task<bool> DeleteRole(long roleId);

        Task SignIn(User userProfile, bool isPersistent);

        Task<bool> Remove(User user);

        Task<string> GenerateEmailConfirmationTokenAsync(User user);
        Task<string> GenerateChangePassTokenAsync(User user);

        Task<UserCreationResultServiceModel> CreateUserAsync(User user, string password);
        Task<UserCreationResultServiceModel> CreateUserWithMainRoleAsync(User user, string password, string mainRoleName = "");
        Task<IdentityResult> DeleteUserAsync(User user);
        //void RemoveUserFromRole(long user, string role);

        //void RemoveUserFromRoles(long userId, IEnumerable<string> roles);
        //void AddUserToRoles(long userId, IEnumerable<SelectListItem> roles);

        //void AddUserToRole(long user, string role);

        bool IsUserInRole(string username, string rolename);
        bool IsUserInRoleById(string userId, string rolename);
        bool IsUserAdmin(string userName);

        Task<User> FindUserByUserNameAsync(string userEmail);
        Task<User> FindUserByUserNameOrEmailAsync(string userEmail);
        Task<User> FindUserByIdAsync(string userId);

        Task<User> CheckUserPassword(string userEmail, string password);

        Task<User> GetCurrentUserAsync();
        //bool IsUserInRole(long userId, string rolename);
        //bool IsUserAdmin(long userId);
        IEnumerable<string> GetRolesForUser(string userId);
        string GetRolesKeyFlagsForUser(string userId);
        IEnumerable<IdentityRole> GetAllRoles();
        Task<bool> RemoveUsersByCustomer(long customerId);
        Task<IResultModel> RemoveListOfUsersByProject(IEnumerable<string> userIdList);
        IEnumerable<User> GetUsersByCustomer(long customerId, bool orderByCreationDate = true);
        IEnumerable<User> GetHomeUsers();
        //IdentityRole FindRole(string id);
        //Task<bool> UpdateRole(IdentityRole role);
        Task<bool> ConfirmEmail(User user, string token);
        Task<bool> ConfirmResetToken(User user, string token);
        Task<IdentityResult> ResetPasswordAsync(string userName, string password);
        Task<IdentityResult> ResetPasswordAsync(User user, string code, string password);
        //UserTypesEnum UserType { get; }
        Task<bool> UpdateUser(User user);

        IEnumerable<User> GetUsersInRole(string roleName);
        IEnumerable<string> GetUserEmailsInRole(string roleName);

        Task<IResultServiceModel<bool>> UserIsOnlyCustomer(User user);

    }
}
