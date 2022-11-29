using OTAPI;
using Terraria;
using Terraria.GameContent.Items;

namespace Starlight
{
    internal static class ItemBinder
    {
        private static HookCaller _caller = null!;

        public static void Bind(HookCaller caller)
        {
            _caller = caller;

            On.Terraria.Item.SetDefaults_int_bool_ItemVariant += OnSetDefaults;
            On.Terraria.Item.netDefaults += OnNetDefaults;
            Hooks.Chest.QuickStack += OnQuickStack;
        }

        private static void OnNetDefaults(On.Terraria.Item.orig_netDefaults orig, Item item, int type)
        {
            var args = new OnSetItemDefaultArgs(item, type, null);

            var result = _caller.OnNetDefaultsAsync(args)
                .GetAwaiter().GetResult();

            if (result.Handled)
                return;

            type = args.Type;

            orig(item, type);
        }

        private static void OnSetDefaults(On.Terraria.Item.orig_SetDefaults_int_bool_ItemVariant orig, Item item, int type, bool noMatCheck, ItemVariant? variant = null)
        {
            var args = new OnSetItemDefaultArgs(item, type, variant);

            var result = _caller.OnSetDefaultsAsync(args)
                .GetAwaiter().GetResult();

            if (result.Handled)
                return;

            type = args.Type;

            orig(item, type, noMatCheck, variant);
        }

        private static void OnQuickStack(object? sender, Hooks.Chest.QuickStackEventArgs e)
        {
            var args = new OnQuickStackArgs(Main.player[e.PlayerId], Main.chest[e.ChestIndex], e.Item);

            var result = _caller.OnQuickStachAsync(args)
                .GetAwaiter().GetResult();

            if (result.Handled)
                e.Result = HookResult.Cancel;
        }
    }
}
