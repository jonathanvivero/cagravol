using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SGE.Cagravol.Domain.Entities.Common;
using SGE.Cagravol.Domain.Entities.Identity;
using SGE.Cagravol.Application.ServiceModel;
using SGE.Cagravol.Infrastructure.Utils.Definitions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Threading.Tasks;
using SGE.Cagravol.Domain.Entities.Workflows;
using SGE.Cagravol.Domain.Entities.Customers;
using SGE.Cagravol.Domain.Entities.Files;
using SGE.Cagravol.Application.Core.Enums.Common;
using SGE.Cagravol.Application.Core.Enums.Common.Common;
using SGE.Cagravol.Application.Core.Enums.Grafidec;

namespace SGE.Cagravol.Infrastructure.Data.Migrations.Updates
{
    /// <summary>
    /// Deploy the very first data in the DB after first Migration.
    /// Since this is the inital migration, only creates the Application Configuration registry with the app version 
    /// First of all, check for if this key already exist.
    /// Both cases, it returns the AppConfiguration instance.
    /// </summary>
    public class PortalUpdate_001 : PortalUpdate, IPortalUpdate
    {

        private UserManager<User, string> manager = null;
        private RoleManager<Role, string> roleManager = null;

        public PortalUpdate_001(SGEContext _context, AppConfiguration _keyVersion)
            : base(_context, _keyVersion)
        { }

