using SGE.Cagravol.Application.Core.Enums.Common;
using SGE.Cagravol.Application.Core.Enums.Common.Common;
using SGE.Cagravol.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Cagravol.Infrastructure.Data.Migrations.Updates
{
    /// <summary>
    /// Deploy the very first data in the DB after first Migration.
    /// Since this is the inital migration, only creates the Application Configuration registry with the app version 
    /// First of all, check for if this key already exist.
    /// Both cases, it returns the AppConfiguration instance.
    /// </summary>
    public class PortalUpdate_000 : PortalUpdate, IPortalUpdate
    {
        public PortalUpdate_000(SGEContext _context, AppConfiguration _keyVersion)
            : base(_context, _keyVersion)
        { }

        public override AppConfiguration DoUpdate()
        {
            this.keyVersion = this.context
                .AppConfigurations
                .Where(w => w.Key == EnumAppConfigurationKeyDefinition.AppVersion)
                .FirstOrDefault();

            if (this.keyVersion == null)
            {
                this.keyVersion = this.context.AppConfigurations.Create();

                this.keyVersion.Key = EnumAppConfigurationKeyDefinition.AppVersion;
                this.keyVersion.Value = "0.0.1";
                this.keyVersion.FieldType = EnumAppConfigurationFieldType.Text; 
                this.keyVersion.IsPublicViewable    = false;
                this.keyVersion.IsPublicEnable = false;

                this.context.AppConfigurations.Add(this.keyVersion);

                this.context.SaveChanges();

            }
            else
            {
                //Do nothing
            }

            return this.keyVersion;
        }
    }
}
