using SGE.Cagravol.Application.Core.Enums.Miscellaneous;
using SGE.Cagravol.Application.ServiceModel;
using SGE.Cagravol.Application.Services.Utils;
using SGE.Cagravol.Domain.Entities.Common;
using SGE.Cagravol.Domain.JSON.Alfred;
using SGE.Cagravol.Domain.Repositories.Common;
using SGE.Cagravol.Presentation.Resources.Miscellaneous;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Cagravol.Application.Services.Common
{
    public class MiscService
        : IMiscService
    {
        protected readonly IMiscRepository miscRepository;
        protected readonly IUtilService utilService;

        public MiscService(IMiscRepository miscRepository,
            IUtilService utilService)
        {
            this.miscRepository = miscRepository;
            this.utilService = utilService;
        }

        public IResultModel SetValue(string key, string value, DateTime limit)
        {
            return this.miscRepository.SetValue(key, value, limit);
        }

        public IResultModel SetValue(string key, string value)
        {
            return this.miscRepository.SetValue(key, value);
        }

        public IResultServiceModel<IEnumerable<Misc>> GetByKey(string key)
        {
            return this.miscRepository.GetByKey(key);
        }
        
        public IResultServiceModel<string> GetValue(string key)
        {
            return this.miscRepository.GetValue(key);
        }
        
        public IResultModel DeleteOutOfLimit()
        {
            return this.miscRepository.DeleteOutOfLimit();
        }

        public IResultServiceModel<string> GenerateDownloadExcelFileId(long projectId)
        {
            string excelGUID = this.utilService.GetGUID();
            IResultServiceModel<string> rsm = new ResultServiceModel<string>();

            var info = new ProjectExcelFileInfo() { GUID = excelGUID, ProjectId = projectId };
            var value = Newtonsoft.Json.JsonConvert.SerializeObject(info);

            var rm = this.miscRepository.SetValue(MiscKeyEnum.EXCEL_DOWNLOAD_GUID.ToString(), value);

            if (rm.Success)
            {
                rsm.OnSuccess(excelGUID);
            }
            else
            {
                rsm.OnError(rm.ErrorMessage, rm.ErrorCode);
            }

            return rsm;
        }

        public IResultServiceModel<long> CheckDownloadExcelFileId(string guid)
        {

            IResultServiceModel<long> rm = new ResultServiceModel<long>();

            var rsm = this.miscRepository.GetByKeyWithValue(MiscKeyEnum.EXCEL_DOWNLOAD_GUID.ToString(), guid);

            if (rsm.Success)
            {
                if (rsm.Value.Limit > DateTime.Now)
                {
                    var info = Newtonsoft.Json.JsonConvert.DeserializeObject<ProjectExcelFileInfo>(rsm.Value.Value);

                    rm.OnSuccess(info.ProjectId);
                }
                else
                { 
                    rm.OnError(MiscResources.TimeForDownloadThisExcelFileIsOut);
                }
            }
            else
            {
                rm.OnError(rm.ErrorMessage, rm.ErrorCode);
            }

            return rm;
        }

        public IResultModel RemoveExcelGUID(string guid)
        {

            IResultModel rm = new ResultModel();

            var rsm = this.miscRepository.GetByKeyWithValue(MiscKeyEnum.EXCEL_DOWNLOAD_GUID.ToString(), guid);


            if (rsm.Success)
            {
                var m = rsm.Value;
                
                this.miscRepository.Delete(m);
                rm = this.miscRepository.Save();                
            }
            else
            {
                rm.OnError(rm.ErrorMessage, rm.ErrorCode);
            }

            return rm;
        }

        
    }
}
