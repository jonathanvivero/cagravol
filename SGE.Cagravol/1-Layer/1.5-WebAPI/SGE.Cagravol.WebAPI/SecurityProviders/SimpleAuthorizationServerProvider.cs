using Microsoft.Owin.Security.OAuth;
using SGE.Cagravol.Application.Services.Identity;
using SGE.Cagravol.WebAPI.App_Start;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using SGE.Cagravol.Domain.Entities.Identity;
using SGE.Cagravol.Domain.POCO.ServiceModels.Identity;
using System.Security.Claims;
using System.Collections.Generic;
using System.Linq;
using SGE.Cagravol.Infrastructure.Utils.Definitions;
using Microsoft.Owin.Security;
using SGE.Cagravol.Presentation.Resources.Common;
using SGE.Cagravol.Application.Services.WebApi;
//using Microsoft.Practices.Unity.Configuration;


namespace SGE.Cagravol.WebAPI.SecurityProviders
{
    public class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {


        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {

            await Task.FromResult(context.Validated());
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {

            //context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

            var unityContainer = UnityConfig.GetConfiguredContainer();

            IAccountService accountService = unityContainer.Resolve<IAccountService>();


            UserLoginResultServiceModel loginResult = await accountService.Login(context.UserName, context.Password, false);

            if (loginResult == null)
            {
                context.SetError("invalid_grant", ErrorResources.UserOrEmailNorValid);
                return;
            }
            else
            {
                if (loginResult.Succeeded)
                {

                    string rolesFlag = accountService.GetRolesKeyFlagsForUser(loginResult.LoggedUser.Id);


                    ClaimsIdentity identity = new ClaimsIdentity(context.Options.AuthenticationType);
                    identity.AddClaim(new Claim("sub", context.UserName));
                    identity.AddClaim(new Claim("role", rolesFlag));

                    AuthenticationProperties props;
                    AuthenticationTicket authTicket;
                    long recentProjectId = 0;

                    if (rolesFlag.ToUpper() != "C")
                    {
                        IWebApiContentService mainService = unityContainer.Resolve<IWebApiContentService>();
                        var rmRecentProject = mainService.GetMostRecentProjectId();

                        if (rmRecentProject.Success)
                        {
                            recentProjectId = rmRecentProject.Value;
                        }
                    }

                    var name = string.Format("{0} {1}", loginResult.LoggedUser.Name, loginResult.LoggedUser.Surname);
                    if (string.IsNullOrWhiteSpace(name)) 
                    {
                        name = loginResult.LoggedUser.UserName;
                    }

                    props = new AuthenticationProperties(
                        new Dictionary<string, string> 
                        { 
                            { "roleFlag", rolesFlag }, 
                            { "name", name }, 
                            {"rpId", recentProjectId.ToString() }  
                        }
                    );

                    authTicket = new AuthenticationTicket(identity, props);
                    context.Validated(authTicket);

                }
                else
                {
                    context.SetError("invalid_grant", loginResult.FailMessage);
                    return;
                }
            }

        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            return Task.FromResult<object>(null);
        }
    }
}
