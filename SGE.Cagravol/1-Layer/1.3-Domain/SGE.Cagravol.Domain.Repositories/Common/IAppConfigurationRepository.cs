using SGE.Cagravol.Domain.Entities.Common;
using SGE.Cagravol.Application.ServiceModel;
using SGE.Cagravol.Domain.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGE.Cagravol.Application.Core.Enums.Common;

namespace SGE.Cagravol.Domain.Repositories.Common
{
    public interface IAppConfigurationRepository 
        : IBaseRepository<AppConfiguration>
    {
        IResultModel SetValue(AppConfigurationKeyEnum key, string value);
        IResultModel SetValue(string key, string value);

        IResultServiceModel<AppConfiguration> GetValue(string key);
        IResultServiceModel<AppConfiguration> GetValue(AppConfigurationKeyEnum key);

        IResultServiceModel<IEnumerable<AppConfiguration>> GetOnlyPlatformParams();
    }
}
