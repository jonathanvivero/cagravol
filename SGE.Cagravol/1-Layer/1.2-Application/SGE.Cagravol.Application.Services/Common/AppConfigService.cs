using SGE.Cagravol.Application.Core.Enums.Common;
using SGE.Cagravol.Application.Services.Utils;
using SGE.Cagravol.Domain.Repositories.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Cagravol.Application.Services.Common
{
    public class AppConfigurationService
        : IAppConfigurationService
    {
        private readonly IAppConfigurationRepository appConfigurationRepository;
        private readonly IUtilService utilService;
        public AppConfigurationService(IAppConfigurationRepository appConfigurationRepository,
            IUtilService utilService)
        {
            this.appConfigurationRepository = appConfigurationRepository;
            this.utilService = utilService;
        }

        public string AppVersion
        {
            get
            {
                try
                {
                    var rmAC = this.appConfigurationRepository.GetValue(AppConfigurationKeyEnum.APP_VERSION);
                    if (rmAC.Success)
                    {
                        return rmAC.Value.Value;
                    }
                    else
                    {
                        return string.Empty;
                    }
                }
                catch (Exception)
                {
                    return string.Empty;
                }
            }
        }
        public long AppVersionNumber
        {
            get
            {
                try
                {
                    var rmAC = this.appConfigurationRepository.GetValue(AppConfigurationKeyEnum.APP_VERSION);
                    if (rmAC.Success)
                    {
                        return utilService.ConvertProductVersionToLong(rmAC.Value.Value);
                    }
                    else
                    {
                        return 0;
                    }
                }
                catch (Exception)
                {
                    return 0;
                }
            }
        }
        public string DefaultFileWorkflowCode {
            get
            {
                try
                {
                    var rmAC = this.appConfigurationRepository.GetValue(AppConfigurationKeyEnum.DEFAULT_FILE_WORKFLOW_CODE);
                    if (rmAC.Success)
                    {
                        return rmAC.Value.Value;
                    }
                    else
                    {
                        return string.Empty;
                    }
                }
                catch (Exception)
                {
                    return string.Empty;
                }
            }
        }

        public string DefaultGeneralSpaceFileWorkflowCode
        {
            get
            {
                try
                {
                    var rmAC = this.appConfigurationRepository.GetValue(AppConfigurationKeyEnum.DEFAULT_GENERAL_SPACE_FILE_WORKFLOW_CODE);
                    if (rmAC.Success)
                    {
                        return rmAC.Value.Value;
                    }
                    else
                    {
                        return string.Empty;
                    }
                }
                catch (Exception)
                {
                    return string.Empty;
                }
            }
        }
        public int DefaultTotalStandsPerEvent {
            get
            {
                try
                {
                    var rmAC = this.appConfigurationRepository.GetValue(AppConfigurationKeyEnum.DEFAULT_TOTAL_STANDS_PER_EVENT);
                    if (rmAC.Success)
                    {
                        return rmAC.Value.Value.ToInt();
                    }
                    else
                    {
                        return 1;
                    }
                }
                catch (Exception)
                {
                    return 1;
                }
            }
        }
        public string PublicKey {
            get
            {
                try
                {
                    var rmAC = this.appConfigurationRepository.GetValue(AppConfigurationKeyEnum.PUBLIC_KEY);
                    if (rmAC.Success)
                    {
                        return rmAC.Value.Value;
                    }
                    else
                    {
                        return string.Empty;
                    }
                }
                catch (Exception)
                {
                    return string.Empty;
                }
            }
        }
        public string SecretKey {
            get
            {
                try
                {
                    var rmAC = this.appConfigurationRepository.GetValue(AppConfigurationKeyEnum.SECRET_KEY);
                    if (rmAC.Success)
                    {
                        return rmAC.Value.Value;
                    }
                    else
                    {
                        return string.Empty;
                    }
                }
                catch (Exception)
                {
                    return string.Empty;
                }
            }
        }

    }
}
