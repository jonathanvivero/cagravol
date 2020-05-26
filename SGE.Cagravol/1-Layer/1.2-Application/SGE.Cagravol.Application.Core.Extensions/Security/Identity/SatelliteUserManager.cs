//using EI.Satellite.Domain.Entities.Identity;
//using EI.Satellite.Presentation.Web.Extensions.Security.Identity;
//using EI.Satellite.Infrastructure.Utils.Validations;
//using Microsoft.AspNet.Identity;
//using System;
//using Microsoft.Practices.Unity;
//using System.Threading.Tasks;

//namespace EI.Satellite.Presentation.Web.Extensions.Security.Identity
//{
//    public class SatelliteUserManager : UserManager<User, string>
//    {
		
//        //private IUserStore<User, string> store;
//        //private UserManager<User, string> um;
 
//        //[InjectionMethod]
//        //public void Initialize(IUserStore<User, string> store) 
//        //{
//        //}

//        /// <summary>
//        /// Initializes a new instance of the <see cref="SatelliteUserManager"/> class.
//        /// </summary>
//        /// <param name="store">The user store.</param>
//        public SatelliteUserManager(IUserStore<User, string> store)
//            :base(store)
//        {

//            //Check.NotNull(store, "store");
//            //this.store = store;

//            //this.um = new UserManager<User, string>(this.store);

//            this.UserValidator = new UserValidator<User, string>(this)
//            {
//                AllowOnlyAlphanumericUserNames = false,
//                RequireUniqueEmail = false
//            };

//            var provider = new Microsoft.Owin.Security.DataProtection.DpapiDataProtectionProvider("EISATELLITE");
//            var DataProtectorTokenProvider = new Microsoft.AspNet.Identity.Owin.DataProtectorTokenProvider<User, string>(provider.Create("EmailConfirmation", "Changepass"));
//            DataProtectorTokenProvider.TokenLifespan = TimeSpan.FromDays(1);

//            this.UserTokenProvider = DataProtectorTokenProvider;
//        }

//        public override Task<User> FindAsync(string userName, string password) {
//            return base.FindAsync(userName, password);
		
//        }

		
//    }
//}
