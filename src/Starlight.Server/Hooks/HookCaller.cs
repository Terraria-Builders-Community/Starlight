using CSF;
using Starlight.Extensions;

namespace Starlight
{
    public class HookCaller
    {
        private readonly IReadOnlySet<ChatResolver> _chatResolvers;
        private readonly IReadOnlySet<GameResolver> _gameResolvers;
        private readonly IReadOnlySet<ItemResolver> _itemResolvers;
        private readonly IReadOnlySet<NetResolver> _netResolvers;
        private readonly IReadOnlySet<NpcResolver> _npcResolvers;
        private readonly IReadOnlySet<PlayerResolver> _playerResolvers;
        private readonly IReadOnlySet<ProjectileResolver> _projectileResolvers;
        private readonly IReadOnlySet<WiringResolver> _wiringResolvers;
        private readonly IReadOnlySet<WorldResolver> _worldResolvers;

        private readonly CommandFramework _framework;
        private readonly IServiceProvider _provider;

        public ILogger<HookCaller> Logger { get; }

        public HookCaller(
            IEnumerable<IResolver> resolvers,
            CommandFramework framework,
            IServiceProvider provider,
            ILogger<HookCaller> logger)
        {
            _framework = framework;
            _provider = provider;
            Logger = logger;

            resolvers = resolvers
                .OrderBy(x => x.Order)
                .OrderBy(x => x.GetType().Name);

            _chatResolvers = resolvers
                .SelectWhere<ChatResolver>()
                .ToHashSet();

            _gameResolvers = resolvers
                .SelectWhere<GameResolver>()
                .ToHashSet();

            _itemResolvers = resolvers
                .SelectWhere<ItemResolver>()
                .ToHashSet();

            _netResolvers = resolvers
                .SelectWhere<NetResolver>()
                .ToHashSet();

            _npcResolvers = resolvers
                .SelectWhere<NpcResolver>()
                .ToHashSet();

            _playerResolvers = resolvers
                .SelectWhere<PlayerResolver>()
                .ToHashSet();

            _projectileResolvers = resolvers
                .SelectWhere<ProjectileResolver>()
                .ToHashSet();

            _wiringResolvers = resolvers
                .SelectWhere<WiringResolver>()
                .ToHashSet();

            _worldResolvers = resolvers
                .SelectWhere<WorldResolver>()
                .ToHashSet();
        }

        public async Task BindAsync()
        {
            _framework.CommandExecuted += async (x, y)
                => await OnCommandResultAsync((ChatCommandContext)x, y);

            ProjectileBinder.Bind(this);
            ItemBinder.Bind(this);
            NpcBinder.Bind(this);

            NetBinder.Bind(this);
            GameBinder.Bind(this);

            await Task.CompletedTask;
        }

        //item net default
        public async Task<HandleResult> OnNetDefaultsAsync(OnSetItemDefaultArgs args)
        {
            foreach (var resolver in _itemResolvers)
            {
                var handle = await resolver.OnNetDefaultsAsync(args);

                if (handle.Handled)
                    return handle;
            }
            return HandleResult.Continue();
        }

        //npc net default
        public async Task<HandleResult> OnSetNetDefaultsAsync(OnSetNPCDefaultArgs args)
        {
            foreach (var resolver in _npcResolvers)
            {
                var handle = await resolver.OnNetDefaultsAsync(args);

                if (handle.Handled)
                    return handle;
            }
            return HandleResult.Continue();
        }

        //item set default
        public async Task<HandleResult> OnSetDefaultsAsync(OnSetItemDefaultArgs args)
        {
            foreach (var resolver in _itemResolvers)
            {
                var handle = await resolver.OnSetDefaultsAsync(args);

                if (handle.Handled)
                    return handle;
            }
            return HandleResult.Continue();
        }

        //npc set default
        public async Task<HandleResult> OnSetDefaultsAsync(OnSetNPCDefaultArgs args)
        {
            foreach (var resolver in _npcResolvers)
            {
                var handle = await resolver.OnSetDefaultsAsync(args);

                if (handle.Handled)
                    return handle;
            }
            return HandleResult.Continue();
        }

        //proj set default
        public async Task<HandleResult> OnSetDefaultsAsync(OnSetProjectileDefaultArgs args)
        {
            foreach (var resolver in _projectileResolvers)
            {
                var handle = await resolver.OnSetDefaultsAsync(args);

                if (handle.Handled)
                    return handle;
            }
            return HandleResult.Continue();
        }

        public async Task<HandleResult> OnQuickStachAsync(OnQuickStackArgs args)
        {
            foreach (var resolver in _itemResolvers)
            {
                var handle = await resolver.OnQuickStackAsync(args);

                if (handle.Handled)
                    return handle;
            }
            return HandleResult.Continue();
        }

