using SGE.Cagravol.Application.Core.Extensions.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Cagravol.Application.Core.Extensions.Models
{
    public class MenuItem
    {
        public MenuItemTypeEnum Type { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public List<MenuItem> SubMenuItems { get; set; }
    }
}
