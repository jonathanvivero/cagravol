using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Cagravol.Entities.Common.Workflow
{
    public interface IWFGroup
    {
        long Id { get; set; }
        string Name { get; set; }
        bool IsPreset { get; set; }
        ICollection<IWFRole> Roles { get; set; }
        ICollection<IWFUser> Users { get; set; }        
    }
}
