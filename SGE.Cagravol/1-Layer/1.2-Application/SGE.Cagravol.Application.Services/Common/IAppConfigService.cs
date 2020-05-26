using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Cagravol.Application.Services.Common
{
    public interface IAppConfigurationService
    {
        string AppVersion { get; }
        long AppVersionNumber { get; }
        string DefaultFileWorkflowCode { get; }
        string DefaultGeneralSpaceFileWorkflowCode { get; }
        int DefaultTotalStandsPerEvent { get; }
        string PublicKey { get; }
        string SecretKey { get; }
    }
}
