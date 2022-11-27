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
            await Task.CompletedTask;
        }
    }
}