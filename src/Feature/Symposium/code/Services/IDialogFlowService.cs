using Google.Cloud.Dialogflow.V2;

namespace Helixbase.Feature.Symposium.Services
{
    public interface IDialogFlowService
    {
        string ProcessDialogFlowRequest(WebhookRequest dialogflowRequest);
    }
}
