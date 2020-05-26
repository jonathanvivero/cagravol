using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Cagravol.Domain.POCO.Panels
{
    public class PanelRow
    {
        public PanelItem left { get; set; }
        public PanelItem center { get; set; }
        public PanelItem right { get; set; }
    }
}
