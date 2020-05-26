using SGE.Cagravol.Domain.Entities.JSON.Files;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Cagravol.Domain.JSON.Alfred
{
    public class AlfredDefaultPlatformParametersResponse
    {
        public int totalStands { get; set; }
        public string publicKey { get; set; }
        public FileTypeJSON[] fileTypes { get; set; }
    }
}
