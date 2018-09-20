using Helixbase.Feature.Symposium.Models;
using Helixbase.Foundation.Logging.Repositories;
using Sitecore.Globalization;
using Sitecore.Publishing;
using System;

namespace Helixbase.Feature.Symposium.Respositories
{
    public class PublishRepository : IPublishRepository
    {
        private readonly ILogRepository _logRepository;

        public PublishRepository(ILogRepository logRepository)
        {
            _logRepository = logRepository;
        }

        public bool PublishSite(PublishConfiguration publishConfig)
        {
            using (new Sitecore.SecurityModel.SecurityDisabler())
            {
                var webDb = Sitecore.Configuration.Factory.GetDatabase("web");
                var masterDb = Sitecore.Configuration.Factory.GetDatabase("master");

                try
                {
                    foreach (Language language in masterDb.Languages)
                    {
                        //loop languages and perform full republish on the Helixbase site
                        var options = new PublishOptions(masterDb, webDb, publishConfig.PublishMode, language, DateTime.Now)
                        {
                            RootItem = masterDb.Items[publishConfig.RootPath],
                            RepublishAll = publishConfig.RepublishAll,
                            Deep = publishConfig.Deep
                        };

                        var myPublisher = new Publisher(options);
                        myPublisher.Publish();
                    }
                }
                catch (Exception ex)
                {
                    _logRepository.Error($"Could not publish {ex}");
                    return false;
                }

                return true;
            }
        }
    }
}