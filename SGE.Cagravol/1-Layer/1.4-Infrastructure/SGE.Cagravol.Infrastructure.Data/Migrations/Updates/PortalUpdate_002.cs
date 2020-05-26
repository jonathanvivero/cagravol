using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SGE.Cagravol.Domain.Entities.Identity;
using SGE.Cagravol.Application.ServiceModel;
using SGE.Cagravol.Infrastructure.Utils.Definitions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Threading.Tasks;
using SGE.Cagravol.Domain.Entities.Workflows;
using SGE.Cagravol.Domain.Entities.Common;

namespace SGE.Cagravol.Infrastructure.Data.Migrations.Updates
{
    /// <summary>
    /// Deploy the very first data in the DB after first Migration.
    /// Since this is the inital migration, only creates the Application Configuration registry with the app version 
    /// First of all, check for if this key already exist.
    /// Both cases, it returns the AppConfiguration instance.
    /// </summary>
    public class PortalUpdate_002 : PortalUpdate, IPortalUpdate
    {
        public PortalUpdate_002(SGEContext _context, AppConfiguration _keyVersion)
            : base(_context, _keyVersion)
        { }

        public override AppConfiguration DoUpdate()
        {
            /* 
             * Nothing to do in this Migration Update. Only increase the Key value for Platform Version
             *
             **/

            this.keyVersion.Value = "0.0.3";

            return this.keyVersion;
        }


    }
}
