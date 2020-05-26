using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Cagravol.Domain.POCO.Panels
{
    public class PanelInfo
    {
        public PanelRow top { get; set; }
        public PanelRow middle { get; set; }
        public PanelRow bottom { get; set; }
    }
}
