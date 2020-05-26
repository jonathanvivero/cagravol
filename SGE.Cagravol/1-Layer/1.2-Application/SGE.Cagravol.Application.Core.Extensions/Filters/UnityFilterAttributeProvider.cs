using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SGE.Cagravol.Application.Core.Extensions.Filters
{
    public class UnityFilterAttributeProvider : FilterAttributeFilterProvider
    {
        public class UnityFilterAttributeFilterProvider : FilterAttributeFilterProvider
        {
            private readonly IUnityContainer container;

            public UnityFilterAttributeFilterProvider(IUnityContainer container)
            {
                this.container = container;
            }

            protected override IEnumerable<FilterAttribute> GetControllerAttributes(
                        ControllerContext controllerContext,
                        ActionDescriptor actionDescriptor)
            {
                var attributes = base.GetControllerAttributes(controllerContext,
                                                              actionDescriptor);
                foreach (var attribute in attributes)
                {
                    container.BuildUp(attribute.GetType(), attribute);
                }

                return attributes;
            }

            public override IEnumerable<Filter> GetFilters(ControllerContext controllerContext, ActionDescriptor actionDescriptor)
            {
                var attributes = base.GetFilters(controllerContext, actionDescriptor);

                foreach (var attribute in attributes)
                {
                    container.BuildUp(attribute.GetType(), attribute);
                }

                return attributes;
            }

            protected override IEnumerable<FilterAttribute> GetActionAttributes(
                        ControllerContext controllerContext,
                        ActionDescriptor actionDescriptor)
            {
                var attributes = base.GetActionAttributes(controllerContext,
                    actionDescriptor);

                foreach (var attribute in attributes)
                {
                    container.BuildUp(attribute.GetType(), attribute);
                }

                return attributes;
            }
        }
    }
}
