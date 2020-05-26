using SGE.Cagravol.Domain.Entities.Common;
using SGE.Cagravol.Domain.Entities.Files;
using SGE.Cagravol.Domain.Entities.Interfaces.Workflow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGE.Cagravol.Domain.Entities.Workflows;
using SGE.Cagravol.Domain.Entities.Identity;

namespace SGE.Cagravol.Domain.Entities.Workflows
{
    public abstract class WFEntityState<T>
        :IEntityIdentity, IWFEntityState<T>
    {
        public long Id { get; set; }
        public long EntityId { get; set; }
        public long WFStateId { get; set; }
        public string UserId { get; set; }
        public DateTime TS { get; set; }
         
        public virtual T Entity { get; set; }
        public virtual WFState WFState { get; set; }
        public virtual User User { get; set; }
        //public abstract ICollection<WFEntityStateNote<T>> Notes { get; set; }
              
        public WFEntityState()
        {
            //this.Notes = new HashSet<WFEntityStateNote<T>>();            
        }
    }
}
