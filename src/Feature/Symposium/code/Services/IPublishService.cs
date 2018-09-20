namespace Helixbase.Feature.Symposium.Services
{
    public interface IPublishService
    {
        bool PublishSite(string publishType);
        string CountItems();
        string CountUnlinkedItems();
    }
}
