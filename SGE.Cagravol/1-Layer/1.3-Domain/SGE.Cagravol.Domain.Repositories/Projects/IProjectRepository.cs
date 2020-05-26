using SGE.Cagravol.Domain.Entities.Identity;
using SGE.Cagravol.Domain.Entities.Projects;
using SGE.Cagravol.Application.ServiceModel;
using SGE.Cagravol.Domain.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Cagravol.Domain.Repositories.Projects
{
    public interface IProjectRepository
        : IBaseRepository<Project>
    {
        IResultServiceModel<IEnumerable<Project>> GetActiveProjects();
        IResultServiceModel<IEnumerable<Project>> GetActiveProjectsByUser(User user);
        IResultModel AddGrantUsersToProject(IEnumerable<UserProject> upList);
        IResultServiceModel<Project> FindByIdAndUser(long projectId, User user);
        Task<IResultModel> DeleteProjectAndDependencies(long projectId, Func<Task<IResultModel>> deleteUsersFn);
        Task<IResultModel> DeleteProjectAndDependencies(Project project, Func<Task<IResultModel>> deleteUsersFn);
        IResultModel ProjectByCodeExists(string code);
        IResultServiceModel<long> GetMostRecent();

        IResultServiceModel<Project> GetById(long id);
    }
}
