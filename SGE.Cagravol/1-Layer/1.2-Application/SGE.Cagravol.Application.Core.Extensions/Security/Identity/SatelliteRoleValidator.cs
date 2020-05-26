//using EI.Satellite.Domain.Entities.Identity;
//using Microsoft.AspNet.Identity;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace EI.Satellite.Presentation.Web.Extensions.Security.Identity
//{
//    public class SatelliteRoleValidator : RoleValidator<Role, string>
//    {
//        private readonly SatelliteRoleManager manager;

//        public SatelliteRoleValidator(SatelliteRoleManager manager)
//            : base(manager)
//        {
//            this.manager = manager;
//        }

//        public override async Task<IdentityResult> ValidateAsync(Role item)
//        {
//            var result = await base.ValidateAsync(item);

//            var exists = this.manager
//                .FindByName(item.Name) != null;

//            if (exists)
//            {
//                result = new IdentityResult("The name of the rule is used by another role.");
//            }
//            return result;
//        }
//    }
//}
