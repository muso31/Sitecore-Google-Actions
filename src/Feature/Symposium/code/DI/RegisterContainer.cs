using Helixbase.Feature.Symposium.Factories;
using Helixbase.Feature.Symposium.Respositories;
using Helixbase.Feature.Symposium.Services;
using Microsoft.Extensions.DependencyInjection;
using Sitecore.DependencyInjection;

namespace Helixbase.Feature.Symposium.DI
{
    public class RegisterContainer : IServicesConfigurator
    {
        public void Configure(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IPublishService, PublishService>();
            serviceCollection.AddTransient<IPublishRepository, PublishRepository>();
            serviceCollection.AddTransient<IDialogFlowService, DialogFlowService>();
            serviceCollection.AddTransient<IComponentFactory, ComponentFactory>();
        }
    }
}