using OTAPI;
using Terraria;

namespace Starlight
{
    internal static class ServerBinder
    {
        private static HookCaller _caller = null!;

        public static void Bind(HookCaller caller)
        {
            _caller = caller;

            On.Terraria.Main.startDedInput += Main_startDedInput;
            On.Terraria.RemoteClient.Reset += RemoteClient_Reset;
            Hooks.Main.CommandProcess += OnProcess;
        }

        static void Main_startDedInput(On.Terraria.Main.orig_startDedInput orig)
        {
            if (Environment.GetCommandLineArgs().Any(x => x.Equals("-disable-commands")))
            {
                _caller.Logger.LogError("The command thread has been disabled.");
                return;
            }

            orig();
        }

        static void OnProcess(object? sender, Hooks.Main.CommandProcessEventArgs e)
        {
            var args = new OnCommandProcessArgs();

            var result = _caller.OnServerCommandArgs(args)
                .GetAwaiter().GetResult();

            if (result.Handled)
                e.Result = HookResult.Cancel;
        }

        static void RemoteClient_Reset(On.Terraria.RemoteClient.orig_Reset orig, RemoteClient client)
        {
            if (!Netplay.Disconnect)
            {
                if (client.IsActive)
                {
                    var args = new OnLeavePlayerArgs();
                    _ = _caller.OnLeavePlayerAsync(args);
                }

                var socketargs = new OnSocketResetArgs();

                _ = _caller.OnSocketResetAsync(socketargs);
            }

            orig(client);
        }
    }
}
