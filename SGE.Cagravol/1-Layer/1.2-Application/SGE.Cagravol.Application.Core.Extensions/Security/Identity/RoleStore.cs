//using EI.Satellite.Domain.Entities.Identity;
//using EI.Satellite.Infrastructure.Identity.Data;
//using EI.Satellite.Infrastructure.Utils.Validations;
//using Microsoft.AspNet.Identity;
//using Microsoft.AspNet.Identity.EntityFramework;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace EI.Satellite.Presentation.Web.Extensions.Security.Identity
//{
//    public sealed class RoleStore : IRoleStore<Role, string>
//    {
//        private readonly RoleManager<Role, string> roleManager;
//        private readonly IEIIdentityContext dbContext;
//        public RoleStore(IEIIdentityContext dbContext)			
//        {
//            Check.NotNull(dbContext, "dbContext");
			
//            this.dbContext = dbContext;

//            this.roleManager= new RoleManager<Role>(
//                new RoleStore<Role>((EIIdentityContext)dbContext)
//            );

//            AutoSaveChanges = true;
//        }

//        /// <summary>
//        /// Gets or sets a value indicating whether automaticaly save changes.
//        /// </summary>
//        /// <value>
//        /// <c>true</c> if automaticaly save changes; otherwise, <c>false</c>.
//        /// </value>
//        public bool AutoSaveChanges { get; set; }


//        public Task CreateAsync(Role role)			
//        {
//            if (role == null)
//            {
//                throw new NotImplementedException();
//            }
//            return this.roleManager.CreateAsync(role);
//        }

//        public Task DeleteAsync(Role role)
//        {
//            if (role == null) 
//            { 
//                throw new NotSupportedException();
//            }
//            return this.roleManager.DeleteAsync(role);
		
//        }

//        public Task<Role> FindByIdAsync(string roleId)
//        {
//            if (roleId == null) 
//            { 
//                throw new NotSupportedException();
//            }
//            return this.roleManager.FindByIdAsync(roleId);
//        }

//        public Task<Role> FindByNameAsync(string roleName)
//        {
//            if (roleName == null) 
//            { 
//                throw new NotSupportedException();
//            }
//            return this.roleManager.FindByNameAsync(roleName);
//        }

//        public Task UpdateAsync(Role role)
//        {
//            if (role == null) 
//            {
//                throw new NotSupportedException();
//            }
//            return this.roleManager.UpdateAsync(role);
//        }

//        public void Dispose()
//        {
			
//        }
//    }
//}
