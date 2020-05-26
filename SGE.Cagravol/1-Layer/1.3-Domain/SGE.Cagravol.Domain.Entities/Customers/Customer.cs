using Newtonsoft.Json;
using SGE.Cagravol.Domain.Entities.Common;
using SGE.Cagravol.Domain.Entities.Files;
using SGE.Cagravol.Domain.Entities.Identity;
using SGE.Cagravol.Domain.Entities.Projects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Cagravol.Domain.Entities.Customers
{
    [JsonObject(IsReference = true)]
    public class Customer
        : IEntityIdentity

    {
        public long Id { get;set; }
        public long ProjectId { get; set; }
        public long BillDataTypeId { get; set; }
        public string UserId { get; set; }
        public string Name { get;set; }        
        public string Notes { get;set;}
        public bool IsGeneric { get;set;}
        public bool Registered { get; set; }
        public bool Reserved { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get;set; }
        public string SignUpCode { get;set; }
        public string BillLegalIdentification { get; set; }
        public string BillAddress { get; set; }
        public string BillPostalCode { get; set; }
        public string BillCity { get; set; }
        public string BillCountry { get; set; }
        public int SpaceNumber { get; set; }
        
        public virtual Project Project { get; set; }
        public virtual User User { get; set; }
        public virtual BillDataType BillDataType { get; set; }
        public virtual ICollection<File> Files { get; set; }

        public Customer() 
        {
            this.Files = new HashSet<File>();
        }


    }
}
