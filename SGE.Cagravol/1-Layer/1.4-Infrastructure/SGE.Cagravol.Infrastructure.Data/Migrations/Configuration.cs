using SGE.Cagravol.Domain.Entities.Common;
using SGE.Cagravol.Infrastructure.Data.Migrations.Updates;
using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Diagnostics;

namespace SGE.Cagravol.Infrastructure.Data.Migrations
{

    internal sealed class Configuration : DbMigrationsConfiguration<SGE.Cagravol.Infrastructure.Data.SGEContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(SGE.Cagravol.Infrastructure.Data.SGEContext context)
        {            
            
            AppConfiguration keyVersion = null;

            IPortalUpdate portalUpdate = new PortalUpdate_000(context, keyVersion);

            keyVersion = portalUpdate.DoUpdate();

            if (keyVersion.Value == "0.0.1" )
            {
                portalUpdate = new PortalUpdate_001(context, keyVersion);
                keyVersion = portalUpdate.DoUpdate();                
            }

            if (keyVersion.Value == "0.0.2")
            {
                portalUpdate = new PortalUpdate_002(context, keyVersion);
                keyVersion = portalUpdate.DoUpdate();
            }

            if (keyVersion.Value == "0.0.3")
            {
                portalUpdate = new PortalUpdate_004(context, keyVersion);
                keyVersion = portalUpdate.DoUpdate();
            }
            
            context.SetState(keyVersion, EntityState.Modified);

            //context.SaveChanges();
        }
    }
}
