using SGE.Cagravol.Domain.Entities.Identity;
using SGE.Cagravol.Domain.Entities.Projects;
using SGE.Cagravol.Domain.JSON.Projects;
using SGE.Cagravol.Application.ServiceModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SGE.Cagravol.Application.Services.Projects
{
    public interface IProjectService
    {
        Task<IResultServiceModel<IEnumerable<Project>>> GetListByUserAsync(string userName);
        IResultServiceModel<IEnumerable<Project>> GetListByUser(string userName);
        Task<IResultServiceModel<Project>> GetProjectByIdAndUserAsync(string userName, long id);
        IResultServiceModel<Project> GetProjectByIdAndUser(string userName, long id);
        IResultServiceModel<Project> GetProjectById(long id);
        Task<IResultServiceModel<Project>> AddProjectByUserAsync(ProjectItemRequest model);
        IResultServiceModel<Project> AddProjectByUser(ProjectItemRequest model);
        Task<IResultServiceModel<Project>> EditProjectByUserAsync(ProjectItemRequest model);
        IResultServiceModel<Project> EditProjectByUser(ProjectItemRequest model);
        Task<IResultModel> DeleteProjectByUserAsync(ProjectItemRequest model);
        IResultModel DeleteProjectByUser(ProjectItemRequest model);
        IResultServiceModel<MemoryStream> GenerateExcel(long projectId);        

    }
}
