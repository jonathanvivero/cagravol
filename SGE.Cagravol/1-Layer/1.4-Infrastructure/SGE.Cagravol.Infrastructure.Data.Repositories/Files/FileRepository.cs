using SGE.Cagravol.Domain.Entities.Files;
using SGE.Cagravol.Application.ServiceModel;
using SGE.Cagravol.Domain.Repositories.Base;
using SGE.Cagravol.Domain.Repositories.Files;
using SGE.Cagravol.Infrastructure.Data.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;
using SGE.Cagravol.Presentation.Resources.Common;
using SGE.Cagravol.Application.Core.Enums.Common;

namespace SGE.Cagravol.Infrastructure.Data.Repositories.Files
{
    public class FileRepository
        : BaseRepository<File>, IFileRepository
    {
        public FileRepository(ISGEContext context)
            : base(context)
        {
        }

        public IResultServiceModel<IEnumerable<File>> GetListByCustomerId(long id)
        {
            return null;
        }

        public IResultServiceModel<File> FindByIdAndUser(long fileId, string userId)
        {
            IResultServiceModel<File> rsm = new ResultServiceModel<File>();

            try
            {
                var item = this.context
                    .Files
                    .Where(w => w.Id == fileId && w.Customer.UserId == userId)
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

        public IResultServiceModel<File> GetCompleteById(long id)
        {
            IResultServiceModel<File> rsm = new ResultServiceModel<File>();

            try
            {
                var item = this.context
                    .Files
                    .Where(w => w.Id == id)
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

        public IResultServiceModel<File> FindGSByIdAndProject(long id, long projectId)
        {
            IResultServiceModel<File> rsm = new ResultServiceModel<File>();

            try
            {
                var item = this.context
                    .Files
                    .Where(w => w.Id == id && w.Customer.ProjectId == projectId)
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
        public IResultModel RemoveAllByCustomer(long customerId)
        {
            var rm = new ResultModel();

            try
            {
                var list = this.context.Files.Where(w => w.CustomerId == customerId).ToList();

                if (list != null)
                {
                    if (list.Any())
                    {
                        this.context.Files.RemoveRange(list);
                        this.context.SaveChanges();
                    }
                }

                rm.OnSuccess();
            }
            catch (Exception ex)
            {
                rm.OnException(ex);                
            }

            return rm;
        }
    }
}