        public override AppConfiguration DoUpdate()
        {
            manager = new UserManager<User, string>(
                //new UserStore<User, Role, string, UserLogin, UserRole, UserClaim>(this.context)
                    new UserStore<User>(this.context)
                );

            roleManager = new RoleManager<Role, string>(
                //new RoleStore<Role, string, UserRole>(this.context)
                    new RoleStore<Role>(this.context)
                    );

            /* 
             * 1.- Create Roles
             * 2.- Create Users
             * 3.- Create AppConfig Entries
             * 4.- Create Default Workflow
             * 5.- Create Default States
             * 6.- Create Default Transitions
             * 7.- Create Sample Project/Event
             * 9.- Create Sample Expositors
             *
             **/

            IResultModel rm = this.CreateRolesAndUsers();

            if (rm.Success)
            {
                rm = this.CreateAppConfigEntries();
            }
            else
            {                
                Debug.WriteLine("PortalUpdate_001.CreateRolesAndUsers");
                Debug.WriteLine(rm.ErrorMessage);
                log("PortalUpdate_001.CreateRolesAndUsers", rm.ErrorMessage);
            }

            if (rm.Success)
            {
                rm = this.CreateWorkflow();
            }
            else
            {
                Debug.WriteLine("PortalUpdate_001.CreateAppConfigEntries");
                Debug.WriteLine(rm.ErrorMessage);
                log("PortalUpdate_001.CreateAppConfigEntries", rm.ErrorMessage);
            }

            if (rm.Success)
            {
                rm = this.CreateSampleProject();
            }
            else
            {
                Debug.WriteLine("PortalUpdate_001.CreateWorkflow");
                Debug.WriteLine(rm.ErrorMessage);
                log("PortalUpdate_001.CreateWorkflow", rm.ErrorMessage);

            }

            if (rm.Success)
            {
                rm = this.CreateBillDataTypes();
            }
            else
            {
                Debug.WriteLine("PortalUpdate_001.CreateSampleProject");
                Debug.WriteLine(rm.ErrorMessage);
                log("PortalUpdate_001.CreateSampleProject", rm.ErrorMessage);
            }

            if (rm.Success)
            {
                rm = this.CreateFileTypes();
            }
            else
            {
                Debug.WriteLine("PortalUpdate_001.CreateBillDataTypes");
                Debug.WriteLine(rm.ErrorMessage);
                log("PortalUpdate_001.CreateBillDataTypes", rm.ErrorMessage);

            }

            if (!rm.Success)
            {
                Debug.WriteLine("PortalUpdate_001.CreateFileTypes");
                Debug.WriteLine(rm.ErrorMessage);
                log("PortalUpdate_001.CreateFileTypes", rm.ErrorMessage);

            }

            this.context.SaveChanges();

            this.keyVersion.Value = "0.0.2";

            return this.keyVersion;
        }
        private IResultModel CreateRolesAndUsers()
        {
            IResultModel rm = new ResultModel();

            try
            {
                if (!roleManager.RoleExists(RoleDefinitions.Administrator))
                {
                    roleManager.Create(new Role() { Name = RoleDefinitions.Administrator });
                }

                //this.context.SaveChanges();

                if (!roleManager.RoleExists(RoleDefinitions.Customer))
                {
                    roleManager.Create(new Role() { Name = RoleDefinitions.Customer });
                }

                if (!roleManager.RoleExists(RoleDefinitions.Manager))
                {
                    roleManager.Create(new Role() { Name = RoleDefinitions.Manager });
                }

                if (!roleManager.RoleExists(RoleDefinitions.Organizer))
                {
                    roleManager.Create(new Role() { Name = RoleDefinitions.Organizer });
                }

                if (!roleManager.RoleExists(RoleDefinitions.Supervisor))
                {
                    roleManager.Create(new Role() { Name = RoleDefinitions.Supervisor });
                }


                // Create 4 test users:
                //Administrator
                if (manager.FindByName("jonathan@sgesoft.com") == null)
                {
                    var user = new User()
                    {
                        UserName = "jonathan@sgesoft.com",
                        Email = "jonathan@sgesoft.com",
                        IsActive = true,
                        CreatedOn = DateTime.UtcNow,
                        LastAccess = DateTime.UtcNow,
                        UpdatedOn = DateTime.UtcNow,
                        Name = "Administrator",
                        Surname = "Trator",
                        EmailConfirmed = true
                    };
                    manager.Create(user, "123456789");

                    manager.AddToRole(user.Id, RoleDefinitions.Administrator);
                    manager.AddToRole(user.Id, RoleDefinitions.Manager);
                    manager.AddToRole(user.Id, RoleDefinitions.Supervisor);
                    manager.AddToRole(user.Id, RoleDefinitions.Organizer);
                    manager.AddToRole(user.Id, RoleDefinitions.Customer);
                }

                this.context.SaveChanges();

                if (manager.FindByName("jms@grafidec.es") == null)
                {
                    var user = new User()
                    {
                        UserName = "jms@grafidec.es",
                        Email = "jms@grafidec.es",
                        IsActive = true,
                        CreatedOn = DateTime.UtcNow,
                        LastAccess = DateTime.UtcNow,
                        UpdatedOn = DateTime.UtcNow,
                        Name = "Jose",
                        Surname = "Grafidec Admin",
                        EmailConfirmed = true
                    };
                    manager.Create(user, "123456789");

                    manager.AddToRole(user.Id, RoleDefinitions.Administrator);
                    manager.AddToRole(user.Id, RoleDefinitions.Manager);
                    manager.AddToRole(user.Id, RoleDefinitions.Supervisor);
                    manager.AddToRole(user.Id, RoleDefinitions.Organizer);
                    manager.AddToRole(user.Id, RoleDefinitions.Customer);
                }

                this.context.SaveChanges();

                
                //Manager
                if (manager.FindByName("jonathan-manager@sgesoft.com") == null)
                {
                    var user = new User()
                    {
                        UserName = "jonathan_manager@sgesoft.com",
                        Email = "jonathan_manager@sgesoft.com",
                        IsActive = true,
                        CreatedOn = DateTime.UtcNow,
                        LastAccess = DateTime.UtcNow,
                        UpdatedOn = DateTime.UtcNow,
                        Name = "Manager",
                        Surname = "Nager",
                        EmailConfirmed = true
                    };
                    manager.Create(user, "123456789");
                    manager.AddToRole(user.Id, RoleDefinitions.Manager);
                    manager.AddToRole(user.Id, RoleDefinitions.Supervisor);
                    manager.AddToRole(user.Id, RoleDefinitions.Organizer);
                    manager.AddToRole(user.Id, RoleDefinitions.Customer);
                }

                this.context.SaveChanges();

                //Supervisor
                if (manager.FindByName("jonathan_supervisor@sgesoft.com") == null)
                {
                    var user = new User()
                    {
                        UserName = "jonathan_supervisor@sgesoft.com",
                        Email = "jonathan_supervisor@sgesoft.com",
                        IsActive = true,
                        CreatedOn = DateTime.UtcNow,
                        LastAccess = DateTime.UtcNow,
                        UpdatedOn = DateTime.UtcNow,
                        Name = "Supervisor",
                        Surname = "Visor",
                        EmailConfirmed = true
                    };
                    manager.Create(user, "123456789");                    
                    manager.AddToRole(user.Id, RoleDefinitions.Supervisor);
                    manager.AddToRole(user.Id, RoleDefinitions.Organizer);
                    manager.AddToRole(user.Id, RoleDefinitions.Customer);
                }

                this.context.SaveChanges();

                //Organizer
                if (manager.FindByName("jonathan_organizer@sgesoft.com") == null)
                {
                    var user = new User()
                    {
                        UserName = "jonathan_organizer@sgesoft.com",
                        Email = "jonathan_organizer@sgesoft.com",
                        IsActive = true,
                        CreatedOn = DateTime.UtcNow,
                        LastAccess = DateTime.UtcNow,
                        UpdatedOn = DateTime.UtcNow,
                        Name = "Organizer",
                        Surname = "Nizer",
                        EmailConfirmed = true
                    };
                    manager.Create(user, "123456789");                    
                    manager.AddToRole(user.Id, RoleDefinitions.Organizer);
                    manager.AddToRole(user.Id, RoleDefinitions.Customer);
                }

                this.context.SaveChanges();

                //Jose Manager
                if (manager.FindByName("jose.gomez@sgesoft.com") == null)
                {
                    var user = new User()
                    {
                        UserName = "jose.gomez@sgesoft.com",
                        Email = "jose.gomez@sgesoft.com",
                        IsActive = true,
                        CreatedOn = DateTime.UtcNow,
                        LastAccess = DateTime.UtcNow,
                        UpdatedOn = DateTime.UtcNow,
                        Name = "Jose",
                        Surname = "Gómez",
                        EmailConfirmed = true
                    };
                    manager.Create(user, "123456789");
                    manager.AddToRole(user.Id, RoleDefinitions.Manager);
                    manager.AddToRole(user.Id, RoleDefinitions.Supervisor);
                    manager.AddToRole(user.Id, RoleDefinitions.Organizer);
                    manager.AddToRole(user.Id, RoleDefinitions.Customer);
                }
                this.context.SaveChanges();
                
                //Carlos Manager
                if (manager.FindByName("operaciones@sgesoft.com") == null)
                {
                    var user = new User()
                    {
                        UserName = "operaciones@sgesoft.com",
                        Email = "operaciones@sgesoft.com",
                        IsActive = true,
                        CreatedOn = DateTime.UtcNow,
                        LastAccess = DateTime.UtcNow,
                        UpdatedOn = DateTime.UtcNow,
                        Name = "Carlos",
                        Surname = "Morales",
                        EmailConfirmed = true
                    };
                    manager.Create(user, "123456789");
                    manager.AddToRole(user.Id, RoleDefinitions.Manager);
                    manager.AddToRole(user.Id, RoleDefinitions.Supervisor);
                    manager.AddToRole(user.Id, RoleDefinitions.Organizer);
                    manager.AddToRole(user.Id, RoleDefinitions.Customer);
                }

                //this.context.SaveChanges();

                rm.OnSuccess("Ok!");
            }
            catch (Exception ex)
            {
                DumpExeption(ex);
                rm.OnException(ex);
            }

            return rm;
        }

