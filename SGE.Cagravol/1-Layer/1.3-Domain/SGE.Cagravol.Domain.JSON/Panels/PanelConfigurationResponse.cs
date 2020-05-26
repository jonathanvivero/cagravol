using SGE.Cagravol.Domain.POCO.NgRoutes;
using SGE.Cagravol.Domain.POCO.Panels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Cagravol.Domain.JSON.Panels
{
    public class PanelConfigurationResponse
    {
        public IEnumerable<NgRouteProvider> routes { get; set; }
        public PanelInfo panelInfo { get; set; }

    }
}
