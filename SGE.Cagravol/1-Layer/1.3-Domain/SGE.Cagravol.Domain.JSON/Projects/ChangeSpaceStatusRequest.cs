using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Cagravol.Domain.JSON.Projects
{
    public class ChangeSpaceStatusRequest
    {
        public long projectId { get; set; }
        public long customerId {get;set;}
        public string email { get; set; }
        public string password { get; set; }
        public bool registered { get; set; }
        public long index { get; set; }
        public int currentStatus { get; set; }
        public int newStatus { get; set; }
        public string userName { get; set; }


    }
}