        private IResultModel CreateAppConfigEntries()
        {
            IResultModel rm = new ResultModel();
            IList<AppConfiguration> addList = new List<AppConfiguration>();

            try
            {
                addList.Add(new AppConfiguration()
                {
                    IsPublicEnable = false,
                    IsPublicViewable = false,
                    Key = EnumAppConfigurationKeyDefinition.DefaultFileWorkflowCode,
                    FieldType = EnumAppConfigurationFieldType.Text,
                    Value = "WORKFLOW_STANDS"
                });

                addList.Add(new AppConfiguration()
                {
                    IsPublicEnable = false,
                    IsPublicViewable = false,
                    Key = EnumAppConfigurationKeyDefinition.DefaultGeneralSpaceFileWorkflowCode,
                    FieldType = EnumAppConfigurationFieldType.Text,
                    Value = "WORKFLOW_GENERAL"
                });

                addList.Add(new AppConfiguration()
                {
                    IsPublicEnable = false,
                    IsPublicViewable = false,
                    Key = EnumAppConfigurationKeyDefinition.DefaultTotalStandsPerEvent,
                    FieldType = EnumAppConfigurationFieldType.Numeric,
                    Value = "50"
                });

                addList.Add(new AppConfiguration()
                {
                    IsPublicEnable = false,
                    IsPublicViewable = false,
                    Key = EnumAppConfigurationKeyDefinition.PublicKey,
                    FieldType = EnumAppConfigurationFieldType.Text,
                    Value = "4!-df%_$gtsfd3t$_Sr5SADsa"
                });

                addList.Add(new AppConfiguration()
                {
                    IsPublicEnable = false,
                    IsPublicViewable = false,
                    Key = EnumAppConfigurationKeyDefinition.SecretKey,
                    FieldType = EnumAppConfigurationFieldType.Text,
                    Value = "asDAS5rS_$t3dfstg$_%fd-!4"
                });

                this.context.AppConfigurations.AddOrUpdate(addList.ToArray());

                this.context.SaveChanges();
                
                rm.OnSuccess();
            }
            catch (Exception ex)
            {
                DumpExeption(ex);
                rm.OnException(ex);
            }

            return rm;
        }

