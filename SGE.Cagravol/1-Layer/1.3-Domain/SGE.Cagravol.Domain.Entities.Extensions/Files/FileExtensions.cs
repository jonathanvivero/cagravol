using SGE.Cagravol.Domain.Entities.History;
using SGE.Cagravol.Domain.Entities.Workflows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Cagravol.Domain.Entities.Files
{
    public static class FileExtensions
    {
        public static WFState CurrentWFState(this File _this)
        {
            if (!_this.History.Any())
            {
                return null;
            }
            else 
            {
                var fhi = _this.History.OrderByDescending(o => o.TS).FirstOrDefault();
                if (fhi != null) 
                {
                    return fhi.WFState;                    
                }
                else 
                {
                    return null;
                }
            }        
        }

        public static WFEntityState<File> LastHistoryItem(this File _this)
        {
            if (!_this.History.Any())
            {
                return null;
            }
            else
            {
                var fhi = _this.History.OrderByDescending(o => o.TS).FirstOrDefault();

                return fhi;
            }

        }        
    }
}
