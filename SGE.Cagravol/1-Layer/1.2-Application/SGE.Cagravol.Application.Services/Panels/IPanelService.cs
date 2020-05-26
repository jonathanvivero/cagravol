using SGE.Cagravol.Domain.JSON.Panels;
using SGE.Cagravol.Application.ServiceModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Cagravol.Application.Services.Panels
{
    public interface IPanelService
    {
        Task<IResultServiceModel<PanelConfigurationResponse>> GetConfigurationCurrentUserAsync(string userName);
        IResultServiceModel<PanelConfigurationResponse> GetConfigurationCurrentUser(string userName);
    }
}
