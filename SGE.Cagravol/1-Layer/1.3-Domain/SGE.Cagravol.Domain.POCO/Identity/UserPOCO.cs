using SGE.Cagravol.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Cagravol.Domain.POCO.Identity
{
    public class UserPOCO
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        public UserPOCO() { }
        public UserPOCO(User entity) 
        {
            this.Id = entity.Id;
            this.UserName = entity.UserName;
            this.Email = entity.Email;
            this.Name = entity.Name;
            this.Surname = entity.Surname;
        }
    }
}
