using SGE.Cagravol.Application.Services.Async;
using SGE.Cagravol.Application.Services.Identity;
using SGE.Cagravol.Domain.Entities.Identity;
using SGE.Cagravol.Domain.JSON.Panels;
using SGE.Cagravol.Domain.POCO.NgRoutes;
using SGE.Cagravol.Domain.POCO.Panels;
using SGE.Cagravol.Application.ServiceModel;
using SGE.Cagravol.Domain.Repositories.Customers;
using SGE.Cagravol.Presentation.Resources.Common;
using SGE.Cagravol.Presentation.Resources.Panels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Cagravol.Application.Services.Panels
{
    public class PanelService
        : IPanelService
    {
        private readonly ICustomerRepository customerRepository;
        private readonly IAccountService accountService;
        private readonly IAsyncService asyncService;

        public PanelService(ICustomerRepository customerRepository,
            IAccountService accountService,
            IAsyncService asyncService)
        {
            this.customerRepository = customerRepository;
            this.accountService = accountService;
            this.asyncService = asyncService;
        }

        /// <summary>
        /// Get the Panels configuration for the user (passed by parameter)
        /// </summary>
        /// <param name="userName">The logged-in User Name</param>
        /// <returns></returns>
        public async Task<IResultServiceModel<PanelConfigurationResponse>> GetConfigurationCurrentUserAsync(string userName)
        {
            IResultServiceModel<PanelConfigurationResponse> rsm = new ResultServiceModel<PanelConfigurationResponse>();
            //get the user
            User user = null;
            string roleFlags = string.Empty;
            PanelItem[] positions = new PanelItem[] { new PanelItem(), new PanelItem(), new PanelItem(), new PanelItem(), new PanelItem(), new PanelItem(), new PanelItem(), new PanelItem(), new PanelItem() };
            int indexPosition = -1;
            IList<NgRouteProvider> routes = new List<NgRouteProvider>();
            PanelConfigurationResponse pcr = new PanelConfigurationResponse();

            try
            {
                user = await this.accountService.FindUserByUserNameOrEmailAsync(userName);
                roleFlags = this.accountService.GetRolesKeyFlagsForUser(user.Id);            

                if (roleFlags.Contains('M'))
                {
                    this.GetPanelConfigurationForManager(ref routes, ref positions, ref indexPosition);
                }

                //Create ProjectPanel => Is Supervisor
                if (roleFlags.Contains('S'))
                {
                    this.GetPanelConfigurationForSupervisor(ref routes, ref positions, ref indexPosition);
                }

                //Create ProjectPanel => Is Organizer
                if (roleFlags.Contains('O'))
                {
                    this.GetPanelConfigurationForOrganizer(ref routes, ref positions, ref indexPosition);
                }

                //Create ProjectPanel => Is Customer
                if (roleFlags.Contains('C'))
                {
                    this.GetPanelConfigurationForCustomer(ref routes, ref positions, ref indexPosition);
                }

                for (var x = 0; x <= indexPosition; x++)
                {
                    switch (x)
                    {
                        case 0:
                            pcr.panelInfo = new PanelInfo();
                            pcr.panelInfo.top = new PanelRow();
                            pcr.panelInfo.top.left = positions[x];
                            break;
                        case 1:
                            pcr.panelInfo.top.center = positions[x];
                            break;
                        case 2:
                            pcr.panelInfo.top.right = positions[x];
                            break;
                        case 3:
                            pcr.panelInfo.middle = new PanelRow();
                            pcr.panelInfo.middle.left = positions[x];
                            break;
                        case 4:
                            pcr.panelInfo.middle.center = positions[x];
                            break;
                        case 5:
                            pcr.panelInfo.middle.right = positions[x];
                            break;
                        case 6:
                            pcr.panelInfo.bottom = new PanelRow();
                            pcr.panelInfo.bottom.left = positions[x];
                            break;
                        case 7:
                            pcr.panelInfo.bottom.center = positions[x];
                            break;
                        case 8:
                            pcr.panelInfo.bottom.right = positions[x];
                            break;
                        default:
                            break;
                    }
                }

                pcr.routes = routes;

                rsm.OnSuccess(pcr);
            }
            catch (Exception ex)
            {
                rsm.OnException(ex);
            }

            return rsm;
        }

        /// <summary>
        /// Get the Panels configuration for the user (passed by parameter)
        /// </summary>
        /// <param name="userName">The logged-in User Name</param>
        /// <returns></returns>
        public IResultServiceModel<PanelConfigurationResponse> GetConfigurationCurrentUser(string userName)
        {
            return this.asyncService.RunSync<IResultServiceModel<PanelConfigurationResponse>>(() => GetConfigurationCurrentUserAsync(userName));
        }

        #region Private Methods
        private void GetPanelConfigurationForManager(ref IList<NgRouteProvider> routes, ref PanelItem[] positions, ref int indexPosition)
        {
            try
            {
                routes.Add(new NgRouteProvider()
                {
                    when = "/project/list",
                    controller = "ListProjectController",
                    templateUrl = "/app/views/projects/list.html"
                });

                routes.Add(new NgRouteProvider()
                {
                    when = "/project/manage/:id",
                    controller = "ManageProjectController",
                    templateUrl = "/app/views/projects/manage.html"
                });

                routes.Add(new NgRouteProvider()
                {
                    when = "/project/create",
                    controller = "EditProjectController",
                    templateUrl = "/app/views/projects/edit.html"
                });

                routes.Add(new NgRouteProvider()
                {
                    when = "/project/edit/:id",
                    controller = "EditProjectController",
                    templateUrl = "/app/views/projects/edit.html"
                });

                routes.Add(new NgRouteProvider()
                {
                    when = "/project/delete/:id",
                    controller = "DeleteProjectController",
                    templateUrl = "/app/views/projects/delete.html"
                });

                routes.Add(new NgRouteProvider()
                {
                    when = "/project/activity/:id",
                    controller = "ViewActivityProjectController",
                    templateUrl = "/app/views/projects/viewactivity.html"
                });

                indexPosition++;
                positions[indexPosition].title = PanelInfoResources.CreateProject;
                positions[indexPosition].url = "/app/views/panels/super/createproject.html";
                positions[indexPosition].onLoad = "createProjectPanelLoad";
                indexPosition++;
                positions[indexPosition].title = PanelInfoResources.ManageLastProject;
                positions[indexPosition].url = "/app/views/panels/super/managelastproject.html";
                positions[indexPosition].onLoad = "manageLastProjectPanelLoad";
                indexPosition++;
                positions[indexPosition].title = PanelInfoResources.ViewActivity;
                positions[indexPosition].url = "/app/views/panels/super/viewactivity.html";
                positions[indexPosition].onLoad = "viewActivityPanelLoad";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private void GetPanelConfigurationForSupervisor(ref IList<NgRouteProvider> routes, ref PanelItem[] positions, ref int indexPosition)
        {
        }

        private void GetPanelConfigurationForOrganizer(ref IList<NgRouteProvider> routes, ref PanelItem[] positions, ref int indexPosition)
        {
        }

        private void GetPanelConfigurationForCustomer(ref IList<NgRouteProvider> routes, ref PanelItem[] positions, ref int indexPosition)
        {
        }

        #endregion
    }
}
