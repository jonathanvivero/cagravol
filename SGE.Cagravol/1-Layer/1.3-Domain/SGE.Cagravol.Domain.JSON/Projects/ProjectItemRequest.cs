using SGE.Cagravol.Application.Core.DataAnnotations;
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
    public class ProjectItemRequest
    {
        [Required(ErrorMessageResourceName = "UserNameIsRequired", ErrorMessageResourceType = typeof(CredentialsResources))]
        public string userName { get; set; }
        public long id { get; set; }
        [Required(ErrorMessageResourceName = "NameIsRequired", ErrorMessageResourceType = typeof(ProjectResources))]
        public string name { get; set; }
        public string description { get; set; }
        [Required(ErrorMessageResourceName = "StartDateIsRequired", ErrorMessageResourceType = typeof(ProjectResources))]
        public DateTime startDate { get; set; }
        [Required(ErrorMessageResourceName = "FinishDateIsRequired", ErrorMessageResourceType = typeof(ProjectResources))]
        public DateTime finishDate { get; set; }
        public DateTime extraChargeForSendingDate { get; set; }
        [Required(ErrorMessageResourceName = "LimitForSendingDateIsRequired", ErrorMessageResourceType = typeof(ProjectResources))]
        public DateTime limitForSendingDate { get; set; }
        [NumberFormat(ErrorMessageResourceName = "extraChargePercentageShoudBeAPercentage", ErrorMessageResourceType = typeof(ProjectResources))]
        public double extraChargePercentage { get; set; }
        public string notes { get; set; }
        [Required(ErrorMessageResourceName = "TotalStandsIsRequired", ErrorMessageResourceType = typeof(ProjectResources))]        
        public long totalStands { get; set; }
        [Required(ErrorMessageResourceName = "CodeIsRequired", ErrorMessageResourceType = typeof(ProjectResources))]        
        public string code { get; set; }
    }
}
