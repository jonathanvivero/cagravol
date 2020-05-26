using SGE.Cagravol.Domain.Entities.Common;
using SGE.Cagravol.Domain.Entities.Interfaces.Workflow;
using SGE.Cagravol.Domain.Entities.Files;
using SGE.Cagravol.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Cagravol.Domain.Entities.Workflows
{
    public abstract class WFEntityStateNote<T>
        :IEntityIdentity, IWFEntityStateNote<T>
    {
        public long Id { get; set; }
        public long WFEntityStateId { get; set; }
        public string UserId { get; set; }
        public DateTime TS { get; set; }
        public string Comment { get; set; }        
        //public virtual WFEntityState<T> WFEntityState { get; set; }        
        public virtual User User { get; set; }        

        public WFEntityStateNote()
        {
            
        }
    }
}
