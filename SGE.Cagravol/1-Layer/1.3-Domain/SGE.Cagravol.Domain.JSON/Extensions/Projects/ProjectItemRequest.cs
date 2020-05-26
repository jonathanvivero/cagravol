using SGE.Cagravol.Application.Core.DataAnnotations;
using SGE.Cagravol.Domain.Entities.Projects;
using SGE.Cagravol.Presentation.Resources.Common;
using SGE.Cagravol.Presentation.Resources.Projects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Cagravol.Domain.JSON.Projects
{
    public static class ProjectItemRequestExtension
    {

        public static void CopyTo(this ProjectItemRequest _this, Project project)
        {
            project.Id = _this.id;
            project.Name = _this.name;
            project.Description = _this.description;
            project.StartDate = new DateTime(_this.startDate.Year, _this.startDate.Month, _this.startDate.Day, 0,0,0);
            project.FinishDate = new DateTime(_this.finishDate.Year, _this.finishDate.Month, _this.finishDate.Day, 23, 59, 59);
            project.ExtraChargeForSendingDate = new DateTime(_this.extraChargeForSendingDate.Year, _this.extraChargeForSendingDate.Month, _this.extraChargeForSendingDate.Day, 23, 59, 59);
            project.LimitForSendingDate = new DateTime(_this.limitForSendingDate.Year, _this.limitForSendingDate.Month, _this.limitForSendingDate.Day, 23, 59, 59); 
            project.ExtraChargePercentage = _this.extraChargePercentage;
            project.Notes = _this.notes;
            project.TotalStands = _this.totalStands;
            project.Code = _this.code.ToUpper();
        }     
    }
}
