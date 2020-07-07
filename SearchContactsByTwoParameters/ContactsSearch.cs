using System;
using System.Activities;
using System.Linq;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk.Workflow;

namespace SearchContactsByTwoParameters
{
    public class ContactsSearch : CodeActivity
    {
        // Input
        [RequiredArgument]
        [Input("First searched field Name")]
        public InArgument<string> FirstInputFieldName { get; set; }
        
        [RequiredArgument]
        [Input("First searched field Value")]
        public InArgument<string> FirstInputFieldValue { get; set; }

        [RequiredArgument]
        [Input("Second searched field Name")]
        public InArgument<string> SecondInputFieldName { get; set; }

        [RequiredArgument]
        [Input("Second searched field Value")]
        public InArgument<string> SecondInputFieldValue { get; set; }

        // Output
        [Output("Int Status Result")]
        public OutArgument<int> SearchResult { get; set; }

        [Output("Contact Reference Result")]
        [ReferenceTarget("contact")]
        public OutArgument<EntityReference> ContactRecordReference { get; set; }

        // Execution
        protected override void Execute(CodeActivityContext context)
        {
            var organizationService = context.GetOrganizationServiceByUser();

            // fetchXML query to retrieve contacts
            var fetchQuerry =
                $@"<fetch>
                  <entity name = 'contact'>
                     <attribute name = 'contactid' alias = 'id'/>
                        <attribute name = 'fullname'/>
                         <filter type = 'and' >
                            <condition attribute = '{FirstInputFieldName.Get(context)}' 
                                operator= 'eq' value = '{FirstInputFieldValue.Get(context)}'/>
                            <condition attribute = '{SecondInputFieldName.Get(context)}' 
                                operator= 'eq' value = '{SecondInputFieldValue.Get(context)}'/>
                        </filter>              
                  </entity>
                </fetch> ";

                var contacts = organizationService.RetrieveMultiple(new FetchExpression(fetchQuerry));

                ProcessQuery(context, contacts);
        }

        private void ProcessQuery(CodeActivityContext context, EntityCollection contacts)
        {
            if (contacts == null || contacts?.Entities == null)
            {
                SearchResult.Set(context, 2);
                return;
            }

            var count = contacts.Entities.Count;

            switch (count)
            {
                case 0:
                    SearchResult.Set(context, 2);
                    break;
                case 1:
                    SearchResult.Set(context, 1);
                    ContactRecordReference.Set(context, contacts.Entities.FirstOrDefault().ToEntityReference());
                    break;
                default:
                    SearchResult.Set(context, 3);
                    break;
            }
        }
    }
}
