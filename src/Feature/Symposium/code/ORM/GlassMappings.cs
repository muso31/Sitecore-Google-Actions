using Glass.Mapper.Sc.Maps;
using Helixbase.Feature.Symposium.Models;

namespace Helixbase.Feature.Symposium.ORM
{
    public class GlassMappings : SitecoreGlassMap<ControllerRenderingItem>
    {
        public override void Configure()
        {
            Map(config =>
            {
                config.AutoMap();
                config.TemplateId(Constants.Templates.ControllerRendering);
                config.Info(f => f.Name).InfoType(Glass.Mapper.Sc.Configuration.SitecoreInfoType.Name);
            });
        }
    }

    public class GlassMappingsDataTemplate : SitecoreGlassMap<DataTemplateItem>
    {
        public override void Configure()
        {
            Map(config =>
            {
                config.AutoMap();
                config.TemplateId(Constants.Templates.DataTemplate);
                config.Info(f => f.Name).InfoType(Glass.Mapper.Sc.Configuration.SitecoreInfoType.Name);
            });
        }
    }
}