using Microsoft.Xna.Framework;
using OTAPI;
using Terraria;
using Terraria.Localization;
using Terraria.Net;

namespace Starlight
{
    internal static class NetBinder
    {
        private static HookCaller _caller = null!;
        private static readonly object _lock = new();

        public static void Bind(HookCaller caller)
        {
            _caller = caller;
            On.Terraria.NetMessage.greetPlayer += OnGreetPlayer;
            On.Terraria.Netplay.OnConnectionAccepted += OnConnectionAccepted;
            On.Terraria.Chat.ChatHelper.BroadcastChatMessage += OnBroadcastChatMessage;
            On.Terraria.Net.NetManager.SendData += OnSendNetData;

            Hooks.NetMessage.SendData += OnSendData;
            Hooks.NetMessage.SendBytes += OnSendBytes;
            Hooks.MessageBuffer.GetData += OnReceiveData;
            Hooks.MessageBuffer.NameCollision += OnNameCollision;

            On.Terraria.Main.startDedInput += Main_startDedInput;
            On.Terraria.RemoteClient.Reset += RemoteClient_Reset;
            Hooks.Main.CommandProcess += OnProcess;
        }

        private static void OnBroadcastChatMessage(On.Terraria.Chat.ChatHelper.orig_BroadcastChatMessage orig, NetworkText text, Color color, int excludedPlayer)
        {
            float r = color.R, g = color.G, b = color.B;

            var args = new OnBroadcastArgs();

            var result = _caller.OnChatBroadcastAsync(args)
                .GetAwaiter().GetResult();

            if (result.Handled)
                return;

            color.R = (byte)r;
            color.G = (byte)g;
            color.B = (byte)b;

            orig(text, color, excludedPlayer);
        }

        private static void OnSendData(object? sender, Hooks.NetMessage.SendDataEventArgs e)
        {
            if (e.Event is not HookEvent.Before)
                return;

            var args = new OnSendDataArgs((PacketType)e.MsgType, e.RemoteClient, e.IgnoreClient, e.Text, e.Number, e.Number2, e.Number3, e.Number4, e.Number5, e.Number6, e.Number7);

            var result = _caller.OnSendDataAsync(args)
                .GetAwaiter().GetResult();

            if (result.Handled)
                e.Result = HookResult.Cancel;

            e.MsgType = (int)args.PacketType;
            e.RemoteClient = args.RemoteClient;
            e.IgnoreClient = args.IgnoreClient;

            e.Text = args.Text;
            e.Number = args.Number;
            e.Number2 = args.Number2;
            e.Number3 = args.Number3;
            e.Number4 = args.Number4;
            e.Number5 = args.Number5;
            e.Number6 = args.Number6;
            e.Number7 = args.Number7;
        }

        private static void OnSendNetData(On.Terraria.Net.NetManager.orig_SendData orig, NetManager netmanager, Terraria.Net.Sockets.ISocket socket, NetPacket packet)
        {
            var args = new OnSendNetDataArgs(netmanager, socket, packet);

            var result = _caller.OnSendNetDataAsync(args)
                .GetAwaiter().GetResult();

            if (result.Handled)
                return;

            orig(netmanager, socket, packet);
        }

        private static void OnReceiveData(object? sender, Hooks.MessageBuffer.GetDataEventArgs e)
        {
            var msgId = e.PacketId;
            var readOffset = e.ReadOffset;
            var length = e.Length;

            var args = new OnReceiveDataArgs();

            var result = _caller.OnReceiveDataAsync(args)
                .GetAwaiter().GetResult();

            if (result.Handled)
                e.Result = HookResult.Cancel;

            e.PacketId = msgId;
            e.ReadOffset = readOffset;
            e.Length = length;
        }

        private static void OnGreetPlayer(On.Terraria.NetMessage.orig_greetPlayer orig, int plr)
        {
            var args = new OnGreetPlayerArgs(plr);

            var result = _caller.OnGreetPlayerAsync(args)
                .GetAwaiter().GetResult();

            if (result.Handled)
                return;

            orig(plr);
        }

        private static void OnSendBytes(object? sender, Hooks.NetMessage.SendBytesEventArgs e)
        {
            var args = new OnSendBytesArgs(Netplay.Clients[e.RemoteClient], e.Data, e.Offset, e.Size);

            var result = _caller.OnSendBytesAsync(args)
                .GetAwaiter().GetResult();

            if (result.Handled)
                e.Result = HookResult.Cancel;
        }

        private static void OnNameCollision(object? sender, Hooks.MessageBuffer.NameCollisionEventArgs e)
        {
            var args = new OnNameCollisionArgs();

            var result = _caller.OnNameCollisionAsync(args)
                .GetAwaiter().GetResult();

            if (result.Handled)
                e.Result = HookResult.Cancel;
        }

        private static void OnConnectionAccepted(On.Terraria.Netplay.orig_OnConnectionAccepted orig, Terraria.Net.Sockets.ISocket client)
        {
            int slot = FindNextOpenClientSlot();
            if (slot is not -1)
            {
                Netplay.Clients[slot].Reset();
                Netplay.Clients[slot].Socket = client;
            }
            if (FindNextOpenClientSlot() is -1)
                Netplay.StopListening();
        }

        private static int FindNextOpenClientSlot()
        {
            lock (_lock)
            {
                for (int i = 0; i < Main.maxNetPlayers; i++)
                    if (!Netplay.Clients[i].IsConnected())
                        return i;
            }
            return -1;
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

            var result = _caller.OnServerCommandAsync(args)
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
                    var args = new OnLeavePlayerArgs(client.Id);
                    _ = _caller.OnLeavePlayerAsync(args);
                }

                var socketargs = new OnSocketResetArgs(client);

                _ = _caller.OnSocketResetAsync(socketargs);
            }

            orig(client);
        }
    }
}
