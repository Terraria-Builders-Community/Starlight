using Microsoft.Extensions.DependencyInjection;

namespace Starlight
{
    public abstract class Plugin : IDisposable
    {
        private bool disposedValue;

        public abstract PluginInfo PluginInfo { get; }

        public Plugin()
        {

        }

        public abstract Task LoadAsync();

        public virtual Task UnloadAsync()
        {
            return Task.CompletedTask;
        }

        public virtual void ConfigureServices(IServiceCollection collection)
        {

        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // managed
                }

                // unmanaged
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
