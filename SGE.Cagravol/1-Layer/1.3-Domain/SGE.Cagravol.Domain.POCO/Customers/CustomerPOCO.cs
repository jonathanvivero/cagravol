using SGE.Cagravol.Domain.Entities.Customers;
using SGE.Cagravol.Domain.POCO.Files;
using SGE.Cagravol.Domain.POCO.Identity;
using SGE.Cagravol.Domain.POCO.Projects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Cagravol.Domain.POCO.Customers
{
    public class CustomerPOCO
    {
        public long Id { get; set; }
        //public long ProjectId { get; set; }
        //public long BillDataTypeId { get; set; }
        //public string UserId { get; set; }
        public string Name { get; set; }
        public string Notes { get; set; }
        //public bool IsGeneric { get; set; }
        public bool Reserved { get; set; }
        public bool Registered { get; set; }
        public string Email { get; set; }
        //public string PasswordHash { get; set; }
        //public string SignUpCode { get; set; }
        public string BillLegalIdentification { get; set; }
        public string BillAddress { get; set; }
        public string BillPostalCode { get; set; }
        public string BillCity { get; set; }
        public string BillCountry { get; set; }
        public string SignUpCode { get; set; }
        public int SpaceNumber { get; set; }
               
        //public BillDataTypePOCO BillDataType { get; set; }
        public IEnumerable<FilePOCO> Files { get; set; }

        public CustomerPOCO() { }
        public CustomerPOCO(Customer entity) 
        {
            this.Id = entity.Id;
            //this.ProjectId = entity.ProjectId;
            //this.BillDataTypeId = entity.BillDataTypeId;
            //this.UserId = entity.UserId;

            this.SignUpCode = entity.SignUpCode;
            this.Name = entity.Name;
            this.Notes = entity.Notes;
            this.Email = entity.Email;
            this.BillLegalIdentification = entity.BillLegalIdentification;
            this.BillAddress = entity.BillAddress;
            this.BillPostalCode = entity.BillPostalCode;
            this.BillCity = entity.BillCity;
            this.BillCountry = entity.BillCountry;
            this.SpaceNumber = entity.SpaceNumber;
            this.Registered = entity.Registered;
            this.Reserved = entity.Reserved;


            
            this.Files = entity.Files.Select(s => new FilePOCO(s));                
        }
    }
}
