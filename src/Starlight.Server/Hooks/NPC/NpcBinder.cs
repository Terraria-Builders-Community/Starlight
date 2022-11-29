using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IL.Microsoft.Xna.Framework;
using IL.Terraria.GameContent.ObjectInteractions;
using OTAPI;
using Terraria;
using Terraria.GameContent.Items;


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
            var args = new NPCKilledEventArgs(e.Npc);

            var result = _caller.OnNPCKilledEventAsync(args)
                .GetAwaiter().GetResult();

            if (result.Handled)
                return;

        }



        // may need someone to take a look at these two... the tsapi method is confuszing me. and im quite frankly exhausted
        private static void OnDropLoot(object? sender, Hooks.NPC.DropLootEventArgs e)
        {
            if (e.Event == HookEvent.Before)
            {
                var args = new NPCLootDropEventArgs
                {
                    Position = new Microsoft.Xna.Framework.Vector2(e.X, e.Y),
                    Width = e.Width,
                    Height = e.Height,
                    ItemId = e.ItemIndex,
                    Stack = e.Stack,
                    Broadcast = e.NoBroadcast,
                    Prefix = e.Pfix,
                    NpcId = e.Npc.type,
                    NpcArrayIndex = e.Npc.netID,
                    NoGrabDelay = e.NoGrabDelay,
                    ReverseLookup = e.ReverseLookup
                };

                var result = _caller.OnNPCDropLootEventAsync(args)
                    .GetAwaiter().GetResult();

                if (result.Handled)
                {
                    e.Result = HookResult.Cancel;
                }

            }
        }
        private static void OnBossBagItem(object? sender, Hooks.NPC.BossBagEventArgs e)
        {
            throw new NotImplementedException();
        }
        // ^^^^^^^^^^^

        private static void OnSpawn(object? sender, Hooks.NPC.SpawnEventArgs e)
        {
            var index = e.Index;
            var type = Terraria.Main.npc[index].type;

            var args = new OnNPCSpawnEventArgs(index, type);

            var result = _caller.OnNPCSpawnEventAsync(args)
                .GetAwaiter().GetResult();

            if (result.Handled)
                return;
        }

        private static void OnNPCAIUpdate(On.Terraria.NPC.orig_AI orig, NPC self)
        {
            var args = new OnNPCAIUpdateArgs(self);

            var result = _caller.OnNPCAIUpdateAsync(args)
                .GetAwaiter().GetResult();

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
                var args = new OnStrikeEventArgs(player, self, Damage, knockBack, hitDirection, crit, noEffect, fromNet);

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

            orig(npc, type, spawnparams);
        }

        private static void OnSetDefaultsFromNetId(On.Terraria.NPC.orig_SetDefaultsFromNetId orig, NPC npc, int id, NPCSpawnParams spawnparams)
        {
            var args = new OnSetNPCDefaultArgs(npc, id);

            var result = _caller.OnSetNetDefaultsAsync(args)
             .GetAwaiter().GetResult();

            if (result.Handled)
                return;

            orig(npc, id, spawnparams);
        }

        
    }
}
