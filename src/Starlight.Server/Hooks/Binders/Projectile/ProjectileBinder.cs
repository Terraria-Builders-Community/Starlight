using Terraria;

namespace Starlight
{
    internal static class ProjectileBinder
    {
        private static HookCaller _caller = null!;

        public static void Bind(HookCaller caller)
        {
            _caller = caller;

            On.Terraria.Projectile.SetDefaults += OnSetDefaults;
            On.Terraria.Projectile.AI += OnAI;
        }

        private static void OnSetDefaults(On.Terraria.Projectile.orig_SetDefaults orig, Projectile projectile, int type)
        {
            var args = new OnSetProjectileDefaultArgs();

            var result = _caller.OnSetDefaultsAsync(args)
                .GetAwaiter().GetResult();

            if (result.Handled)
                return;

            orig(projectile, type);
        }

        private static void OnAI(On.Terraria.Projectile.orig_AI orig, Projectile projectile)
        {
            var args = new OnProjectileAIUpdateArgs();

            var result = _caller.OnProjectileAIUpdateAsync(args)
                .GetAwaiter().GetResult();

            if (result.Handled)
                return;

            orig(projectile);
        }
    }
}