        private IResultModel CreateWorkflow()
        {
            IResultModel rm = new ResultModel();
            IList<WFState> addStates = new List<WFState>();
            var listWFTransitions = new List<WFWorkflowTransition>();
            WFTransition tr;
                
            try
            {
                WFWorkflow wfStands = this.context.WFWorkflows.Create();
                WFWorkflow wfGeneral = this.context.WFWorkflows.Create();

                string key = string.Empty;

                #region add workflows

                wfStands.Name = "Workflow Expositores";
                wfStands.Code = GrafidecWorkflowCodeEnum.WORKFLOW_STANDS.ToString();
                wfStands.Notes = "Flujo de Trabajo por Defecto para gestión de Artes Finales de Expositores";
                wfStands.WFWorkflowVersion = 1;
                wfStands.WFWorkflowRelatedEntities.Add(
                    new WFWorkflowRelatedEntity()
                    {
                        EntityType = Application.Core.Enums.Workflows.WFEntityTypeEnum.File,
                        Name = "Workflow de Artes Finales de Expositores"
                    });

                wfGeneral.Name = "Workflow Espacio General";
                wfGeneral.Code = GrafidecWorkflowCodeEnum.WORKFLOW_GENERAL.ToString();
                wfGeneral.Notes = "Flujo de Trabajo por Defecto para gestión del espacio general";
                wfStands.WFWorkflowVersion = 1;
                wfGeneral.WFWorkflowRelatedEntities.Add(
                    new WFWorkflowRelatedEntity()
                    {
                        EntityType = Application.Core.Enums.Workflows.WFEntityTypeEnum.File,
                        Name = "Workflow de Artes Finales Espacio General"
                    });

                this.context.WFWorkflows.Add(wfStands);
                this.context.WFWorkflows.Add(wfGeneral);
                this.context.SaveChanges();

                #endregion
                
                #region Create Group Users
                var roleAdmin = roleManager.FindByName(RoleDefinitions.Administrator);
                var roleMan = roleManager.FindByName(RoleDefinitions.Manager);
                var roleSuper = roleManager.FindByName(RoleDefinitions.Supervisor);
                var roleOrg = roleManager.FindByName(RoleDefinitions.Organizer);
                var roleCuz = roleManager.FindByName(RoleDefinitions.Customer);

                var groupAdmin = new WFGroup() { Name = "Adminitradores", IsPreset = true };
                groupAdmin.Roles.Add(new WFRole() { RoleId = roleAdmin.Id });

                var groupMan = new WFGroup() { Name = "Managers", IsPreset = true };
                groupMan.Roles.Add(new WFRole() { RoleId = roleMan.Id });

                var groupSup = new WFGroup() { Name = "Supervisores", IsPreset = true };
                groupSup.Roles.Add(new WFRole() { RoleId = roleSuper.Id });

                var groupOrg = new WFGroup() { Name = "Organizadores", IsPreset = true };
                groupOrg.Roles.Add(new WFRole() { RoleId = roleOrg.Id });

                var groupCuz = new WFGroup() { Name = "Expositores", IsPreset = true };
                groupCuz.Roles.Add(new WFRole() { RoleId = roleCuz.Id });

                var groupFunctional = new WFGroup() { Name = "Supervisores y Expositores", IsPreset = true };
                groupFunctional.Roles.Add(new WFRole() { RoleId = roleSuper.Id });
                groupFunctional.Roles.Add(new WFRole() { RoleId = roleCuz.Id });


                var groupAll = new WFGroup() { Name = "Todos", IsPreset = true };
                groupAll.Roles.Add(new WFRole() { RoleId = roleAdmin.Id });
                groupAll.Roles.Add(new WFRole() { RoleId = roleMan.Id });
                groupAll.Roles.Add(new WFRole() { RoleId = roleOrg.Id });
                groupAll.Roles.Add(new WFRole() { RoleId = roleSuper.Id });
                groupAll.Roles.Add(new WFRole() { RoleId = roleCuz.Id });

                this.context.WFGroups.Add(groupAdmin);
                this.context.WFGroups.Add(groupAll);
                this.context.WFGroups.Add(groupMan);
                this.context.WFGroups.Add(groupSup);
                this.context.WFGroups.Add(groupOrg);
                this.context.WFGroups.Add(groupCuz);
                this.context.WFGroups.Add(groupFunctional);
                this.context.SaveChanges();

                #endregion

                #region Add States



                key = GrafidecStateEnum.FILE_IN_UPLOAD.ToString(); // "FILE_IN_UPLOAD";
                WFState WFState_FILE_IN_UPLOAD = new WFState() { Name = "Archivo En Envío/Subida", Description = string.Empty, Code = key, WFGrantedGroupId = groupFunctional.Id };

                key = GrafidecStateEnum.FILE_UPLOAD_FAILED.ToString();
                WFState WFState_FILE_UPLOAD_FAILED = new WFState() { Name = "Error en Envío de Archivo", Description = string.Empty, Code = key, WFGrantedGroupId = groupFunctional.Id };

                key = GrafidecStateEnum.FILE_LOADED.ToString();
                WFState WFState_FILE_LOADED = new WFState() { Name = "Archivo Recibido", Description = string.Empty, Code = key, WFGrantedGroupId = groupFunctional.Id };
                
                key = GrafidecStateEnum.FILE_RE_UPLOADED.ToString();
                WFState WFState_FILE_RE_UPLOADED = new WFState() { Name = "Archivo Re-enviado", Description = string.Empty, Code = key, WFGrantedGroupId = groupFunctional.Id };

                key = GrafidecStateEnum.FILE_REJECTED.ToString();
                WFState WFState_FILE_REJECTED = new WFState() { Name = "Archivo Rechazado", Description = string.Empty, Code = key, WFGrantedGroupId = groupFunctional.Id };

                key = GrafidecStateEnum.FILE_IN_REVISION.ToString();
                WFState WFState_FILE_IN_REVISION = new WFState() { Name = "Archivo En Revisión", Description = string.Empty, Code = key, WFGrantedGroupId = groupFunctional.Id };

                key = GrafidecStateEnum.FILE_READY_FOR_PRODUCTION.ToString();
                WFState WFState_FILE_READY_FOR_PRODUCTION = new WFState() { Name = "Listo para Producción", Description = string.Empty, Code = key, WFGrantedGroupId = groupFunctional.Id };

                key = GrafidecStateEnum.FILE_IN_PRODUCTION.ToString();
                WFState WFState_FILE_IN_PRODUCTION = new WFState() { Name = "En Producción", Description = string.Empty, Code = key, WFGrantedGroupId = groupFunctional.Id };

                key = GrafidecStateEnum.FILE_FILED.ToString();
                WFState WFState_FILE_FILED = new WFState() { Name = "Archivado/Eliminado", Description = string.Empty, Code = key, WFGrantedGroupId = groupFunctional.Id };

                this.context.WFStates.Add(WFState_FILE_FILED);
                this.context.WFStates.Add(WFState_FILE_IN_PRODUCTION);
                this.context.WFStates.Add(WFState_FILE_IN_REVISION);
                this.context.WFStates.Add(WFState_FILE_IN_UPLOAD);
                this.context.WFStates.Add(WFState_FILE_RE_UPLOADED);
                this.context.WFStates.Add(WFState_FILE_LOADED);
                this.context.WFStates.Add(WFState_FILE_READY_FOR_PRODUCTION);
                this.context.WFStates.Add(WFState_FILE_REJECTED);
                this.context.WFStates.Add(WFState_FILE_UPLOAD_FAILED);                

                this.context.SaveChanges();

                #endregion

                #region Add Transitions


//FILE_IN_UPLOAD_TO_FILE_UPLOAD_FAILED,
                key = GrafidecTransitionEnum.FILE_IN_UPLOAD_TO_FILE_UPLOAD_FAILED.ToString();
                WFTransition WFTran_FILE_IN_UPLOAD_TO_FILE_UPLOAD_FAILED = new WFTransition() { CouldComment = true, MustComment = false, Code = key, WFNotificationGroupId = groupMan.Id , WFDefaultStateOriginId = WFState_FILE_IN_UPLOAD.Id, WFDefaultStateDestinationId = WFState_FILE_UPLOAD_FAILED.Id };
                
//FILE_IN_UPLOAD_TO_FILE_LOADED,        
                key = GrafidecTransitionEnum.FILE_IN_UPLOAD_TO_FILE_LOADED.ToString();
                WFTransition WFTran_FILE_IN_UPLOAD_TO_FILE_LOADED = new WFTransition() { CouldComment = true, MustComment = false, Code = key, WFNotificationGroupId = groupMan.Id , WFDefaultStateOriginId = WFState_FILE_IN_UPLOAD.Id, WFDefaultStateDestinationId = WFState_FILE_LOADED.Id };
                
//FILE_LOADED_TO_FILE_IN_REVISION,
                key = GrafidecTransitionEnum.FILE_LOADED_TO_FILE_IN_REVISION.ToString();
                WFTransition WFTran_FILE_LOADED_TO_FILE_IN_REVISION = new WFTransition() { CouldComment = true, MustComment = false, Code = key, WFNotificationGroupId = groupMan.Id , WFDefaultStateOriginId = WFState_FILE_LOADED.Id, WFDefaultStateDestinationId = WFState_FILE_IN_REVISION.Id };
                
//FILE_RE_UPLOADED_TO_FILE_IN_REVISION,
                key = GrafidecTransitionEnum.FILE_RE_UPLOADED_TO_FILE_IN_REVISION.ToString();
                WFTransition WFTran_FILE_RE_UPLOADED_TO_FILE_IN_REVISION = new WFTransition() { CouldComment = true, MustComment = false, Code = key, WFNotificationGroupId = groupMan.Id , WFDefaultStateOriginId = WFState_FILE_RE_UPLOADED.Id, WFDefaultStateDestinationId = WFState_FILE_IN_REVISION.Id };
                
//FILE_IN_REVISION_TO_FILE_REJECTED,
                key = GrafidecTransitionEnum.FILE_IN_REVISION_TO_FILE_REJECTED.ToString();
                WFTransition WFTran_FILE_IN_REVISION_TO_FILE_REJECTED = new WFTransition() { CouldComment = true, MustComment = false, Code = key, WFNotificationGroupId = groupMan.Id , WFDefaultStateOriginId = WFState_FILE_IN_REVISION.Id, WFDefaultStateDestinationId = WFState_FILE_REJECTED.Id };
                
//FILE_IN_REVISION_TO_FILE_READY_FOR_PRODUCTION,        
                key = GrafidecTransitionEnum.FILE_IN_REVISION_TO_FILE_READY_FOR_PRODUCTION.ToString();
                WFTransition WFTran_FILE_IN_REVISION_TO_FILE_READY_FOR_PRODUCTION = new WFTransition() { CouldComment = true, MustComment = false, Code = key, WFNotificationGroupId = groupMan.Id , WFDefaultStateOriginId = WFState_FILE_IN_REVISION.Id, WFDefaultStateDestinationId = WFState_FILE_READY_FOR_PRODUCTION.Id };
                
//FILE_READY_FOR_PRODUCTION_TO_FILE_REJECTED,
                key = GrafidecTransitionEnum.FILE_READY_FOR_PRODUCTION_TO_FILE_REJECTED.ToString();
                WFTransition WFTran_FILE_READY_FOR_PRODUCTION_TO_FILE_REJECTED = new WFTransition() { CouldComment = true, MustComment = false, Code = key, WFNotificationGroupId = groupMan.Id , WFDefaultStateOriginId = WFState_FILE_READY_FOR_PRODUCTION.Id, WFDefaultStateDestinationId = WFState_FILE_REJECTED.Id };

//FILE_READY_FOR_PRODUCTION_TO_FILE_IN_PRODUCTION,        
                key = GrafidecTransitionEnum.FILE_READY_FOR_PRODUCTION_TO_FILE_IN_PRODUCTION.ToString();
                WFTransition WFTran_FILE_READY_FOR_PRODUCTION_TO_FILE_IN_PRODUCTION = new WFTransition() { CouldComment = true, MustComment = false, Code = key, WFNotificationGroupId = groupMan.Id , WFDefaultStateOriginId = WFState_FILE_READY_FOR_PRODUCTION.Id, WFDefaultStateDestinationId = WFState_FILE_IN_PRODUCTION.Id };
                
//FILE_IN_PRODUCTION_TO_FILE_FILED,
                key = GrafidecTransitionEnum.FILE_IN_PRODUCTION_TO_FILE_FILED.ToString();
                WFTransition WFTran_FILE_IN_PRODUCTION_TO_FILE_FILED = new WFTransition() { CouldComment = true, MustComment = false, Code = key, WFNotificationGroupId = groupMan.Id , WFDefaultStateOriginId = WFState_FILE_IN_PRODUCTION.Id, WFDefaultStateDestinationId = WFState_FILE_FILED.Id };
                
//FILE_IN_PRODUCTION_TO_FILE_REJECTED,
                key = GrafidecTransitionEnum.FILE_IN_PRODUCTION_TO_FILE_REJECTED.ToString();
                WFTransition WFTran_FILE_IN_PRODUCTION_TO_FILE_REJECTED = new WFTransition() { CouldComment = true, MustComment = false, Code = key, WFNotificationGroupId = groupMan.Id , WFDefaultStateOriginId = WFState_FILE_IN_PRODUCTION.Id, WFDefaultStateDestinationId = WFState_FILE_REJECTED.Id };

//FILE_REJECTED_TO_FILE_RE_UPLOADED
                key = GrafidecTransitionEnum.FILE_REJECTED_TO_FILE_RE_UPLOADED.ToString(); 
                WFTransition WFTran_FILE_REJECTED_TO_FILE_RE_UPLOADED = new WFTransition() { CouldComment = true, MustComment = false, Code = key, WFNotificationGroupId = groupMan.Id, WFDefaultStateOriginId = WFState_FILE_REJECTED.Id, WFDefaultStateDestinationId = WFState_FILE_RE_UPLOADED.Id };

//FILE_LOADED_TO_FILE_IN_PRODUCTION, => Only for General Workflow
                key = GrafidecTransitionEnum.FILE_LOADED_TO_FILE_IN_PRODUCTION.ToString();
                WFTransition WFTran_FILE_LOADED_TO_FILE_IN_PRODUCTION = new WFTransition() { CouldComment = true, MustComment = false, Code = key, WFNotificationGroupId = groupMan.Id, WFDefaultStateOriginId = WFState_FILE_LOADED.Id, WFDefaultStateDestinationId = WFState_FILE_IN_PRODUCTION.Id };


                this.context.WFTransitions.Add(WFTran_FILE_IN_UPLOAD_TO_FILE_UPLOAD_FAILED);
                this.context.WFTransitions.Add(WFTran_FILE_IN_UPLOAD_TO_FILE_LOADED);                
                this.context.WFTransitions.Add(WFTran_FILE_LOADED_TO_FILE_IN_REVISION);
                this.context.WFTransitions.Add(WFTran_FILE_IN_REVISION_TO_FILE_REJECTED);                
                this.context.WFTransitions.Add(WFTran_FILE_IN_REVISION_TO_FILE_READY_FOR_PRODUCTION);
                this.context.WFTransitions.Add(WFTran_FILE_REJECTED_TO_FILE_RE_UPLOADED);
                this.context.WFTransitions.Add(WFTran_FILE_RE_UPLOADED_TO_FILE_IN_REVISION);
                this.context.WFTransitions.Add(WFTran_FILE_READY_FOR_PRODUCTION_TO_FILE_IN_PRODUCTION);
                this.context.WFTransitions.Add(WFTran_FILE_READY_FOR_PRODUCTION_TO_FILE_REJECTED);
                this.context.WFTransitions.Add(WFTran_FILE_IN_PRODUCTION_TO_FILE_FILED);
                this.context.WFTransitions.Add(WFTran_FILE_IN_PRODUCTION_TO_FILE_REJECTED);
                //=> Only for General Workflow
                this.context.WFTransitions.Add(WFTran_FILE_LOADED_TO_FILE_IN_PRODUCTION);                

                this.context.SaveChanges();

                #endregion

                #region STANDS WORKFLOW => Creates the workflow-states and the workflow-transitions

                key = GrafidecStateEnum.FILE_IN_UPLOAD.ToString();
                WFWorkflowState wfws_FILE_IN_UPLOAD = new WFWorkflowState()
                {
                    IsInitial = true,
                    WFStateId = WFState_FILE_IN_UPLOAD.Id,
                    WFWorkflowId = wfStands.Id,
                    WFWorkflowVersion = wfStands.WFWorkflowVersion
                };
                
                key = GrafidecStateEnum.FILE_UPLOAD_FAILED.ToString();
                WFWorkflowState wfws_FILE_UPLOAD_FAILED = new WFWorkflowState()
                {
                    IsInitial = false,
                    WFStateId = WFState_FILE_UPLOAD_FAILED.Id,
                    WFWorkflowId = wfStands.Id,
                    WFWorkflowVersion = wfStands.WFWorkflowVersion
                };
                
                key = GrafidecStateEnum.FILE_LOADED.ToString();
                WFWorkflowState wfws_FILE_LOADED = new WFWorkflowState()
                {
                    IsInitial = false,
                    WFStateId = WFState_FILE_LOADED.Id,
                    WFWorkflowId = wfStands.Id,
                    WFWorkflowVersion = wfStands.WFWorkflowVersion
                };
                
                key = GrafidecStateEnum.FILE_RE_UPLOADED.ToString();
                WFWorkflowState wfws_FILE_RE_UPLOADED = new WFWorkflowState()
                {
                    IsInitial = false,
                    WFStateId = WFState_FILE_RE_UPLOADED.Id,
                    WFWorkflowId = wfStands.Id,
                    WFWorkflowVersion = wfStands.WFWorkflowVersion
                };
                
                key = GrafidecStateEnum.FILE_REJECTED.ToString();
                WFWorkflowState wfws_FILE_REJECTED = new WFWorkflowState()
                {
                    IsInitial = false,
                    WFStateId = WFState_FILE_REJECTED.Id,
                    WFWorkflowId = wfStands.Id,
                    WFWorkflowVersion = wfStands.WFWorkflowVersion
                };
                key = GrafidecStateEnum.FILE_IN_REVISION.ToString();
                WFWorkflowState wfws_FILE_IN_REVISION = new WFWorkflowState()
                {
                    IsInitial = false,
                    WFStateId = WFState_FILE_IN_REVISION.Id,
                    WFWorkflowId = wfStands.Id,
                    WFWorkflowVersion = wfStands.WFWorkflowVersion
                };
                key = GrafidecStateEnum.FILE_READY_FOR_PRODUCTION.ToString();
                WFWorkflowState wfws_FILE_READY_FOR_PRODUCTION = new WFWorkflowState()
                {
                    IsInitial = false,
                    WFStateId = WFState_FILE_READY_FOR_PRODUCTION.Id,
                    WFWorkflowId = wfStands.Id,
                    WFWorkflowVersion = wfStands.WFWorkflowVersion
                };
                key = GrafidecStateEnum.FILE_IN_PRODUCTION.ToString();
                WFWorkflowState wfws_FILE_IN_PRODUCTION = new WFWorkflowState()
                {
                    IsInitial = false,
                    WFStateId = WFState_FILE_IN_PRODUCTION.Id,
                    WFWorkflowId = wfStands.Id,
                    WFWorkflowVersion = wfStands.WFWorkflowVersion
                };
                key = GrafidecStateEnum.FILE_FILED.ToString();
                WFWorkflowState wfws_FILE_FILED = new WFWorkflowState()
                {
                    IsInitial = false,
                    WFStateId = WFState_FILE_FILED.Id,
                    WFWorkflowId = wfStands.Id,
                    WFWorkflowVersion = wfStands.WFWorkflowVersion
                };
                
                this.context.WFWorkflowStates.Add(wfws_FILE_IN_UPLOAD);
                this.context.WFWorkflowStates.Add(wfws_FILE_UPLOAD_FAILED);
                this.context.WFWorkflowStates.Add(wfws_FILE_LOADED);
                this.context.WFWorkflowStates.Add(wfws_FILE_RE_UPLOADED);
                this.context.WFWorkflowStates.Add(wfws_FILE_REJECTED);
                this.context.WFWorkflowStates.Add(wfws_FILE_IN_REVISION);
                this.context.WFWorkflowStates.Add(wfws_FILE_READY_FOR_PRODUCTION);
                this.context.WFWorkflowStates.Add(wfws_FILE_IN_PRODUCTION);
                this.context.WFWorkflowStates.Add(wfws_FILE_FILED);
                this.context.SaveChanges();

                //Link Workflows with States and transitions
                
                tr = WFTran_FILE_IN_UPLOAD_TO_FILE_LOADED;
                listWFTransitions.Add(new WFWorkflowTransition()
                {
                    WFTransitionId = tr.Id,
                    WFWorkflowStateOriginId = wfws_FILE_IN_UPLOAD.Id,
                    WFWorkflowStateDestinationId = wfws_FILE_LOADED.Id, 
                    WFWorkflowId = wfStands.Id, 
                    WFWorkflowVersion = wfStands.WFWorkflowVersion               
                });

                tr = WFTran_FILE_IN_UPLOAD_TO_FILE_UPLOAD_FAILED;
                listWFTransitions.Add(new WFWorkflowTransition()
                {
                    WFTransitionId = tr.Id,
                    WFWorkflowStateOriginId = wfws_FILE_IN_UPLOAD.Id,
                    WFWorkflowStateDestinationId = wfws_FILE_UPLOAD_FAILED.Id,
                    WFWorkflowId = wfStands.Id,
                    WFWorkflowVersion = wfStands.WFWorkflowVersion
                });

                tr = WFTran_FILE_LOADED_TO_FILE_IN_REVISION;
                listWFTransitions.Add(new WFWorkflowTransition()
                {
                    WFTransitionId = tr.Id,
                    WFWorkflowStateOriginId = wfws_FILE_LOADED.Id,
                    WFWorkflowStateDestinationId = wfws_FILE_IN_REVISION.Id,
                    WFWorkflowId = wfStands.Id,
                    WFWorkflowVersion = wfStands.WFWorkflowVersion
                });

                tr = WFTran_FILE_IN_REVISION_TO_FILE_READY_FOR_PRODUCTION;
                listWFTransitions.Add(new WFWorkflowTransition()
                {
                    WFTransitionId = tr.Id,
                    WFWorkflowStateOriginId = wfws_FILE_IN_REVISION.Id,
                    WFWorkflowStateDestinationId = wfws_FILE_READY_FOR_PRODUCTION.Id,
                    WFWorkflowId = wfStands.Id,
                    WFWorkflowVersion = wfStands.WFWorkflowVersion
                });

                tr = WFTran_FILE_IN_REVISION_TO_FILE_REJECTED;
                listWFTransitions.Add(new WFWorkflowTransition()
                {
                    WFTransitionId = tr.Id,
                    WFWorkflowStateOriginId = wfws_FILE_IN_REVISION.Id,
                    WFWorkflowStateDestinationId = wfws_FILE_REJECTED.Id,
                    WFWorkflowId = wfStands.Id,
                    WFWorkflowVersion = wfStands.WFWorkflowVersion
                });

                tr = WFTran_FILE_REJECTED_TO_FILE_RE_UPLOADED;
                listWFTransitions.Add(new WFWorkflowTransition()
                {
                    WFTransitionId = tr.Id,
                    WFWorkflowStateOriginId = wfws_FILE_REJECTED.Id,
                    WFWorkflowStateDestinationId = wfws_FILE_RE_UPLOADED.Id,
                    WFWorkflowId = wfStands.Id,
                    WFWorkflowVersion = wfStands.WFWorkflowVersion
                });

                tr = WFTran_FILE_RE_UPLOADED_TO_FILE_IN_REVISION;
                listWFTransitions.Add(new WFWorkflowTransition()
                {
                    WFTransitionId = tr.Id,
                    WFWorkflowStateOriginId = wfws_FILE_RE_UPLOADED.Id,
                    WFWorkflowStateDestinationId = wfws_FILE_IN_REVISION.Id,
                    WFWorkflowId = wfStands.Id,
                    WFWorkflowVersion = wfStands.WFWorkflowVersion
                });

                tr = WFTran_FILE_READY_FOR_PRODUCTION_TO_FILE_IN_PRODUCTION;
                listWFTransitions.Add(new WFWorkflowTransition()
                {
                    WFTransitionId = tr.Id,
                    WFWorkflowStateOriginId = wfws_FILE_READY_FOR_PRODUCTION.Id,
                    WFWorkflowStateDestinationId = wfws_FILE_IN_PRODUCTION.Id,
                    WFWorkflowId = wfStands.Id,
                    WFWorkflowVersion = wfStands.WFWorkflowVersion
                });

                tr = WFTran_FILE_READY_FOR_PRODUCTION_TO_FILE_REJECTED;
                listWFTransitions.Add(new WFWorkflowTransition()
                {
                    WFTransitionId = tr.Id,
                    WFWorkflowStateOriginId = wfws_FILE_READY_FOR_PRODUCTION.Id,
                    WFWorkflowStateDestinationId = wfws_FILE_IN_PRODUCTION.Id,
                    WFWorkflowId = wfStands.Id,
                    WFWorkflowVersion = wfStands.WFWorkflowVersion
                });

                tr = WFTran_FILE_IN_PRODUCTION_TO_FILE_REJECTED;
                listWFTransitions.Add(new WFWorkflowTransition()
                {
                    WFTransitionId = tr.Id,
                    WFWorkflowStateOriginId = wfws_FILE_IN_PRODUCTION.Id,
                    WFWorkflowStateDestinationId = wfws_FILE_REJECTED.Id,
                    WFWorkflowId = wfStands.Id,
                    WFWorkflowVersion = wfStands.WFWorkflowVersion
                });

                tr = WFTran_FILE_IN_PRODUCTION_TO_FILE_FILED;
                listWFTransitions.Add(new WFWorkflowTransition()
                {
                    WFTransitionId = tr.Id,
                    WFWorkflowStateOriginId = wfws_FILE_IN_PRODUCTION.Id,
                    WFWorkflowStateDestinationId = wfws_FILE_FILED.Id,
                    WFWorkflowId = wfStands.Id,
                    WFWorkflowVersion = wfStands.WFWorkflowVersion
                });

                this.context.WFWorkflowTransitions.AddOrUpdate(listWFTransitions.ToArray());
                this.context.SaveChanges();

                #endregion                

                #region GENERAL WORKFLOW => Creates the workflow-states and the workflow-transitions

                // links workflow and states

                key = GrafidecStateEnum.FILE_LOADED.ToString();
                wfws_FILE_LOADED = new WFWorkflowState()
                {
                    IsInitial = true,
                    WFStateId = WFState_FILE_LOADED.Id,
                    WFWorkflowId = wfGeneral.Id,
                    WFWorkflowVersion = wfGeneral.WFWorkflowVersion
                };

                key = GrafidecStateEnum.FILE_IN_PRODUCTION.ToString();
                wfws_FILE_IN_PRODUCTION = new WFWorkflowState()
                {
                    IsInitial = false,
                    WFStateId = WFState_FILE_IN_PRODUCTION.Id,
                    WFWorkflowId = wfGeneral.Id,
                    WFWorkflowVersion = wfGeneral.WFWorkflowVersion
                };

                key = GrafidecStateEnum.FILE_FILED.ToString();
                wfws_FILE_FILED = new WFWorkflowState()
                {
                    IsInitial = false,
                    WFStateId = WFState_FILE_FILED.Id,
                    WFWorkflowId = wfGeneral.Id,
                    WFWorkflowVersion = wfGeneral.WFWorkflowVersion
                };

                this.context.WFWorkflowStates.Add(wfws_FILE_LOADED);
                this.context.WFWorkflowStates.Add(wfws_FILE_IN_PRODUCTION);
                this.context.WFWorkflowStates.Add(wfws_FILE_FILED);
                this.context.SaveChanges();
                                
                //Link Workflows and transitions

                listWFTransitions = new List<WFWorkflowTransition>();

                tr = WFTran_FILE_LOADED_TO_FILE_IN_PRODUCTION;
                listWFTransitions.Add(new WFWorkflowTransition()
                {
                    WFTransitionId = tr.Id,
                    WFWorkflowStateOriginId = wfws_FILE_LOADED.Id,
                    WFWorkflowStateDestinationId = wfws_FILE_IN_PRODUCTION.Id,
                    WFWorkflowId = wfGeneral.Id,
                    WFWorkflowVersion = wfGeneral.WFWorkflowVersion
                });

                tr = WFTran_FILE_IN_PRODUCTION_TO_FILE_FILED;
                listWFTransitions.Add(new WFWorkflowTransition()
                {
                    WFTransitionId = tr.Id,
                    WFWorkflowStateOriginId = wfws_FILE_IN_PRODUCTION.Id,
                    WFWorkflowStateDestinationId = wfws_FILE_FILED.Id,
                    WFWorkflowId = wfGeneral.Id,
                    WFWorkflowVersion = wfGeneral.WFWorkflowVersion
                });

                this.context.WFWorkflowTransitions.AddOrUpdate(listWFTransitions.ToArray());
                this.context.SaveChanges();

                #endregion

                rm.OnSuccess();
            }
            catch (Exception ex)
            {
                DumpExeption(ex);
                rm.OnException(ex);                
            }

            return rm;
        }

