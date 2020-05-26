using SGE.Cagravol.Domain.Entities.Projects;
using SGE.Cagravol.Domain.POCO.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Cagravol.Domain.POCO.Projects
{
    public class UserProjectPOCO
    {
        public long Id { get; set; }
        public long ProjectId { get; set; }
        public string UserId { get; set; }
        public bool IsAuthorized { get; set; }
        public bool IsOwner { get; set; }
        public UserPOCO User { get; set; }

        public UserProjectPOCO() { }
        public UserProjectPOCO(UserProject entity)
        {
            if (entity != null)
            {
                this.Id = entity.Id;
                this.ProjectId = entity.ProjectId;
                this.UserId = entity.UserId;
                this.IsAuthorized = entity.IsAuthorized;
                this.IsOwner = entity.IsOwner;
                if (entity.User != null)
                {
                    this.User = new UserPOCO(entity.User);
                }
            }
        }



    }
}
