using SGE.Cagravol.Application.Core.Enums;
using SGE.Cagravol.Domain.Entities.Common;
using SGE.Cagravol.Application.ServiceModel;
using SGE.Cagravol.Domain.Repositories.Base;
using SGE.Cagravol.Domain.Repositories.Common;
using SGE.Cagravol.Infrastructure.Data.Repositories.Base;
using SGE.Cagravol.Presentation.Resources.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGE.Cagravol.Application.Core.Enums.Common;

namespace SGE.Cagravol.Infrastructure.Data.Repositories.Common
{
    public class AppConfigurationRepository 
        : BaseRepository<AppConfiguration>, IAppConfigurationRepository        
    {
        public AppConfigurationRepository(ISGEContext context)
			: base(context)
		{
		}

        public IResultModel SetValue(AppConfigurationKeyEnum key, string value)
        {
            return this.SetValue(key.ToString(), value);
        }

        public IResultModel SetValue(string key, string value)
        {
            IResultModel rm = new ResultModel();

            try
            {
                var rmVal = this.GetValue(key);

                if (rmVal.Success)
                {
                    AppConfiguration item = rmVal.Value;
                    item.Value = value;
                    
                    this.context.SetState(item, System.Data.Entity.EntityState.Modified);
                    this.context.SaveChanges();

                    rm.OnSuccess();
                }
                else
                {
                    rm.OnError(rmVal.ErrorMessage, rmVal.ErrorCode);
                }

            }
            catch (Exception ex)
            {
                rm.OnException(ex);
            }

            return rm;
        }

        public IResultServiceModel<AppConfiguration> GetValue(string key)
        {
            IResultServiceModel<AppConfiguration> rsm = new ResultServiceModel<AppConfiguration>();
            try
            {
                var item = this.context
                    .AppConfigurations
                    .Where(w => w.Key == key)
                    .FirstOrDefault();

                if (item != null)
                {
                    rsm.OnSuccess(item);
                }
                else
                {
                    rsm.OnError(ErrorResources.ItemDoesNotExist, EnumErrorCode.ITEM_DOES_NOT_EXIST);
                }
            }
            catch (Exception ex)
            {
                rsm.OnException(ex);
            }

            return rsm;
        }

        public IResultServiceModel<AppConfiguration> GetValue(AppConfigurationKeyEnum key)
        {
            return this.GetValue(key.ToString());
        }

        public IResultServiceModel<IEnumerable<AppConfiguration>> GetOnlyPlatformParams()
        { 
            var allowed = new string[] { EnumAppConfigurationKeyDefinition.PublicKey, EnumAppConfigurationKeyDefinition.DefaultTotalStandsPerEvent };
            IResultServiceModel<IEnumerable<AppConfiguration>> rsm = new ResultServiceModel<IEnumerable<AppConfiguration>>();

            try
            {
                var list = this.context.AppConfigurations.Where(w => allowed.Contains(w.Key));
                rsm.OnSuccess(list);
            }
            catch (Exception ex)
            {

                rsm.OnException(ex);
            }
            return rsm;
        }
    }
}
