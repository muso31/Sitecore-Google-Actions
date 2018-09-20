using Sitecore.Publishing;

namespace Helixbase.Feature.Symposium.Models
{
    public class PublishConfiguration
    {
        public PublishMode PublishMode { get; set; }
        public string RootPath { get; set; }
        public bool RepublishAll { get; set; } = true;
        public bool Deep { get; set; } = true;
    }
}