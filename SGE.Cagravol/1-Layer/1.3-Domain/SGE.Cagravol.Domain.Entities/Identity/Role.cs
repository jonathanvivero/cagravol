using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;


namespace SGE.Cagravol.Domain.Entities.Identity
{
	public class Role : IdentityRole //<string, UserRole>
	{        
        public Role() 
            :base()
        {
        }        
	}
}
