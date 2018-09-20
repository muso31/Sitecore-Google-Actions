using Google.Cloud.Dialogflow.V2;
using Google.Protobuf.WellKnownTypes;
using Helixbase.Feature.Symposium.Factories;

namespace Helixbase.Feature.Symposium.Services
{
    public class DialogFlowService : IDialogFlowService
    {
        private readonly IPublishService _publishService;
        private readonly IComponentFactory _componentFactory;

        public DialogFlowService(IPublishService publishService, IComponentFactory componentFactory)
        {
            _publishService = publishService;
            _componentFactory = componentFactory;
        }

        public string ProcessDialogFlowRequest(WebhookRequest dialogflowRequest) {

            var fulfilmentText = string.Empty;

            if (dialogflowRequest.QueryResult.Action.Equals("publish.items"))
            {
                // Hard code because dialogflowRequest.QueryResult.Parameters don't map - BUG
                if (dialogflowRequest.QueryResult.QueryText.ToLower().Contains("smart"))
                    _publishService.PublishSite("smart");

                fulfilmentText = $"{dialogflowRequest.QueryResult.FulfillmentText}";
            }

            if (dialogflowRequest.QueryResult.Action.Equals("count.items"))
            {
                var itemAmount = _publishService.CountItems();

                fulfilmentText = $"{Constants.FulfilmentText.ItemCount} {itemAmount}";
            }

            if (dialogflowRequest.QueryResult.Action.Equals("unlinked.items"))
            {
                var itemAmount = _publishService.CountUnlinkedItems();

                fulfilmentText = $"{Constants.FulfilmentText.UnlinkedItemCount} {itemAmount}";
            }

            if (dialogflowRequest.QueryResult.Action.Equals("stub.component"))
            {
                // Hard code because dialogflowRequest.QueryResult.Parameters don't map - BUG?
                if (dialogflowRequest.QueryResult.QueryText.ToLower().Contains("feature"))
                    _componentFactory.StubComponent("TestComponent", "Feature");

                if (dialogflowRequest.QueryResult.QueryText.ToLower().Contains("foundation"))
                    _componentFactory.StubComponent("TestComponent", "Foundation");

                if (dialogflowRequest.QueryResult.QueryText.ToLower().Contains("project"))
                    _componentFactory.StubComponent("TestComponent", "Project");

                fulfilmentText = $"{dialogflowRequest.QueryResult.FulfillmentText}";
            }

            var googlePayload = new Struct
            {
                Fields = { { "expectUserResponse", Value.ForBool(false) } }
            };

            var dialogflowResponse = new WebhookResponse
            {
                // TODO: remove requirement for response
                // Only returning basic data so Google will expect a user response
                FulfillmentText = fulfilmentText,
                Payload = new Struct
                {
                    Fields = { { "google", Value.ForStruct(googlePayload) } }
                }
            };

            return dialogflowResponse.ToString();
        }
    }
}