using SGE.Cagravol.Domain.Entities.Files;
using SGE.Cagravol.Application.ServiceModel;
using SGE.Cagravol.Domain.Repositories.Base;
using SGE.Cagravol.Domain.Repositories.Files;
using SGE.Cagravol.Infrastructure.Data.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Cagravol.Infrastructure.Data.Repositories.Files
{
    public class FileUploadRepository
        : BaseRepository<FileUpload>, IFileUploadRepository
    {
        public FileUploadRepository(ISGEContext context)
            : base(context)
        {
        }

        public IResultServiceModel<IEnumerable<FileUpload>> GetListByCustomerId(long id)
        {
            IResultServiceModel<IEnumerable<FileUpload>> rsm = new ResultServiceModel<IEnumerable<FileUpload>>();

            try
            {
                var list = this.context
                    .FileUploads
                    .Where(w => w.CustomerId == id);

                rsm.OnSuccess(list.ToList());

            }
            catch (Exception ex)
            {
                rsm.OnException(ex);

            }

            return rsm;


        }

        public IResultServiceModel<FileUpload> GetByChannelAndCustomer(string channelId, long customerId)
        {
            IResultServiceModel<FileUpload> rsm = new ResultServiceModel<FileUpload>();

            try
            {
                var item = this.context
                    .FileUploads
                    .Where(w => w.CustomerId == customerId &&
                        w.ChannelId == channelId)
                    .FirstOrDefault();

                rsm.OnSuccess(item);
            }
            catch (Exception ex)
            {
                rsm.OnException(ex);
            }

            return rsm;
        }

        public IResultServiceModel<FileUpload> FindByChannel(string channelId)
        {
            IResultServiceModel<FileUpload> rsm = new ResultServiceModel<FileUpload>();

            try
            {
                var item = this.context
                    .FileUploads
                    .Where(w => w.ChannelId == channelId)
                    .FirstOrDefault();

                rsm.OnSuccess(item);
            }
            catch (Exception ex)
            {
                rsm.OnException(ex);
            }

            return rsm;
        }

        public IResultModel RemoveAllCompleted()
        { 
            IResultModel rm = new ResultModel();

            try
            {
                var list = this.context.FileUploads.Where(w => w.IsCompleted);
                this.context.FileUploads.RemoveRange(list);
                this.context.SaveChanges();

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
