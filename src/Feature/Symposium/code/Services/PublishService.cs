using Glass.Mapper.Sc;
using Helixbase.Feature.Symposium.Models;
using Helixbase.Feature.Symposium.Respositories;
using Helixbase.Foundation.Content.Repositories;
using Helixbase.Foundation.Logging.Repositories;
using Helixbase.Foundation.ORM.Models;
using Sitecore.Publishing;
using System.Linq;

namespace Helixbase.Feature.Symposium.Services
{
    public class PublishService : IPublishService
    {
        private readonly ILogRepository _logRepository;
        private readonly IContentRepository _contentRepository;
        private readonly IContextRepository _contextRepository;
        private readonly IPublishRepository _publishRepository;

        public PublishService(ILogRepository logRepository, IContentRepository contentRepository, IContextRepository contextRepository, IPublishRepository publishRepository)
        {
            _logRepository = logRepository;
            _contentRepository = contentRepository;
            _contextRepository = contextRepository;
            _publishRepository = publishRepository;
        }

        public bool PublishSite(string publishType)
        {
            PublishMode publishMode = PublishMode.Full;

            switch (publishType) {
                case  "smart" :
                    publishMode = PublishMode.Smart;
                    break;
                case "incremental":
                    publishMode = PublishMode.Incremental;
                    break;
                default:
                    publishMode = PublishMode.Full;
                    break;
            }

            var publishConfig = new PublishConfiguration() { PublishMode = publishMode, RootPath = "/sitecore/content/Helixbase" };

            return _publishRepository.PublishSite(publishConfig);

        }

        //TODO: add a context switch for ISitecoreService or this will count items from the Web DB - POC only
        public string CountItems()
        {
            var siteItems = _contentRepository.GetItems<ISitecoreItem>(new GetItemsByQueryOptions
            {
                Query = new Query($"{_contextRepository.GetContextSiteRoot()}//*"), VersionCount = false
            });

            var itemCount = siteItems.Cast<ISitecoreItem>().Count();

            return itemCount.ToString();
        }

        public string CountUnlinkedItems()
        {
            //TODO: incomplete as haven't added a context switch for ISitecoreService - POC only 
            return "5";
        }
    }
}