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
using SGE.Cagravol.Domain.Entities.Customers;
using SGE.Cagravol.Domain.Entities.Files;

namespace SGE.Cagravol.Infrastructure.Data.Migrations.Updates
{
    /// <summary>
    /// Deploy the very first data in the DB after first Migration.
    /// Since this is the inital migration, only creates the Application Configuration registry with the app version 
    /// First of all, check for if this key already exist.
    /// Both cases, it returns the AppConfiguration instance.
    /// </summary>
    public class PortalUpdate_004 : PortalUpdate, IPortalUpdate
    {
        public PortalUpdate_004(SGEContext _context, AppConfiguration _keyVersion)
            : base(_context, _keyVersion)
        { }

        public override AppConfiguration DoUpdate()
        {

            var existingCustomerList = this.context.Customers.ToList();
            //var existingFileList = this.context.Files.ToList();

            var cuzUpdateList = new List<Customer>();
            //var fileUpdateList = new List<File>();

            foreach (var cuz in existingCustomerList)
            {
                if (!string.IsNullOrWhiteSpace(cuz.Email))
                {
                    cuz.Reserved = true;
                    cuz.Registered = true;
                    cuzUpdateList.Add(cuz);
                }
                //else 
                //{
                //    cuz.Reserved = false;
                //    cuz.Registered = false;
                //}
            }

            //foreach (var file in existingFileList)
            //{
            //    file.FileNotes = string.Empty;
            //    fileUpdateList.Add(file);
            //}

            this.context.Customers.AddOrUpdate<Customer>(cuzUpdateList.ToArray());
            //this.context.Files.AddOrUpdate<File>(fileUpdateList.ToArray());

            this.keyVersion.Value = "0.0.4";

            return this.keyVersion;
        }


    }
}
