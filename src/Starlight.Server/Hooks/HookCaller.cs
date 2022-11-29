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
            NetBinder.Bind(this);

            await Task.CompletedTask;
        }
        //item net default
        public async Task<HandleResult> OnNetDefaultsAsync(OnSetItemDefaultArgs args)
        {
            foreach (var resolver in _resolvers)
            {
                var handle = await resolver.OnNetDefaultsAsync(args);

                if (handle.Handled)
                    return HandleResult.Break();
            }
            return HandleResult.Continue();
        }

        //npc net default
        public async Task<HandleResult> OnSetNetDefaultsAsync(OnSetNPCDefaultArgs args)
        {
            foreach (var resolver in _resolvers)
            {
                var handle = await resolver.OnNetDefaultsAsync(args);

                if (handle.Handled)
                    return HandleResult.Break();
            }
            return HandleResult.Continue();
        }

        //ITEM SET DEFAULT
        public async Task<HandleResult> OnSetDefaultsAsync(OnSetItemDefaultArgs args)
        {
            foreach (var resolver in _resolvers)
            {
                var handle = await resolver.OnSetDefaultsAsync(args);

                if (handle.Handled)
                    return HandleResult.Break();
            }
            return HandleResult.Continue();
        }

        //NPC SET DEFAULT
        public async Task<HandleResult> OnSetDefaultsAsync(OnSetNPCDefaultArgs args)
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


        private bool currentGameMenuState;
        public async Task OnGameUpdateAsync()
        {
            if(currentGameMenuState != Terraria.Main.gameMenu)
            {
                currentGameMenuState = Terraria.Main.gameMenu;

                if (Terraria.Main.gameMenu)
                    await OnGameWorldDisconnect();
                else
                    await OnGameWorldConnect();
                            
            }

            foreach (var resolver in _resolvers)
            {
                var handle = await resolver.OnGameUpdateAsync();

                if (handle.Handled)
                    break;
            }

            return;

        }


        public async Task OnPostGameUpdateAsync()
        {
            foreach (var resolver in _resolvers)
            {
                var handle = await resolver.OnPostGameUpdateAsync();

                if (handle.Handled)
                    break;
            }

            return;
        }

        public async Task OnPostInitializeAsync()
        {
            foreach (var resolver in _resolvers)
            {
                var handle = await resolver.OnPostInitializeAsync();

                if (handle.Handled)
                    break;
            }
            return;
        }


        public async Task<HandleResult> OnStatueSpawnAsync(OnStatueSpawnArgs args)
        {
            foreach (var resolver in _resolvers)
            {
                var handle = await resolver.OnStatueSpawnAsync();

                if (handle.Handled)
                    return handle;
            }
            return HandleResult.Continue();
        }

        //WILL DO (LATER :>)
        public async Task OnGameWorldConnect()
        {
            return;
        }

        public async Task OnGameWorldDisconnect()
        {
            return;
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

        public async Task<HandleResult> OnHardmodeTileUpdateAsync(OnHardmodeTileUpdateArgs args)
        {
            foreach(var resolver in _resolvers)
            {
                var handle = await resolver.OnHardmodeTileUpdateAsync(args);

                if (handle.Handled)
                    return handle;
            }
            return HandleResult.Continue();
        }

        public async Task<HandleResult> OnHardmodeTilePlaceAsync(OnHardmodeTilePlaceArgs args)
        {
            foreach (var resolver in _resolvers)
            {
                var handle = await resolver.OnHardmodeTilePlaceAsync(args);

                if (handle.Handled)
                    return handle;
            }
            return HandleResult.Continue();
        }

        public async Task<HandleResult> OnStrikeAsync(OnStrikeEventArgs args)
        {

            foreach (var resolver in _resolvers)
            {
                var handle = await resolver.OnStrikeAsync(args);

                if (handle.Handled)
                    return handle;
            }
            return HandleResult.Continue();
        }

        public async Task<HandleResult> OnNPCAIUpdateAsync(OnNPCAIUpdateArgs args)
        {
            foreach (var resolver in _resolvers)
            {
                var handle = await resolver.OnNPCAIUpdateAsync(args);

                if (handle.Handled)
                    return handle;
            }
            return HandleResult.Continue();
        }

        public async Task<HandleResult> OnNPCSpawnEventAsync(OnNPCSpawnEventArgs args)
        {
            foreach (var resolver in _resolvers)
            {
                var handle = await resolver.OnNPCSpawnEventAsync(args);

                if (handle.Handled)
                    return handle;
            }
            return HandleResult.Continue();
        }

        public async Task<HandleResult> OnNPCKilledEventAsync(NPCKilledEventArgs args)
        {
            foreach (var resolver in _resolvers)
            {
                var handle = await resolver.OnNPCKilledEventAsync(args);

                if (handle.Handled)
                    return handle;
            }
            return HandleResult.Continue();
        }

        public async Task<HandleResult> OnTransformAsync(OnTransformArgs args)
        {
            foreach (var resolver in _resolvers)
            {
                var handle = await resolver.OnTransformAsync(args);

                if (handle.Handled)
                    return handle;
            }
            return HandleResult.Continue();
        }

        public async Task<HandleResult> OnNPCDropLootEventAsync(NPCLootDropEventArgs args)
        {
            foreach (var resolver in _resolvers)
            {
                var handle = await resolver.OnNPCDropLootEventAsync(args);

                if (handle.Handled)
                    return handle;
            }
            return HandleResult.Continue();
        }

       
    }
}
