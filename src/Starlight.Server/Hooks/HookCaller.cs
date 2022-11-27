namespace Starlight
{
    public class HookCaller
    {
        private readonly IReadOnlyList<HookResolver> _resolvers;

        public HookCaller(IEnumerable<HookResolver> resolvers)
        {
            _resolvers = resolvers
                .OrderBy(x => x.Order)
                .OrderBy(x => x.GetType().Name)
                .ToList();
        }

        public async Task AttachAsync()
        {

        }

        public async Task OnChatAsync(OnChatArgs args)
        {
            foreach (var resolver in _resolvers)
            {
                await resolver.OnChatAsync(args);
            }
        }
    }
}
