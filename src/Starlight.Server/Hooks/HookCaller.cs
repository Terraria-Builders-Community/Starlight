using CSF;

namespace Starlight
{
    public class HookCaller
    {
        private readonly IReadOnlyList<HookResolver> _resolvers;
        private readonly CommandFramework _framework;
        private readonly IServiceProvider _provider;

        public HookCaller(IEnumerable<HookResolver> resolvers, CommandFramework framework, IServiceProvider provider)
        {
            _framework = framework;
            _provider = provider;

            _resolvers = resolvers
                .OrderBy(x => x.Order)
                .OrderBy(x => x.GetType().Name)
                .ToList();

            _framework.CommandExecuted += async (x, y) 
                => await OnCommandResultAsync((ChatCommandContext)x, y);
        }

        public async Task AttachAsync()
        {
            
            await Task.CompletedTask;
        }

        public async Task OnChatAsync(OnChatArgs args)
        {
            var raw = args.RawMessage;
            if (_framework.TryParsePrefix(ref raw, out var prefix))
            {
                var context = new ChatCommandContext(raw, prefix);

                var preCommandHandle = await OnCommandAsync(context);

                if (preCommandHandle.Handled)
                    return;

                await _framework.ExecuteCommandAsync(context, _provider);

                return;
            }

            foreach (var resolver in _resolvers)
            {
                var chatHandle = await resolver.OnChatAsync(args);

                if (chatHandle.Handled)
                    return;
            }
        }

        public async Task OnCommandResultAsync(ChatCommandContext context, IResult result)
        {
            foreach (var resolver in _resolvers)
            {
                var commandHandle = await resolver.OnPostCommandAsync(new(result, context));

                if (commandHandle.Handled)
                    return;
            }
        }

        public async Task<HandleResult> OnCommandAsync(ChatCommandContext context)
        {
            foreach (var resolver in _resolvers)
            {
                var commandHandle = await resolver.OnPreCommandAsync(new(context));

                if (commandHandle.Handled)
                    return HandleResult.Break();
            }
            return HandleResult.Continue();
        }
    }
}
