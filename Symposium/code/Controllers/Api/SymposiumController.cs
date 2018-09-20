using Google.Cloud.Dialogflow.V2;
using Helixbase.Feature.Symposium.Services;
using Helixbase.Foundation.Logging.Repositories;
using Sitecore.Mvc.Controllers;
using System.Web.Mvc;

namespace Helixbase.Feature.Symposium.Controllers.Api
{
    public class SymposiumController : SitecoreController
    {
        private readonly ILogRepository _logRepository;
        private readonly IDialogFlowService _dialogFlowService;

        public SymposiumController(ILogRepository logRepository, IDialogFlowService dialogFlowService)
        {
            _logRepository = logRepository;
            _dialogFlowService = dialogFlowService;
        }

        //Test using the nuget package
        [HttpPost]
        public dynamic ProcessDialogflowIntent(WebhookRequest dialogflowRequest)
        {
            var response = _dialogFlowService.ProcessDialogFlowRequest(dialogflowRequest);
           
            return new ContentResult { Content = response, ContentType = "application/json" };
        }
    }
}