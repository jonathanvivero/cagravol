using SGE.Cagravol.Application.Core.Enums;
using SGE.Cagravol.Domain.Entities.Identity;
using SGE.Cagravol.Domain.Entities.Projects;
using SGE.Cagravol.Application.ServiceModel;
using SGE.Cagravol.Domain.Repositories.Base;
using SGE.Cagravol.Domain.Repositories.Projects;
using SGE.Cagravol.Infrastructure.Data.Repositories.Base;
using SGE.Cagravol.Presentation.Resources.Projects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGE.Cagravol.Application.Core.Enums.Common;
using SGE.Cagravol.Presentation.Resources.Common;

namespace SGE.Cagravol.Infrastructure.Data.Repositories.Projects
{
    public class ProjectRepository
        : BaseRepository<Project>, IProjectRepository
    {
        public ProjectRepository(ISGEContext context)
            : base(context)
        {
        }

        public IResultServiceModel<IEnumerable<Project>> GetActiveProjects()
        {
            IResultServiceModel<IEnumerable<Project>> rsm = new ResultServiceModel<IEnumerable<Project>>();
            try
            {
                var list = this.context.Projects.OrderByDescending(o => o.StartDate);

                return rsm.OnSuccess(list);
            }
            catch (Exception ex)
            {
                return rsm.OnException(ex);
            }
        }

        public IResultServiceModel<IEnumerable<Project>> GetActiveProjectsByUser(User user)
        {
            IResultServiceModel<IEnumerable<Project>> rsm = new ResultServiceModel<IEnumerable<Project>>();
            try
            {
                var authProjectIdList = this.context
                    .UserProjects
                    .Where(w => w.UserId == user.Id)
                    .Select(s => s.ProjectId);

                var list = this.context
                    .Projects
                    .Where(w => authProjectIdList.Contains(w.Id))
                    .OrderByDescending(o => o.StartDate);

                return rsm.OnSuccess(list);
            }
            catch (Exception ex)
            {
                return rsm.OnException(ex);
            }
        }

        public IResultModel AddGrantUsersToProject(IEnumerable<UserProject> upList)
        {
            IResultModel rm = new ResultModel();
            try
            {
                this.context.UserProjects.AddRange(upList);
                this.context.SaveChanges();
                return rm.OnSuccess();
            }
            catch (Exception ex)
            {
                return rm.OnException(ex);
            }
        }

        public IResultServiceModel<Project> FindByIdAndUser(long projectId, User user)
        {
            IResultServiceModel<Project> rm = new ResultServiceModel<Project>();

            try
            {
                var isAuthorized = this.context
                .UserProjects
                .Where(w => w.UserId == user.Id &&
                    w.ProjectId == projectId)
                .Any();

                if (isAuthorized)
                {
                    var prj = this.context.Projects.Find(projectId);
                    rm.OnSuccess(prj);
                }
                else
                {
                    rm.OnError(ProjectResources.UserHasNotEnoughPrivilegesOnThisProjectForThisAction, EnumErrorCode.INSUFFICIENT_PRIVILEGES);
                }

            }
            catch (Exception ex)
            {
                rm.OnException(ex);

            }


            return rm;
        }

        public async Task<IResultModel> DeleteProjectAndDependencies(long projectId, Func<Task<IResultModel>> deleteUsersFn)
        {
            var project = this.context.Projects.Find(projectId);
            return await this.DeleteProjectAndDependencies(project, deleteUsersFn);
        }

        public async Task<IResultModel> DeleteProjectAndDependencies(Project project, Func<Task<IResultModel>> deleteUsersFn)
        {
            IResultModel rm = new ResultModel();
            long projectId = project.Id;
            try
            {
                var cuzList = this.context.Customers.Where(w => w.ProjectId == projectId);
                var upList = this.context.UserProjects.Where(w => w.ProjectId == projectId);

                var fileList = this.context.Files.Where(w => w.Customer.ProjectId == projectId);
                var fileIdList = fileList.Select(s => s.Id);

                var fileHistoryNoteList = this.context.WFFileStateNotes.Where(w => fileIdList.Contains(w.WFEntityState.EntityId));
                var fileHistoryItemList = this.context.WFFileStates.Where(w => fileIdList.Contains(w.EntityId));

                //this.context
                //using (var transaction = this.context.CurrentDatabase.BeginTransaction())
                //{
                    try
                    {

                        this.context.WFFileStateNotes.RemoveRange(fileHistoryNoteList.ToList());
                        this.context.SaveChanges();

                        this.context.WFFileStates.RemoveRange(fileHistoryItemList.ToList());
                        this.context.SaveChanges();

                        this.context.Customers.RemoveRange(cuzList.ToList());
                        this.context.SaveChanges();

                        this.context.UserProjects.RemoveRange(upList.ToList());
                        this.context.SaveChanges();

                        this.context.Projects.Remove(project);
                        this.context.SaveChanges();

                        var rmDelUsers = await deleteUsersFn();

                        if (rmDelUsers.Success)
                        {
                            //transaction.Commit();
                            rm.OnSuccess();
                        }
                        else
                        {
                            //transaction.Rollback();
                            rm.OnError(rmDelUsers.ErrorMessage, rmDelUsers.ErrorCode);
                        }

                    }
                    catch (Exception ex)
                    {
                        //transaction.Rollback();
                        rm.OnException(ex);
                    }
                //}
            }
            catch (Exception ex)
            {
                rm.OnException(ex);
            }

            return rm;
        }

        public IResultModel ProjectByCodeExists(string code)
        {
            IResultModel rm = new ResultModel();

            try
            {
                var itExist = this.context
                    .Projects
                    .Where(w => w.Code == code)
                    .Any();
                if (itExist)
                {
                    rm.OnSuccess();
                }
                else 
                {
                    rm.OnError(ProjectResources.ErrorProjectCodeAlreadyExists);
                }

            }
            catch (Exception ex)
            {
                rm.OnException(ex);
            }

            return rm;
        }

        public IResultServiceModel<long> GetMostRecent() 
        {
            IResultServiceModel<long> rsm = new ResultServiceModel<long>();

            try
            {
                var item = this.context
                    .Projects
                    .OrderByDescending(o => o.Id)                    
                    .FirstOrDefault();

                if (item != null)
                {
                    rsm.OnSuccess(item.Id);
                }
                else 
                {
                    rsm.OnError(ErrorResources.NoProjectsAlreadyCreated, EnumErrorCode.ZERO_PROJECTS);
                }
            }
            catch (Exception ex)
            {
                rsm.OnException(ex);
                
            }

            return rsm;
            
        }

        public IResultServiceModel<Project> GetById(long id)
        {
            IResultServiceModel<Project> rsm = new ResultServiceModel<Project>();

            try
            {
                var item = this.context.Projects.Where(w => w.Id == id).FirstOrDefault();
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
    }
}