        private IResultModel CreateSampleProject()
        {
            IResultModel rm = new ResultModel();

            try
            {
                //as soon as we have the DI set up...

                rm.OnSuccess();
            }
            catch (Exception ex)
            {
                DumpExeption(ex);
                rm.OnException(ex);
            }


            return rm;
        }

        private IResultModel CreateBillDataTypes()
        {
            IResultModel rm = new ResultModel();
            IList<BillDataType> addList = new List<BillDataType>();

            try
            {
                addList.Add(new BillDataType()
                {
                    I18NCode = "BILLDATATYPE_NONE",
                    Name = "None",
                    Notes = "No Data"
                });

                this.context.BillDataTypes.AddOrUpdate(addList.ToArray());

                this.context.SaveChanges();

                rm.OnSuccess();
            }
            catch (Exception ex)
            {
                DumpExeption(ex);
                rm.OnException(ex);
            }

            return rm;
        }

        private IResultModel CreateFileTypes()
        {
            IResultModel rm = new ResultModel();
            IList<FileType> addList = new List<FileType>();

            try
            {
                addList.Add(new FileType()
                {
                    Name = "JPEG",
                    Notes = "",
                    FileExtension = "jpg|jpeg|jpe|jfif"
                });

                addList.Add(new FileType()
                {
                    Name = "PNG",
                    Notes = "",
                    FileExtension = "png"
                });

                addList.Add(new FileType()
                {
                    Name = "Adobe Photoshop",
                    Notes = "",
                    FileExtension = "psd"
                });

                addList.Add(new FileType()
                {
                    Name = "Adobe Illustrator",
                    Notes = "",
                    FileExtension = "ai"
                });

                addList.Add(new FileType()
                {
                    Name = "SVG",
                    Notes = "",
                    FileExtension = "svg|svgz"
                });
                addList.Add(new FileType()
                {
                    Name = "PDF",
                    Notes = "",
                    FileExtension = "pdf"
                });

                addList.Add(new FileType()
                {
                    Name = "BMP",
                    Notes = "",
                    FileExtension = "bmp|dib"
                });

                addList.Add(new FileType()
                {
                    Name = "WMF",
                    Notes = "Windows Meta File",
                    FileExtension = "wmf"
                });

                addList.Add(new FileType()
                {
                    Name = "GIF",
                    Notes = "",
                    FileExtension = "gif|giff"
                });

                addList.Add(new FileType()
                {
                    Name = "TIFF",
                    Notes = "",
                    FileExtension = "tif|tiff|tff"
                });

                addList.Add(new FileType()
                {
                    Name = "TGA",
                    Notes = "",
                    FileExtension = "tga"
                });

                addList.Add(new FileType()
                {
                    Name = "Corel Draw",
                    Notes = "",
                    FileExtension = "cdr"
                });

                addList.Add(new FileType()
                {
                    Name = "Corel Photo Paint",
                    Notes = "",
                    FileExtension = "ctp"
                });

                addList.Add(new FileType()
                {
                    Name = "EPS",
                    Notes = "",
                    FileExtension = "eps"
                });

                addList.Add(new FileType()
                {
                    Name = "ZIP",
                    Notes = "",
                    FileExtension = "zip"
                });

                addList.Add(new FileType()
                {
                    Name = "RAR",
                    Notes = "",
                    FileExtension = "rar|r00"
                });

                addList.Add(new FileType()
                {
                    Name = "Otros",
                    Notes = "Tipo de Archivo No gráfico o no contemplado",
                    FileExtension = "*|*.*"
                });

                this.context.FileTypes.AddOrUpdate(addList.ToArray());

                this.context.SaveChanges();

                rm.OnSuccess();
            }
            catch (Exception ex)
            {
                DumpExeption(ex);
                rm.OnException(ex);
            }

            return rm;
        }
    }
}
