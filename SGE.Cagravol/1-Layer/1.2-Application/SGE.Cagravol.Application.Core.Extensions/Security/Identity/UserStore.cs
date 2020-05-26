//using EI.Satellite.Application.Core.Services;
//using EI.Satellite.Domain.Entities.Identity;
//using EI.Satellite.Infrastructure.Data;
//using EI.Satellite.Infrastructure.Identity.Data;
//using EI.Satellite.Infrastructure.Utils.Validations;
//using Microsoft.AspNet.Identity;
//using Microsoft.AspNet.Identity.EntityFramework;
//using Microsoft.AspNet.Identity.Owin;
//using Microsoft.Owin;
//using Microsoft.Owin.Security;
//using System;
//using System.Collections.Generic;
//using System.Data.Entity;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Web;

//namespace EI.Satellite.Presentation.Web.Extensions.Security.Identity
//{
//    public sealed class UserStore : IUserStore<User, string>, IUserPasswordStore<User, string>
//    {
//        private readonly UserManager<User, string> userManager;
//        private readonly IUserStore<User, string> userStore;
//        private readonly IEIIdentityContext dbContext;
//        private bool _disposed = false;

//        /// <summary>
//        /// Initializes a new instance of the <see cref="UserStore"/> class.
//        /// </summary>
//        /// <param name="dbContext">The unit of work.</param>
//        public UserStore(IEIIdentityContext dbContext)
//        {
//            Check.NotNull(dbContext, "dbContext");

//            this.dbContext = dbContext;
//            this.userStore = new UserStore<User>((EIIdentityContext)dbContext);
//            this.userManager = new UserManager<User>((UserStore<User>)this.userStore);			

//            AutoSaveChanges = true;
//        }

//        /// <summary>
//        /// Gets or sets a value indicating whether automaticaly save changes.
//        /// </summary>
//        /// <value>
//        /// <c>true</c> if automaticaly save changes; otherwise, <c>false</c>.
//        /// </value>
//        public bool AutoSaveChanges { get; set; }


//        ///<inheritdoc/>
//        public async Task CreateAsync(User user)
//        {
//            ThrowIfDisposed();
//            if (user == null)
//            {
//                throw new ArgumentNullException("user");
//            }
//            //this.userManager.Users.Add(user);
//            await this.userManager.CreateAsync(user);
//            //await this.SaveChanges().ConfigureAwait(false);
//        }

//        ///<inheritdoc/>
//        public async Task UpdateAsync(User user)
//        {
//            ThrowIfDisposed();
//            if (user == null)
//            {
//                throw new ArgumentNullException("user");
//            }

//            await this.userManager.UpdateAsync(user);

//            //_dbContext.SetState(user, EntityState.Modified);
//            //await this.SaveChanges().ConfigureAwait(false);
//        }

//        ///<inheritdoc/>
//        public async Task DeleteAsync(User user)
//        {
//            ThrowIfDisposed();
//            if (user == null)
//            {
//                throw new ArgumentNullException("user");
//            }
//            //_dbContext.Users.Remove(user);
//            //await this.SaveChanges().ConfigureAwait(false);
//            await this.userManager.DeleteAsync(user);
//        }

//        ///<inheritdoc/>
//        public Task<User> FindByIdAsync(string userId)
//        {
//            ThrowIfDisposed();
//            //return _dbContext.Users.SingleOrDefaultAsync(x => x.Id == userId);
//            return this.userManager.FindByIdAsync(userId);
//        }

//        ///<inheritdoc/>
//        public async Task<User> FindByNameAsync(string userName)
//        {
//            ThrowIfDisposed();
//            if (userName == null)
//            {
//                throw new ArgumentNullException("userName");
//            }
//            //var user = await _dbContext
//            //	.Users
//            //	.Where(i => i.UserName == userName)
//            //	.SingleOrDefaultAsync();

//            //return user;

//            return await this.userManager.FindByNameAsync(userName);
//        }

//        //public async Task AddToRoleAsync(UserProfile user, string roleName)
//        //{
//        //	var role = await this._dbContext
//        //		.Roles
//        //		.Where(i => i.Name == roleName)
//        //		.SingleOrDefaultAsync();

//        //	if (role != null)
//        //	{
//        //		user.Roles.Add(new UserRole() { RoleId = role.Id, UserId = user.Id });
//        //		await this.SaveChanges();
//        //	}
//        //}

//        //public async Task<IList<string>> GetRolesAsync(UserProfile user)
//        //{
//        //	var result = await this._dbContext
//        //				.Roles
//        //				.Where(i => i.Users
//        //				.Where(j => j.UserId == user.Id)
//        //				.Any())
//        //				.Select(i => i.Name)
//        //				.ToListAsync();

//        //	return result;
//        //}

//        //public async Task<bool> IsInRoleAsync(UserProfile user, string roleName)
//        //{
//        //	var result = false;

//        //	var role = await this._dbContext
//        //		.Roles
//        //		.Where(i => i.Name == roleName)
//        //		.SingleOrDefaultAsync();

//        //	if (role != null)
//        //	{
//        //		result = user
//        //			.Roles
//        //			.Where(i => i.RoleId == role.Id)
//        //			.Any();
//        //	}

//        //	return result;
//        //}

//        //public async Task RemoveFromRoleAsync(UserProfile user, string roleName)
//        //{
//        //	var role = await this._dbContext
//        //	   .Roles
//        //	   .Where(i => i.Name == roleName)
//        //	   .SingleOrDefaultAsync();

//        //	if (role != null)
//        //	{
//        //		var userRole = user
//        //			.Roles
//        //			.Where(i => i.RoleId == role.Id)
//        //			.SingleOrDefault();

//        //		if (userRole != null)
//        //		{
//        //			user.Roles
//        //				.Remove(userRole);
//        //		}
//        //	}
//        //}

//        ///<inheritdoc/>
//        public void Dispose()
//        {
//            if (!_disposed)
//            {
//                _disposed = true;
//                this.userManager.Dispose();
//            }
//        }

//        private void ThrowIfDisposed()
//        {
//            if (_disposed) throw new ObjectDisposedException(GetType().Name);
//        }

//        //private async Task SaveChanges()
//        //{
//        //	if (AutoSaveChanges)
//        //	{
//        //		int num = await this.userManager.SaveChangesAsync(); //.ConfigureAwait(false);
//        //	}
//        //}


//        public Task<string> GetPasswordHashAsync(User user)
//        {
//            if (user == null)
//            {
//                throw new NotSupportedException();
//            }
//            try
//            {											
//                var pUser = this.userManager.Users.Where(w => w.UserName == user.UserName).FirstOrDefault();
//                if (pUser != null)
//                {					
//                    return Task<string>.Factory.StartNew(() => pUser.PasswordHash);
//                }

//            }
//            catch (KeyNotFoundException) {
//                throw new NotSupportedException();
//            }

//            return Task<string>.Factory.StartNew(() => "");

//        }

//        public Task<bool> HasPasswordAsync(User user)
//        {
//            throw new NotImplementedException();
//        }

//        public Task SetPasswordHashAsync(User user, string passwordHash)
//        {
//            throw new NotImplementedException();
//        }		

//    }
//}