        private bool _currentGameMenuState;
        public async Task OnGameUpdateAsync()
        {
            if (_currentGameMenuState != Terraria.Main.gameMenu)
            {
                _currentGameMenuState = Terraria.Main.gameMenu;

                if (Terraria.Main.gameMenu)
                    await OnGameWorldDisconnectAsync();
                else
                    await OnGameWorldConnectAsync();

            }

            foreach (var resolver in _gameResolvers)
            {
                var handle = await resolver.OnGameUpdateAsync();

                if (handle.Handled)
                    return;
            }
        }

        public async Task OnPostGameUpdateAsync()
        {
            foreach (var resolver in _gameResolvers)
            {
                var handle = await resolver.OnPostGameUpdateAsync();

                if (handle.Handled)
                    return;
            }
        }

        public async Task OnPostInitializeAsync()
        {
            foreach (var resolver in _gameResolvers)
            {
                var handle = await resolver.OnPostInitializeAsync();

                if (handle.Handled)
                    return;
            }
        }

        public async Task<HandleResult> OnStatueSpawnAsync(OnStatueSpawnArgs args)
        {
            foreach (var resolver in _npcResolvers)
            {
                var handle = await resolver.OnStatueSpawnAsync(args);

                if (handle.Handled)
                    return handle;
            }
            return HandleResult.Continue();
        }

        public async Task OnGameWorldConnectAsync()
        {
            foreach (var resolver in _worldResolvers)
            {
                var handle = await resolver.OnGameWorldDisconnectAsync();

                if (handle.Handled)
                    return;
            }
        }

        public async Task OnGameWorldDisconnectAsync()
        {
            foreach (var resolver in _worldResolvers)
            {
                var handle = await resolver.OnGameWorldDisconnectAsync();

                if (handle.Handled)
                    return;
            }
        }

