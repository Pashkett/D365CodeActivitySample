using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;
using System.Activities;

namespace SearchContactsByTwoParameters
{
    public static class ContextExtensions
    {
        public static IOrganizationService GetOrganizationServiceByUser(this CodeActivityContext context)
        {
            var workflowContext = context.GetExtension<IWorkflowContext>();
            var serviceFactory = context.GetExtension<IOrganizationServiceFactory>();
            
            return serviceFactory.CreateOrganizationService(workflowContext.UserId);
        }
    }
}
