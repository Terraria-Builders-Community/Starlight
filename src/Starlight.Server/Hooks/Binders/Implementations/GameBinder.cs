﻿using Microsoft.Xna.Framework;
using OTAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starlight
{
    internal static class GameBinder
    {
        private static HookCaller _caller = null!;

        public static void Bind(HookCaller caller)
        {
            _caller = caller;

            On.Terraria.Main.Update += OnUpdate;
            On.Terraria.Main.Initialize += OnInitialize;
            On.Terraria.Netplay.StartServer += OnStartServer;

            Hooks.WorldGen.HardmodeTilePlace += OnHardmodeTilePlace;
            Hooks.WorldGen.HardmodeTileUpdate += OnHardmodeTileUpdate;

            Hooks.Item.MechSpawn += OnItemMechSpawn;
            Hooks.NPC.MechSpawn += OnNpcMechSpawn;
        }

        private static void OnUpdate(On.Terraria.Main.orig_Update orig, Terraria.Main instance, GameTime gameTime)
        {
            _ = _caller.InvokeGameUpdateAsync();

            orig(instance, gameTime);

            _ = _caller.InvokeGamePostUpdateAsync();
        }

        private static void OnHardmodeTileUpdate(object? sender, Hooks.WorldGen.HardmodeTileUpdateEventArgs e)
        {
            var args = new OnHardmodeTileUpdateArgs();

            var result = _caller.OnHardmodeTileUpdateAsync(args);

            if (result.Handled)
                e.Result = HookResult.Cancel;
        }

        private static void OnHardmodeTilePlace(object? sender, Hooks.WorldGen.HardmodeTilePlaceEventArgs e)
        {
            var args = new OnHardmodeTilePlaceArgs();

            var result = _caller.OnHardmodeTilePlaceAsync(args);

            if (result.Handled)
                e.Result = HardmodeTileUpdateResult.Cancel;
        }

        private static void OnInitialize(On.Terraria.Main.orig_Initialize orig, Terraria.Main instance)
        {
            orig(instance);
        }

        private static void OnStartServer(On.Terraria.Netplay.orig_StartServer orig)
        {
            _ = _caller.OnPostInitializeAsync();
            orig();
        }

        private static void OnItemMechSpawn(object? sender, Hooks.Item.MechSpawnEventArgs e)
        {
            var args = new OnStatueSpawnArgs();

            var result = _caller.OnStatueSpawnAsync(args);

            if (result.Handled)
                e.Result = HookResult.Cancel;
        }

        private static void OnNpcMechSpawn(object? sender, Hooks.NPC.MechSpawnEventArgs e)
        {
            var args = new OnStatueSpawnArgs();

            var result = _caller.OnStatueSpawnAsync(args);

            if (result.Handled)
                e.Result = HookResult.Cancel;
        }
    }
}