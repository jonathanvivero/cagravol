using SGE.Cagravol.Domain.Entities.History;
using SGE.Cagravol.Domain.POCO.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Cagravol.Domain.POCO.Workflows
{
    public class WFFileStateNotePOCO
    {
        public long Id { get; set; }
        public string Comment { get; set; }
        public DateTime TS { get; set; }
        public UserPOCO User {get;set;}

        public WFFileStateNotePOCO() { }
        public WFFileStateNotePOCO(WFFileStateNote entity) 
        {
            this.Id = entity.Id;
            this.Comment = entity.Comment;
            this.TS = entity.TS;            
            this.User = new UserPOCO(entity.User);            
        }
    }
}
