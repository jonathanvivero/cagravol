using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Cagravol.Domain.JSON.Projects
{
    public class ChangeSpaceStatusResponse
    {
        public int newSpaceStatus { get; set; }
        public long index { get; set; }
        public bool reserved { get; set; }
        public bool registered { get; set; }
        public string email { get; set; }
    }
}