        public async Task<HandleResult> OnProjectileAIUpdateAsync(OnProjectileAIUpdateArgs args)
        {
            foreach (var resolver in _projectileResolvers)
            {
                var handle = await resolver.OnProjectileAIUpdateAsync(args);

                if (handle.Handled)
                    return handle;
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

            foreach (var resolver in _chatResolvers)
            {
                var handle = await resolver.OnChatAsync(args);

                if (handle.Handled)
                    return handle;
            }
            return HandleResult.Continue();
        }

        public async Task<HandleResult> OnCommandResultAsync(ChatCommandContext context, IResult result)
        {
            foreach (var resolver in _chatResolvers)
            {
                var handle = await resolver.OnPostCommandAsync(new(result, context));

                if (handle.Handled)
                    return handle;
            }
            return HandleResult.Continue();
        }

        public async Task<HandleResult> OnCommandAsync(ChatCommandContext context)
        {
            foreach (var resolver in _chatResolvers)
            {
                var handle = await resolver.OnPreCommandAsync(new(context));

                if (handle.Handled)
                    return HandleResult.Break();
            }
            return HandleResult.Continue();
        }

        public async Task<HandleResult> OnReceiveDataAsync(OnReceiveDataArgs args)
        {
            foreach (var resolver in _netResolvers)
            {
                var handle = await resolver.OnReceiveDataAsync(args);

                if (handle.Handled)
                    return HandleResult.Break();
            }
            return HandleResult.Continue();
        }

        public async Task<HandleResult> OnSendBytesAsync(OnSendBytesArgs args)
        {
            foreach (var resolver in _netResolvers)
            {
                var handle = await resolver.OnSendBytesAsync(args);

                if (handle.Handled)
                    return HandleResult.Break();
            }
            return HandleResult.Continue();
        }

        public async Task<HandleResult> OnSendNetDataAsync(OnSendNetDataArgs args)
        {
            foreach (var resolver in _netResolvers)
            {
                var handle = await resolver.OnSendNetDataAsync(args);

                if (handle.Handled)
                    return HandleResult.Break();
            }
            return HandleResult.Continue();
        }

        public async Task<HandleResult> OnSendDataAsync(OnSendDataArgs args)
        {
            foreach (var resolver in _netResolvers)
            {
                var handle = await resolver.OnSendDataAsync(args);

                if (handle.Handled)
                    return HandleResult.Break();
            }
            return HandleResult.Continue();
        }

        public async Task<HandleResult> OnGreetPlayerAsync(OnGreetPlayerArgs args)
        {
            foreach (var resolver in _playerResolvers)
            {
                var handle = await resolver.OnGreetPlayerAsync(args);

                if (handle.Handled)
                    return HandleResult.Break();
            }
            return HandleResult.Continue();
        }

        public async Task<HandleResult> OnNameCollisionAsync(OnNameCollisionArgs args)
        {
            foreach (var resolver in _playerResolvers)
            {
                var handle = await resolver.OnNameCollisionAsync(args);

                if (handle.Handled)
                    return HandleResult.Break();
            }
            return HandleResult.Continue();
        }

        public async Task<HandleResult> OnChatBroadcastAsync(OnBroadcastArgs args)
        {
            foreach (var resolver in _chatResolvers)
            {
                var handle = await resolver.OnChatBroadcastAsync(args);

                if (handle.Handled)
                    return HandleResult.Break();
            }
            return HandleResult.Continue();
        }

        public async Task<HandleResult> OnHardmodeTileUpdateAsync(OnHardmodeTileUpdateArgs args)
        {
            foreach (var resolver in _worldResolvers)
            {
                var handle = await resolver.OnHardmodeTileUpdateAsync(args);

                if (handle.Handled)
                    return handle;
            }
            return HandleResult.Continue();
        }

        public async Task<HandleResult> OnHardmodeTilePlaceAsync(OnHardmodeTilePlaceArgs args)
        {
            foreach (var resolver in _worldResolvers)
            {
                var handle = await resolver.OnHardmodeTilePlaceAsync(args);

                if (handle.Handled)
                    return handle;
            }
            return HandleResult.Continue();
        }

        public async Task<HandleResult> OnStrikeAsync(OnStrikeArgs args)
        {
            foreach (var resolver in _npcResolvers)
            {
                var handle = await resolver.OnStrikeAsync(args);

                if (handle.Handled)
                    return handle;
            }
            return HandleResult.Continue();
        }

        public async Task<HandleResult> OnNPCAIUpdateAsync(OnNPCAIUpdateArgs args)
        {
            foreach (var resolver in _npcResolvers)
            {
                var handle = await resolver.OnNPCAIUpdateAsync(args);

                if (handle.Handled)
                    return handle;
            }
            return HandleResult.Continue();
        }

        public async Task<HandleResult> OnNPCSpawnEventAsync(OnNPCSpawnArgs args)
        {
            foreach (var resolver in _npcResolvers)
            {
                var handle = await resolver.OnNPCSpawnAsync(args);

                if (handle.Handled)
                    return handle;
            }
            return HandleResult.Continue();
        }

        public async Task<HandleResult> OnNPCKilledEventAsync(OnNPCKilledArgs args)
        {
            foreach (var resolver in _npcResolvers)
            {
                var handle = await resolver.OnNPCKilledAsync(args);

                if (handle.Handled)
                    return handle;
            }
            return HandleResult.Continue();
        }

        public async Task<HandleResult> OnTransformAsync(OnTransformArgs args)
        {
            foreach (var resolver in _npcResolvers)
            {
                var handle = await resolver.OnTransformAsync(args);

                if (handle.Handled)
                    return handle;
            }
            return HandleResult.Continue();
        }

        public async Task<HandleResult> OnNPCDropLootAsync(OnNPCLootDropArgs args)
        {
            foreach (var resolver in _npcResolvers)
            {
                var handle = await resolver.OnNPCDropLootAsync(args);

                if (handle.Handled)
                    return handle;
            }
            return HandleResult.Continue();
        }

        public async Task<HandleResult> OnBossBagItemAsync(OnBossBagDropArgs args)
        {
            foreach (var resolver in _npcResolvers)
            {
                var handle = await resolver.OnBossBagItemAsync(args);

                if (handle.Handled)
                    return handle;
            }
            return HandleResult.Continue();
        }

        public async Task<HandleResult> OnServerCommandAsync(OnCommandProcessArgs args)
        {
            foreach (var resolver in _chatResolvers)
            {
                var handle = await resolver.OnServerCommandAsync(args);

                if (handle.Handled)
                    return handle;
            }
            return HandleResult.Continue();
        }

        public async Task<HandleResult> OnLeavePlayerAsync(OnLeavePlayerArgs args)
        {
            foreach (var resolver in _playerResolvers)
            {
                var handle = await resolver.OnLeavePlayerAsync(args);

                if (handle.Handled)
                    return handle;
            }
            return HandleResult.Continue();
        }

        public async Task<HandleResult> OnSocketResetAsync(OnSocketResetArgs args)
        {
            foreach (var resolver in _netResolvers)
            {
                var handle = await resolver.OnSocketResetAsync(args);

                if (handle.Handled)
                    return handle;
            }
            return HandleResult.Continue();
        }
    }
}
