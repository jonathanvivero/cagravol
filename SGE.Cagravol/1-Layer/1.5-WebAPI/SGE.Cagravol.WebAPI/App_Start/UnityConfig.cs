using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using Microsoft.Practices.Unity;
using SGE.Cagravol.Application.Core.Helpers;
using SGE.Cagravol.Application.Services.Async;
using SGE.Cagravol.Application.Services.Common;
using SGE.Cagravol.Application.Services.Customers;
using SGE.Cagravol.Application.Services.Files;
using SGE.Cagravol.Application.Services.History;
using SGE.Cagravol.Application.Services.Identity;
using SGE.Cagravol.Application.Services.Logs;
using SGE.Cagravol.Application.Services.Mailing;
using SGE.Cagravol.Application.Services.Panels;
using SGE.Cagravol.Application.Services.Projects;
using SGE.Cagravol.Application.Services.Utils;
using SGE.Cagravol.Application.Services.WebApi;
using SGE.Cagravol.Application.Services.Workflows;
using SGE.Cagravol.Domain.Entities.Identity;
using SGE.Cagravol.Domain.Repositories.Common;
using SGE.Cagravol.Domain.Repositories.Customers;
using SGE.Cagravol.Domain.Repositories.Files;
using SGE.Cagravol.Domain.Repositories.History;
//using SGE.Cagravol.Domain.Repositories.History;
using SGE.Cagravol.Domain.Repositories.Projects;
using SGE.Cagravol.Domain.Repositories.Workflows;
using SGE.Cagravol.Infrastructure.Data;
using SGE.Cagravol.Infrastructure.Data.Repositories.Common;
using SGE.Cagravol.Infrastructure.Data.Repositories.Customers;
using SGE.Cagravol.Infrastructure.Data.Repositories.Files;
//using SGE.Cagravol.Infrastructure.Data.Repositories.History;
using SGE.Cagravol.Infrastructure.Data.Repositories.Projects;
using SGE.Cagravol.Infrastructure.Data.Repositories.Workflows;
using SGE.Cagravol.Insfractructure.Data.Repositories.Customers;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Routing;

