using Glass.Mapper.Sc;
using Helixbase.Feature.Symposium.Models;
using Helixbase.Foundation.Content.Repositories;
using Helixbase.Foundation.ORM.Models;
using Sitecore.SecurityModel;

namespace Helixbase.Feature.Symposium.Factories
{
    public class ComponentFactory : IComponentFactory
    {
        private readonly IContentRepository _contentRepository;

        public ComponentFactory(IContentRepository contentRepository)
        {
            _contentRepository = contentRepository;
        }

        public bool StubComponent(string ComponentName, string LayerName)
        {
            var renderingParent = _contentRepository.GetItem<ISitecoreItem>(new GetItemByPathOptions { Path = $"/sitecore/layout/Renderings/{LayerName}" });
            var renderingChild = new ControllerRenderingItem
            {
                Name = ComponentName
            };

            var dataTemplateParent = _contentRepository.GetItem<ISitecoreItem>(new GetItemByPathOptions { Path = $"/sitecore/templates/{LayerName}" });
            var dataTemplateChild = new DataTemplateItem
            {
                Name = $"_{ComponentName}"
            };

            using (new SecurityDisabler())
            {
                //create the rendering
                _contentRepository.CreateItem(new CreateByModelOptions { Model = renderingChild, Parent = renderingParent });

                //Create the interface template
                _contentRepository.CreateItem(new CreateByModelOptions { Model = dataTemplateChild, Parent = dataTemplateParent });
            }

            return true;
        }
    }
}