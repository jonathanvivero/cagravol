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
    public class MiscRepository 
        : BaseRepository<Misc>, IMiscRepository        
    {
        public MiscRepository(ISGEContext context)
			: base(context)
		{
		}

        public IResultModel SetValue(string key, string value) 
        {
            return this.SetValue(key, value, DateTime.Now.AddDays(1));
        }

        public IResultModel SetValue(string key, string value, DateTime limit) 
        {
            IResultModel rm = new ResultModel();

            try
            {
                var m = new Misc()
                {
                    Key = key, 
                    Limit = limit, 
                    Value = value
                };

                this.context.Miscellaneous.Add(m);
                this.context.SaveChanges();

                rm.OnSuccess();
            }
            catch (Exception ex)
            {
                rm.OnException(ex);                
            }

            return rm;
        }
        public IResultServiceModel<IEnumerable<Misc>> GetByKey(string key)
        {
            IResultServiceModel<IEnumerable<Misc>> rsm = new ResultServiceModel<IEnumerable<Misc>>();

            try
            {
                var m = this.context.Miscellaneous.Where(w => w.Key == key);
                if (m != null)
                {
                    rsm.OnSuccess(m);
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
        public IResultServiceModel<Misc> GetByKeyWithValue(string key, string value, bool exactMatch = false) 
        {
            IResultServiceModel<Misc> rsm = new ResultServiceModel<Misc>();
            Misc m = null;

            try
            {
                if (exactMatch)
                {
                    m = this.context
                        .Miscellaneous
                        .Where(w => w.Key == key && w.Value == value)
                        .FirstOrDefault();
                }
                else 
                {
                    m = this.context
                        .Miscellaneous
                        .Where(w => w.Key == key && w.Value.Contains(value))
                        .FirstOrDefault();

                }
                if (m != null)
                {
                    rsm.OnSuccess(m);
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
        public IResultServiceModel<string> GetValue(string key) 
        {
            IResultServiceModel<string> rsm = new ResultServiceModel<string>();

            try
            {
                var m = this.context.Miscellaneous.Where(w => w.Key == key).FirstOrDefault();
                if (m != null)
                {
                    rsm.OnSuccess(m.Value);
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
        public IResultModel DeleteOutOfLimit() 
        {
            IResultModel rm = new ResultModel();

            try
            {

                var list = this.context.Miscellaneous.Where(w => w.Limit < DateTime.Now).ToArray();

                if (list != null)
                {
                    if (list.Length > 0)
                    {
                        this.context.Miscellaneous.RemoveRange(list);
                        this.context.SaveChanges();
                    }

                    rm.OnSuccess();
                }
                else 
                {
                    rm.OnError(ErrorResources.ItemDoesNotExist, EnumErrorCode.ITEM_DOES_NOT_EXIST);
                }
            }
            catch (Exception ex)
            {
                rm.OnException(ex);
            }

            return rm;
        }
    }
}
