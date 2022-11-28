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
        }

        public async Task BindAsync()
        {
            _framework.CommandExecuted += async (x, y)
                => await OnCommandResultAsync((ChatCommandContext)x, y);

            ItemBinder.Bind(this);

            await Task.CompletedTask;
        }

        public async Task<HandleResult> OnNetDefaultsAsync(OnSetDefaultArgs args)
        {
            foreach (var resolver in _resolvers)
            {
                var handle = await resolver.OnNetDefaultsAsync(args);

                if (handle.Handled)
                    return HandleResult.Break();
            }
            return HandleResult.Continue();
        }

        public async Task<HandleResult> OnSetDefaultsAsync(OnSetDefaultArgs args)
        {
            foreach (var resolver in _resolvers)
            {
                var handle = await resolver.OnSetDefaultsAsync(args);

                if (handle.Handled)
                    return HandleResult.Break();
            }
            return HandleResult.Continue();
        }

        public async Task<HandleResult> OnQuickStachAsync(OnQuickStackArgs args)
        {
            foreach (var resolver in _resolvers)
            {
                var handle = await resolver.OnQuickStackAsync(args);

                if (handle.Handled)
                    return HandleResult.Break();
            }
            return HandleResult.Continue();
        }

        public async Task<HandleResult> OnChatAsync(OnChatArgs args)
        {
            var raw = args.RawMessage;
            if (_framework.TryParsePrefix(ref raw, out var prefix))
            {
                var context = new ChatCommandContext(raw, prefix);

                var preHandle = await OnCommandAsync(context);

                if (preHandle.Handled)
                    return preHandle;

                await _framework.ExecuteCommandAsync(context, _provider);

                return HandleResult.Break();
            }

            foreach (var resolver in _resolvers)
            {
                var handle = await resolver.OnChatAsync(args);

                if (handle.Handled)
                    return handle;
            }
            return HandleResult.Continue();
        }

        public async Task<HandleResult> OnCommandResultAsync(ChatCommandContext context, IResult result)
        {
            foreach (var resolver in _resolvers)
            {
                var handle = await resolver.OnPostCommandAsync(new(result, context));

                if (handle.Handled)
                    return handle;
            }
            return HandleResult.Continue();
        }

        public async Task<HandleResult> OnCommandAsync(ChatCommandContext context)
        {
            foreach (var resolver in _resolvers)
            {
                var handle = await resolver.OnPreCommandAsync(new(context));

                if (handle.Handled)
                    return HandleResult.Break();
            }
            return HandleResult.Continue();
        }

        public async Task<HandleResult> OnReceiveDataAsync(OnReceiveDataArgs args)
        {
            foreach (var resolver in _resolvers)
            {
                var handle = await resolver.OnReceiveDataAsync(args);

                if (handle.Handled)
                    return HandleResult.Break();
            }
            return HandleResult.Continue();
        }

        public async Task<HandleResult> OnSendBytesAsync(OnSendBytesArgs args)
        {
            foreach (var resolver in _resolvers)
            {
                var handle = await resolver.OnSendBytesAsync(args);

                if (handle.Handled)
                    return HandleResult.Break();
            }
            return HandleResult.Continue();
        }

        public async Task<HandleResult> OnSendNetDataAsync(OnSendNetDataArgs args)
        {
            foreach (var resolver in _resolvers)
            {
                var handle = await resolver.OnSendNetDataAsync(args);

                if (handle.Handled)
                    return HandleResult.Break();
            }
            return HandleResult.Continue();
        }

        public async Task<HandleResult> OnSendDataAsync(OnSendDataArgs args)
        {
            foreach (var resolver in _resolvers)
            {
                var handle = await resolver.OnSendDataAsync(args);

                if (handle.Handled)
                    return HandleResult.Break();
            }
            return HandleResult.Continue();
        }

        public async Task<HandleResult> OnGreetPlayerAsync(OnGreetPlayerArgs args)
        {
            foreach (var resolver in _resolvers)
            {
                var handle = await resolver.OnGreetPlayerAsync(args);

                if (handle.Handled)
                    return HandleResult.Break();
            }
            return HandleResult.Continue();
        }

        public async Task<HandleResult> OnNameCollisionAsync(OnNameCollisionArgs args)
        {
            foreach (var resolver in _resolvers)
            {
                var handle = await resolver.OnNameCollisionAsync(args);

                if (handle.Handled)
                    return HandleResult.Break();
            }
            return HandleResult.Continue();
        }

        public async Task<HandleResult> OnChatBroadcastAsync(OnBroadcastArgs args)
        {
            foreach (var resolver in _resolvers)
            {
                var handle = await resolver.OnChatBroadcastAsync(args);

                if (handle.Handled)
                    return HandleResult.Break();
            }
            return HandleResult.Continue();
        }
    }
}