namespace SGE.Cagravol.WebAPI.App_Start
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        /// <summary>
        /// Gets the configured Unity container.
        /// </summary>
        public static IUnityContainer GetConfiguredContainer()
        {
            return container.Value;
        }
        #endregion

        /// <summary>Registers the type mappings with the Unity container.</summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>There is no need to register concrete types such as controllers or API controllers (unless you want to 
        /// change the defaults), as Unity allows resolving a concrete type even if it was not previously registered.</remarks>
        public static void RegisterTypes(IUnityContainer container)
        {

            InjectionConstructor accountInjectionConstructor = new InjectionConstructor(new SGEContext());

            // NOTE: To load from web.config uncomment the line below. Make sure to add a Microsoft.Practices.Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            // TODO: Register your types here
            // container.RegisterType<IProductRepository, ProductRepository>();

            container.RegisterType<ISGEContext, SGEContext>();
            container.RegisterType<IdentityDbContext<User>, SGEContext>();

            //Identity Stuff
            container.RegisterType<IAccountService, AccountService>();
            //container.RegisterType<IUserStore<User, string>, UserStore<User, Role, string, UserLogin, UserRole, UserClaim>>();
            //container.RegisterType<IRoleStore<Role, string>, RoleStore<Role, string, UserRole>>();
            //container.RegisterType<UserManager<User, string>>(new Microsoft.Practices.Unity.InjectionFactory(i =>
            //{
            //    return new UserManager<User, string>(new UserStore<User, Role, string, UserLogin, UserRole, UserClaim>(new SGEContext()));
            //}));
            //container.RegisterType<RoleManager<Role, string>>(new Microsoft.Practices.Unity.InjectionFactory(i =>
            //{
            //    return new RoleManager<Role, string>(new RoleStore<Role, string, UserRole>(new SGEContext()));
            //}));

            container.RegisterType<IUserStore<User, string>, UserStore<User>>(accountInjectionConstructor);
            container.RegisterType<IRoleStore<Role, string>, RoleStore<Role>>(accountInjectionConstructor);
            container.RegisterType<IRoleStore<IdentityRole, string>, RoleStore<IdentityRole>>(accountInjectionConstructor);


            //container.RegisterType<IUserStore<User, string>>(new Microsoft.Practices.Unity.InjectionFactory(i =>
            //{
            //    return new UserStore<User>(new SGEContext());
            //}));
            //container.RegisterType<IRoleStore<Role, string>>(new Microsoft.Practices.Unity.InjectionFactory(i =>
            //{
            //    return new RoleStore<Role>(new SGEContext());
            //}));

            container.RegisterType<UserManager<User, string>>(new Microsoft.Practices.Unity.InjectionFactory(i =>
            {
                var um = new UserManager<User>(new UserStore<User>(new SGEContext()));
                um.UserValidator = new UserValidator<User>(um){ AllowOnlyAlphanumericUserNames = false, RequireUniqueEmail = true};

                return um;
            }));
            container.RegisterType<RoleManager<Role, string>>(new Microsoft.Practices.Unity.InjectionFactory(i =>
            {
                return new RoleManager<Role>(new RoleStore<Role>(new SGEContext()));
            }));

            container.RegisterType<IAuthenticationManager>(new Microsoft.Practices.Unity.InjectionFactory(i =>
            {
                return HttpContext.Current.GetOwinContext().Authentication;
            }));
            //container.RegisterType<IOwinContext>(new Microsoft.Practices.Unity.InjectionFactory(i =>
            //{
            //	return HttpContext.Current.GetOwinContext();
            //}));

            //Repositories
            container.RegisterType<IAppConfigurationRepository, AppConfigurationRepository>();
            container.RegisterType<ICustomerRepository, CustomerRepository>();
            container.RegisterType<IBillDataTypeRepository, BillDataTypeRepository>();
            container.RegisterType<IFileTypeRepository, FileTypeRepository>();
            container.RegisterType<IFileRepository, FileRepository>();
            container.RegisterType<IWFFileStateRepository, WFFileStateRepository>();
            container.RegisterType<IWFFileStateNoteRepository, WFFileStateNoteRepository>();
            container.RegisterType<IProjectRepository, ProjectRepository>();
            container.RegisterType<IWorkflowRepository, WorkflowRepository>();
            container.RegisterType<IWFStateRepository, WFStateRepository>();
            container.RegisterType<IWFTransitionRepository, WFTransitionRepository>();
            container.RegisterType<IFileUploadRepository, FileUploadRepository>();
            container.RegisterType<IMiscRepository, MiscRepository>();

            //Services
            container.RegisterType<IEmailFactoryService, EmailFactoryService>();
            container.RegisterType<IEmailManagerService, EmailManagerService>();
            container.RegisterType<IEmailService, EmailService>();
            container.RegisterType<IFileService, FileService>();
            container.RegisterType<ISessionStorageService, SessionStorageService>();
            container.RegisterType<ISmsService, SmsService>();

            container.RegisterType<IWebApiContentService, WebApiContentService>();
            container.RegisterType<ILogService, LogService>();
            container.RegisterType<IAsyncService, AsyncService>();
            container.RegisterType<IUtilService, UtilService>();
            container.RegisterType<IHtmlToViewHelper, HtmlToViewHelper>();
            container.RegisterType<IEmailTemplateService, EmailTemplateService>();
            container.RegisterType<IProjectService, ProjectService>();
            container.RegisterType<IPanelService, PanelService>();
            container.RegisterType<ICustomerFileService, CustomerFileService>();
            container.RegisterType<ICustomerService, CustomerService>();
            container.RegisterType<IWorkflowService, WorkflowService>();
            container.RegisterType<IAppConfigurationService, AppConfigurationService>();
            container.RegisterType<IMailingService, MailingService>();
            container.RegisterType<IHistoryService, HistoryService>();
            container.RegisterType<IMiscService, MiscService>();
            
            //Generics
            container.RegisterType(typeof(IDictionary<,>), typeof(Dictionary<,>));
            container.RegisterType<RequestContext>(new InjectionFactory(i =>
            {
                return HttpContext.Current.Request.RequestContext;
            }));            

        }
    }
}
