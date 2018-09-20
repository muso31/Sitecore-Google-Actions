using Helixbase.Feature.Symposium.Models;

namespace Helixbase.Feature.Symposium.Respositories
{
    public interface IPublishRepository
    {
        bool PublishSite(PublishConfiguration publishConfig);
    }
}
