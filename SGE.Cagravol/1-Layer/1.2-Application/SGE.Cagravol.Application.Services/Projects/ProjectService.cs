using SGE.Cagravol.Application.Core.Enums;
using SGE.Cagravol.Application.Services.Async;
using SGE.Cagravol.Application.Services.Identity;
using SGE.Cagravol.Domain.Entities.Customers;
using SGE.Cagravol.Domain.Entities.Identity;
using SGE.Cagravol.Domain.Entities.Projects;
using SGE.Cagravol.Domain.JSON.Projects;
using SGE.Cagravol.Application.ServiceModel;
using SGE.Cagravol.Domain.Repositories.Customers;
using SGE.Cagravol.Domain.Repositories.Projects;
using SGE.Cagravol.Presentation.Resources.Projects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGE.Cagravol.Application.Core.Enums.Common;
using ClosedXML.Excel;
using System.IO;
using System.Web;
using SGE.Cagravol.Domain.Repositories.History;

namespace SGE.Cagravol.Application.Services.Projects
{
    public class ProjectService
        : IProjectService
    {
        private readonly IProjectRepository projectRepository;
        private readonly IAccountService accountService;
        private readonly IAsyncService asyncService;
        private readonly ICustomerRepository customerRepository;
        private readonly IWFFileStateRepository fileStateRepository;

        public ProjectService(IProjectRepository projectRepository,
            IAccountService accountService,
            IAsyncService asyncService,
            ICustomerRepository customerRepository,
            IWFFileStateRepository fileStateRepository
            )
        {
            this.projectRepository = projectRepository;
            this.accountService = accountService;
            this.asyncService = asyncService;
            this.customerRepository = customerRepository;
            this.fileStateRepository = fileStateRepository;
        }

        public async Task<IResultServiceModel<IEnumerable<Project>>> GetListByUserAsync(string userName)
        {

            IResultServiceModel<IEnumerable<Project>> rsm = new ResultServiceModel<IEnumerable<Project>>();
            //get the user
            User user = null;
            string roleFlags = string.Empty;

            try
            {
                user = await this.accountService.FindUserByUserNameOrEmailAsync(userName);
                roleFlags = this.accountService.GetRolesKeyFlagsForUser(user.Id);

                //depending on the user, will see the whole list, part of it, or nothing
                if (roleFlags.Trim() == "C")
                {
                    rsm.OnSuccess(Enumerable.Empty<Project>());
                    rsm.Message = ProjectResources.CustomersCannotListProjects;
                }
                else
                {
                    var rmsList = this.projectRepository.GetActiveProjectsByUser(user);
                    if (rmsList.Success)
                    {
                        rsm.OnSuccess(rmsList.Value);
                    }
                    else
                    {
                        rsm.OnError(rmsList.ErrorMessage, rmsList.ErrorCode);
                    }
                }
            }
            catch (Exception ex)
            {
                rsm.OnException(ex);
            }

            return rsm;
        }

        public IResultServiceModel<IEnumerable<Project>> GetListByUser(string userName)
        {
            return this.asyncService.RunSync(() => GetListByUserAsync(userName));
        }


        public async Task<IResultServiceModel<Project>> GetProjectByIdAndUserAsync(string userName, long id)
        {
            IResultServiceModel<Project> rsm = new ResultServiceModel<Project>();
            //get the user
            User user = null;
            string roleFlags = string.Empty;

            try
            {
                user = await this.accountService.FindUserByUserNameOrEmailAsync(userName);
                roleFlags = this.accountService.GetRolesKeyFlagsForUser(user.Id);

                //depending on the user, will see the whole list, part of it, or nothing
                if (roleFlags.Trim() == "C")
                {
                    rsm.OnSuccess(new Project());
                    rsm.Message = ProjectResources.CustomersCannotGetProjects;
                }
                else
                {
                    var rmsList = this.projectRepository.Find(id);
                    if (rmsList.Success)
                    {
                        rsm.OnSuccess(rmsList.Value);
                    }
                    else
                    {
                        rsm.OnError(rmsList.ErrorMessage, rmsList.ErrorCode);
                    }
                }
            }
            catch (Exception ex)
            {
                rsm.OnException(ex);
            }

            return rsm;
        }
        public IResultServiceModel<Project> GetProjectByIdAndUser(string userName, long id)
        {
            return this.asyncService.RunSync(() => GetProjectByIdAndUserAsync(userName, id));
        }


        public async Task<IResultServiceModel<Project>> AddProjectByUserAsync(ProjectItemRequest model)
        {
            IResultServiceModel<Project> rsm = new ResultServiceModel<Project>();

            //get the user
            User user = null;
            string roleFlags = string.Empty;

            try
            {
                user = await this.accountService.FindUserByUserNameOrEmailAsync(model.userName);

                /* 1st => data validation */
                var rm = this.CheckProjectForAdding(model);
                if (rm.Success)
                {
                    var project = new Project();
                    model.CopyTo(project);

                    this.projectRepository.Add(project);
                    var rmAdd = this.projectRepository.Save();

                    if (rmAdd.Success)
                    {
                        rmAdd = this.CompleteProjectCreation(project, model.userName);
                        if (rmAdd.Success)
                        {
                            rmAdd = this.GrantUsersInProject(project, user);

                            if (rmAdd.Success)
                            {
                                rsm.OnSuccess(project);
                            }
                            else
                            {
                                rsm.OnError(rmAdd.ErrorMessage, rmAdd.ErrorCode);
                            }
                        }
                        else
                        {
                            rsm.OnError(rmAdd.ErrorMessage, rmAdd.ErrorCode);
                        }
                    }
                    else
                    {
                        rsm.OnError(rmAdd.ErrorMessage, rmAdd.ErrorCode);
                    }

                }
                else
                {
                    rsm.OnError(rm.ErrorMessage, rm.ErrorCode);
                }
            }
            catch (Exception ex)
            {
                rsm.OnException(ex);
            }

            return rsm;
        }

        public IResultServiceModel<Project> AddProjectByUser(ProjectItemRequest model)
        {
            return this.asyncService.RunSync(() => AddProjectByUserAsync(model));
        }

        public async Task<IResultServiceModel<Project>> EditProjectByUserAsync(ProjectItemRequest model)
        {
            IResultServiceModel<Project> rsm = new ResultServiceModel<Project>();

            //get the user
            User user = null;
            string roleFlags = string.Empty;
            Project project = null;
            long oldTotalStands = 0;
            string oldProjectCode = string.Empty;

            try
            {
                user = await this.accountService.FindUserByUserNameOrEmailAsync(model.userName);
                var rmProject = this.projectRepository.FindByIdAndUser(model.id, user);

                if (!rmProject.Success)
                {
                    return rsm.OnError(rmProject.ErrorMessage, rmProject.ErrorCode);
                }
                else
                {
                    project = rmProject.Value;
                }

                /* 1st => data validation */
                var rm = this.CheckProjectForEditing(model, project);
                if (rm.Success)
                {

                    oldTotalStands = project.TotalStands;
                    oldProjectCode = project.Code;

                    model.CopyTo(project);

                    this.projectRepository.Update(project);
                    var rmOp = this.projectRepository.Save();

                    if (rmOp.Success)
                    {
                        rmOp = this.CompleteProjectEdition(project, model.userName, oldTotalStands);
                        if (rmOp.Success)
                        {
                            rsm.OnSuccess(project);
                        }
                        else
                        {
                            rsm.OnError(rmOp.ErrorMessage, rmOp.ErrorCode);
                        }


                        if (oldProjectCode != project.Code)
                        {
                            var rmReProject = this.customerRepository.GetListByProjectId(project.Id);
                            if (rmReProject.Success)
                            {
                                var cuzList = rmReProject.Value.ToList();

                                //var cuzList = project.Customers.ToList();
                                foreach (var cuz in cuzList)
                                {
                                    cuz.SignUpCode = "{0}-{1}".sf(project.Code.ToUpper(), cuz.SpaceNumber);
                                    this.customerRepository.Update(cuz);
                                    var rmcuzSave = this.customerRepository.Save();


                                }
                            }
                        }

                    }
                    else
                    {
                        rsm.OnError(rmOp.ErrorMessage, rmOp.ErrorCode);
                    }

                }
                else
                {
                    rsm.OnError(rm.ErrorMessage, rm.ErrorCode);
                }
            }
            catch (Exception ex)
            {
                rsm.OnException(ex);
            }

            return rsm;
        }

        public IResultServiceModel<Project> EditProjectByUser(ProjectItemRequest model)
        {
            return this.asyncService.RunSync(() => EditProjectByUserAsync(model));
        }
        public async Task<IResultModel> DeleteProjectByUserAsync(ProjectItemRequest model)
        {
            IResultModel rsm = new ResultModel();

            //get the user
            User user = null;
            string roleFlags = string.Empty;
            Project project = null;            

            try
            {
                user = await this.accountService.FindUserByUserNameOrEmailAsync(model.userName);
                var rmProject = this.projectRepository.FindByIdAndUser(model.id, user);

                if (!rmProject.Success)
                {
                    return rsm.OnError(rmProject.ErrorMessage, rmProject.ErrorCode);
                }
                else
                {
                    project = rmProject.Value;
                }

                var rmUList = this.customerRepository.GetUserIdListByProjectId(project.Id);
                if (rmUList.Success)
                {
                    var userIdList = rmUList.Value.ToList();
                    Func<Task<IResultModel>> deleteUsersFn = new Func<Task<IResultModel>>(() => this.accountService.RemoveListOfUsersByProject(userIdList));

                    var rmOp = await this.projectRepository.DeleteProjectAndDependencies(project, deleteUsersFn);
                
                    if (rmOp.Success)
                    {
                        rsm.OnSuccess();
                    }
                    else
                    {
                        rsm.OnError(rmOp.ErrorMessage, rmOp.ErrorCode);
                    }                    
                }

            }
            catch (Exception ex)
            {
                rsm.OnException(ex);
            }

            return rsm;
        }
        public IResultModel DeleteProjectByUser(ProjectItemRequest model)
        {
            return this.asyncService.RunSync(() => DeleteProjectByUserAsync(model));
        }

        public IResultServiceModel<Project> GetProjectById(long id)
        {
            return this.projectRepository.GetById(id);
        }
        
        private IResultModel CheckProjectForAdding(ProjectItemRequest model)
        {
            IList<string> errors = new List<string>();
            IResultModel rm = new ResultModel();

            //Get Existing project with the same Code;

            try
            {
                /*
                 * Validations are: 
                 * TotalStands > 1
                 * Code between min 4  chars
                 * Dates Correlation
                 */
                var rmPrj = this.projectRepository.ProjectByCodeExists(model.code.ToUpper());
                if (rmPrj.Success)
                {
                    errors.Add(ProjectResources.ErrorProjectCodeAlreadyExists);
                }


                if (model.totalStands <= 0)
                {
                    errors.Add("Total Stands Min 1");
                }

                if (model.code.Length < 4)
                {
                    errors.Add("Code Length Min 4");
                }

                if (model.startDate > model.finishDate)
                {
                    errors.Add("Start Date Before Finish Date");
                }

                if (errors.Any())
                {
                    rm.OnError(string.Join("", errors.Select(s => string.Format("<li>{0}</li>", s))), EnumErrorCode.VALIDATION_ERROR);
                }
                else
                {
                    rm.OnSuccess();
                }
            }
            catch (Exception ex)
            {
                rm.OnException(ex);
            }
            return rm;
        }

        private IResultModel CheckProjectForEditing(ProjectItemRequest model, Project project)
        {
            IList<string> errors = new List<string>();
            IResultModel rm = new ResultModel();

            try
            {
                /*
                 * Validations are: 
                 * TotalStands > 1
                 * Code between min 4  chars
                 * Dates Correlation
                 */

                if (model.totalStands <= 0)
                {
                    errors.Add("Total Stands Min 1");
                }
                else if (model.totalStands < project.TotalStands)
                {
                    errors.Add("Cannot Reduce Total Stands");
                }

                if (model.code.Length < 4)
                {
                    errors.Add("Code Length Min 4");
                }

                if (model.startDate > model.finishDate)
                {
                    errors.Add("Start Date Before Finish Date");
                }

                if (errors.Any())
                {
                    rm.OnError(string.Join("", errors.Select(s => string.Format("<li>{0}</li>", s))), EnumErrorCode.VALIDATION_ERROR);
                }
                else
                {
                    rm.OnSuccess();
                }
            }
            catch (Exception ex)
            {
                rm.OnException(ex);
            }
            return rm;
        }

        /// <summary>
        /// Complete all stuff for project creation: Space for Common, space for customers
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        private IResultModel CompleteProjectCreation(Project project, string userEmail)
        {
            IList<Customer> customers = new List<Customer>();
            int x = 0;
            string code = "{0}-{1}".sf(project.Code.Trim().ToUpper(), 0);

            try
            {
                //add the generic one
                customers.Add(new Customer()
                {
                    BillAddress = string.Empty,
                    BillCity = string.Empty,
                    BillCountry = string.Empty,
                    BillDataTypeId = 1,
                    BillLegalIdentification = string.Empty,
                    BillPostalCode = string.Empty,
                    Email = userEmail,
                    IsGeneric = true,
                    Name = ProjectResources.CustomerCommonSpace,
                    Notes = string.Empty,
                    PasswordHash = string.Empty,
                    ProjectId = project.Id,
                    SignUpCode = code,
                    UserId = null,
                    SpaceNumber = 0,
                    Reserved = true, 
                    Registered = true
                });

                for (x = 1; x <= project.TotalStands; x++)
                {
                    code = "{0}-{1}".sf(project.Code.Trim().ToUpper(), x);

                    customers.Add(new Customer()
                    {
                        BillAddress = string.Empty,
                        BillCity = string.Empty,
                        BillCountry = string.Empty,
                        BillDataTypeId = 1,
                        BillLegalIdentification = string.Empty,
                        BillPostalCode = string.Empty,
                        Email = null,
                        IsGeneric = false,
                        Name = ProjectResources.CustomerSpace_Format.sf(x),
                        Notes = string.Empty,
                        PasswordHash = string.Empty,
                        ProjectId = project.Id,
                        SignUpCode = code,
                        UserId = null,
                        SpaceNumber = x,
                        Reserved = false,
                        Registered = false
                    });
                }

                return this.customerRepository.AddCustomerForProject(customers);
            }
            catch (Exception ex)
            {
                return new ResultModel().OnException(ex);
            }
        }

        /// <summary>
        /// Complete all stuff for project creation: Space for Common, space for customers
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        private IResultModel CompleteProjectEdition(Project project, string userEmail, long oldTotalStands)
        {   
            IList<Customer> customers = new List<Customer>();
            long x = 0;
            string code = "{0}-{1}".sf(project.Code.Trim().ToUpper(), 0);

            try
            {
                for (x = oldTotalStands; x < project.TotalStands; x++)
                {
                    code = "{0}-{1}".sf(project.Code.Trim().ToUpper(), x);

                    customers.Add(new Customer()
                    {
                        BillAddress = string.Empty,
                        BillCity = string.Empty,
                        BillCountry = string.Empty,
                        BillDataTypeId = 1,
                        BillLegalIdentification = string.Empty,
                        BillPostalCode = string.Empty,
                        Email = null,
                        IsGeneric = true,
                        Name = ProjectResources.CustomerSpace_Format.sf(x),
                        Notes = string.Empty,
                        PasswordHash = string.Empty,
                        ProjectId = project.Id,
                        SignUpCode = code,
                        UserId = null,
                        SpaceNumber = (int)x,
                        Reserved = false,
                        Registered = false
                    });
                }

                return this.customerRepository.AddCustomerForProject(customers);
            }
            catch (Exception ex)
            {
                return new ResultModel().OnException(ex);
            }
        }

        /// <summary>
        /// Grant Management Permissions for all users (almost Customers)
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        private IResultModel GrantUsersInProject(Project project, User user)
        {
            IList<UserProject> upList = new List<UserProject>();
            var userList = this.accountService.GetHomeUsers();            

            try
            {                
                foreach (var u in userList)
                {
                    upList.Add(new UserProject()
                    {
                        ProjectId = project.Id,
                        IsOwner = (u.Id == user.Id),
                        IsAuthorized = true,
                        UserId = u.Id
                    });
                }

                return this.projectRepository.AddGrantUsersToProject(upList);
            }
            catch (Exception ex)
            {
                return new ResultModel().OnException(ex);
            }
        }


        /// <summary>
        /// General un archivo excel con toda la actividad del Evento
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public IResultServiceModel<MemoryStream> GenerateExcel(long projectId)
        {
            IResultServiceModel<MemoryStream> rsm = new ResultServiceModel<MemoryStream>();
            Project pr = null;
            int row = 1, col = 1;

            
                

            MemoryStream excelStream = new MemoryStream();

            XLWorkbook workbook = new XLWorkbook();

            var rmProj = this.projectRepository.GetById(projectId);
            if (rmProj.Success)
            {
                pr = rmProj.Value;

                IXLWorksheet ws = workbook.Worksheets.Add(pr.Name);


                ws.Cell("B2").Style.Font.Bold = true;
                ws.Cell("B2").Value = ProjectResources.Name;
                ws.Cell("C3").Value = pr.Name;

                ws.Cell("B4").Style.Font.Bold = true;
                ws.Cell("B4").Value = ProjectResources.ProjectCode;
                ws.Cell("C5").Value = pr.Code.ToUpper();
                
                ws.Cell("B6").Style.Font.Bold = true;
                ws.Cell("B6").Value = ProjectResources.TotalStands;
                ws.Cell("C7").Value = pr.TotalStands;

                ws.Cell("B8").Style.Font.Bold = true;
                ws.Cell("B8").Value = ProjectResources.Description;
                ws.Cell("C9").Value = pr.Description;

                ws.Cell("B10").Style.Font.Bold = true;
                ws.Cell("B10").Value = ProjectResources.StartDate;
                ws.Cell("C11").Value = pr.StartDate.ToLongDateString();

                ws.Cell("B12").Style.Font.Bold = true;
                ws.Cell("B12").Value = ProjectResources.EndDate;
                ws.Cell("C13").Value = pr.FinishDate.ToLongDateString();

                ws.Cell("B14").Style.Font.Bold = true;
                ws.Cell("B14").Value = ProjectResources.NoChargeLimitDate;
                ws.Cell("C15").Value = pr.ExtraChargeForSendingDate.ToLongDateString();

                ws.Cell("B16").Style.Font.Bold = true;
                ws.Cell("B16").Value = ProjectResources.LimitDate;
                ws.Cell("C17").Value = pr.LimitForSendingDate.ToLongDateString();

                ws.Cell("B18").Style.Font.Bold = true;
                ws.Cell("B18").Value = ProjectResources.ChargePercentage;
                ws.Cell("C19").Value = "{0:C}".sf(pr.ExtraChargePercentage).Replace("€","%");


                ws.Cell("B22").Style.Font.Bold = true; 
                ws.Cell("C22").Value = ProjectResources.Excel_StandsTitle;

                row = 23;

                foreach (Customer cuz in pr.Customers)
                {
                    if (cuz.IsGeneric)
                    {
                        ws.Cell(D(row++)).Value = ProjectResources.Excel_GeneralSpaceTitle;                                                
                        ws.Cell(E(row++)).Value = ProjectResources.Excel_FilesTitle;

                        this.ExcelListCustomerFiles(ws, cuz, ref row);
                    }
                    else 
                    {
                        ws.Cell(D(row++)).Value = cuz.SignUpCode;

                        if (!string.IsNullOrWhiteSpace(cuz.Email))
                        {
                            ws.Cell(E(row)).Style.Font.Bold = true;
                            ws.Cell(E(row++)).Value = cuz.Email;
                            ws.Cell(E(row++)).Value = ProjectResources.Excel_FilesTitle;

                            this.ExcelListCustomerFiles(ws, cuz, ref row);
                        }
                        else
                        {
                            ws.Cell(E(row)).Style.Font.Italic = true;
                            ws.Cell(E(row++)).Value = ProjectResources.Excel_NoCustomerAssigned;
                            ws.Cell(E(row)).Style.Font.Italic = true;
                            ws.Cell(E(row++)).Value = ProjectResources.Excel_NoFilesTitle;                            
                        }                    
                    }
                }

                ws.Column(3).AdjustToContents();
                ws.Column(4).AdjustToContents();
                ws.Column(8).AdjustToContents();
                ws.Column(9).AdjustToContents();

                workbook.SaveAs(excelStream);
                excelStream.Position = 0;
                rsm.OnSuccess(excelStream);
            }
            else 
            {
                IXLWorksheet ws = workbook.Worksheets.Add("Error");

                ws.Cell("B2").Style.Font.Bold = true;
                ws.Cell("B2").Value = ProjectResources.ErrorOnExportationTitle;

                ws.Cell("B3").Value = ProjectResources.ErrorOnExportationContent;
                ws.Cell("B4").Value = rmProj.ErrorMessage;

                workbook.SaveAs(excelStream);

                rsm.OnSuccess(excelStream);
            }

            return rsm;
        }


        private void ExcelListCustomerFiles(IXLWorksheet ws, Customer cuz, ref int row)
        {

            var dtFormat = "dd/MM/yyyy HH:mm:ss";

            if (cuz.Files.Any())
            {
                

                foreach (var file in cuz.Files)
                {
                    ws.Cell(F(row)).Style.Font.Bold = true;
                    ws.Cell(F(row++)).Value = file.Name;

                    ws.Cell(G(row)).Style.Font.Italic = true;
                    ws.Cell(G(row)).Hyperlink = new XLHyperlink(file.URL);
                    ws.Cell(G(row++)).Value = file.FileName;

                    ws.Cell(G(row++)).Value = ProjectResources.Excel_History;

                    var rmHlist = fileStateRepository.GetListByFileId(file.Id);

                    if (rmHlist.Success)
                    {
                        var hlist = rmHlist.Value;
                        ws.Cell(H(row++)).Value = ProjectResources.Excel_History;

                        foreach (var hist in hlist)
                        {
                            ws.Cell(H(row)).Style.Font.FontColor = XLColor.White;
                            ws.Cell(H(row)).Style.Fill.BackgroundColor = XLColor.Black;
                            ws.Cell(H(row)).Value = hist.WFState.Name;
                            ws.Cell(I(row++)).Value = hist.TS.ToString(dtFormat);

                            foreach (var note in hist.Notes)
                            {
                                ws.Cell(H(row)).Value = note.User.Email;
                                ws.Cell(I(row)).Value = note.TS.ToString(dtFormat);
                                ws.Cell(J(row++)).Value = note.Comment;
                            }
                        }
                    }
                    else
                    {
                        ws.Cell(H(row++)).Value = ProjectResources.Excel_NoHistory;
                    }

                }
            }
            else
            {
                ws.Cell(E(row)).Style.Font.Italic = true;
                ws.Cell(E(row++)).Value = ProjectResources.Excel_NoFilesTitle;                            
            }                    
        }


        private string cll(string _col, int _row) { return "{0}{1}".sf(_col, _row); }
        private string A (int _row) { return cll("A", _row); }
        private string B (int _row) { return cll("B", _row); }
        private string C (int _row) { return cll("C", _row); }
        private string D (int _row) { return cll("D", _row); }
        private string E (int _row) { return cll("E", _row); }
        private string F (int _row) { return cll("F", _row); }
        private string G (int _row) { return cll("G", _row); }
        private string H (int _row) { return cll("H", _row); }
        private string I(int _row) { return cll("I", _row); }
        private string J(int _row) { return cll("J", _row); }
        private string K(int _row) { return cll("K", _row); }


    }
}
