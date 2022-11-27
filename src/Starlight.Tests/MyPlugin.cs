
namespace Starlight.Tests
{
    public class MyPlugin : Plugin
    {
        public override PluginInfo PluginInfo => new(x =>
        {
            x.Name = "MyPlugin";
            x.Version = new(1, 0, 0, 0);
        });

        public override async Task LoadAsync()
        {
            var config = await IModel.GetAsync<MyPluginConfiguration>(x => true);

            config ??= await IModel.CreateAsync<MyPluginConfiguration>(x =>
            {
                x.SomeKindOfValue = "This is some config value";
            });
        }
    }
}