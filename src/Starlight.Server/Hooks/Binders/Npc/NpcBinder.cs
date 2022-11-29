using OTAPI;
using Terraria;


namespace Starlight
{
    internal static class NpcBinder
    {
        private static HookCaller _caller = null!;

        public static void Bind(HookCaller caller)
        {
            _caller = caller;

            On.Terraria.NPC.SetDefaults += OnSetDefaultsById;
            On.Terraria.NPC.SetDefaultsFromNetId += OnSetDefaultsFromNetId;
            On.Terraria.NPC.StrikeNPC += OnStrike;
            On.Terraria.NPC.Transform += OnTransform;
            On.Terraria.NPC.AI += OnNPCAIUpdate;

            Hooks.NPC.Spawn += OnSpawn;

            //someone take a lot at these two please. no idea if im implementing OnDropLoot correctly and OnBossBagItem is almost the exact same. need a brain that isn't completely exhausted
            Hooks.NPC.DropLoot += OnDropLoot;
            Hooks.NPC.BossBag += OnBossBagItem;

            Hooks.NPC.Killed += OnKilled;
        }

        private static void OnKilled(object? sender, Hooks.NPC.KilledEventArgs e)
        {
            var args = new OnNPCKilledArgs(e.Npc);

            _ = _caller.OnNPCKilledEventAsync(args);
        }

        // may need someone to take a look at these two... the tsapi method is confuszing me. and im quite frankly exhausted
        private static void OnDropLoot(object? sender, Hooks.NPC.DropLootEventArgs e)
        {
            if (e.Event is HookEvent.Before)
            {
                var args = new OnNPCLootDropArgs(null!, new(e.X, e.Y), e.Width, e.Height, e.Stack, e.Type, e.NoBroadcast, e.Pfix, e.Npc.type, e.Npc.netID, e.NoGrabDelay, e.ReverseLookup);

                var result = _caller.OnNPCDropLootAsync(args)
                    .GetAwaiter().GetResult();

                if (result.Handled)
                    e.Result = HookResult.Cancel;

                e.X = (int)args.Position.X;
                e.Y = (int)args.Position.Y;

                e.Width = args.Width;
                e.Height = args.Height;
                e.Type = args.ItemId;
                e.Stack = args.Stack;
                e.NoBroadcast = args.Broadcast;
                e.Pfix = args.Prefix;
                e.NoGrabDelay = args.NoGrabDelay;
                e.ReverseLookup = args.ReverseLookup;
            }
        }
        private static void OnBossBagItem(object? sender, Hooks.NPC.BossBagEventArgs e)
        {
            var args = new OnBossBagDropArgs(new(e.X, e.Y), e.Width, e.Height, e.Stack, e.Type, e.NoBroadcast, e.Pfix, e.Npc.type, e.Npc.netID, e.NoGrabDelay, e.ReverseLookup);

            var result = _caller.OnBossBagItemAsync(args)
                .GetAwaiter().GetResult();

            if (result.Handled)
                e.Result = HookResult.Cancel;

            e.X = (int)args.Position.X;
            e.Y = (int)args.Position.Y;

            e.Width = args.Width;
            e.Height = args.Height;
            e.Type = args.ItemId;
            e.Stack = args.Stack;
            e.NoBroadcast = args.Broadcast;
            e.Pfix = args.Prefix;
            e.NoGrabDelay = args.NoGrabDelay;
            e.ReverseLookup = args.ReverseLookup;
        }

        private static void OnSpawn(object? sender, Hooks.NPC.SpawnEventArgs e)
        {
            var args = new OnNPCSpawnArgs(e.Index, Main.npc[e.Index].type);

            var result = _caller.OnNPCSpawnEventAsync(args)
                .GetAwaiter().GetResult();

            if (result.Handled)
                e.Result = HookResult.Cancel;

            e.Index = args.Type;
        }

        private static void OnNPCAIUpdate(On.Terraria.NPC.orig_AI orig, NPC self)
        {
            var args = new OnNPCAIUpdateArgs(self);

            var result = _caller.OnNPCAIUpdateAsync(args)
                .GetAwaiter().GetResult();

            if (result.Handled)
                return;

            orig(self);
        }

        private static void OnTransform(On.Terraria.NPC.orig_Transform orig, NPC self, int newType)
        {
            var args = new OnTransformArgs(self.whoAmI);

            var result = _caller.OnTransformAsync(args)
                .GetAwaiter().GetResult();

            if (result.Handled)
                return;

            orig(self, newType);
        }

        private static double OnStrike(On.Terraria.NPC.orig_StrikeNPC orig, NPC self, int Damage, float knockBack, int hitDirection, bool crit, bool noEffect, bool fromNet, Entity entity)
        {
            if (entity is Player player)
            {
                var args = new OnStrikeArgs(player, self, Damage, knockBack, hitDirection, crit, noEffect, fromNet);

                var result = _caller.OnStrikeAsync(args)
                    .GetAwaiter().GetResult();

                if (result.Handled)
                    return 0;
            }

            return orig(self, Damage, knockBack, hitDirection, crit, noEffect, fromNet, entity);
        }

        private static void OnSetDefaultsById(On.Terraria.NPC.orig_SetDefaults orig, NPC npc, int type, NPCSpawnParams spawnparams)
        {
            var args = new OnSetNPCDefaultArgs(npc, type);

            var result = _caller.OnSetDefaultsAsync(args)
                .GetAwaiter().GetResult();

            if (result.Handled)
                return;

            type = args.Type;

            orig(npc, type, spawnparams);
        }

        private static void OnSetDefaultsFromNetId(On.Terraria.NPC.orig_SetDefaultsFromNetId orig, NPC npc, int id, NPCSpawnParams spawnparams)
        {
            var args = new OnSetNPCDefaultArgs(npc, id);

            var result = _caller.OnSetNetDefaultsAsync(args)
                .GetAwaiter().GetResult();

            if (result.Handled)
                return;

            id = args.Type;

            orig(npc, id, spawnparams);
        }
    }
}
