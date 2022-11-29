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

            var msgType = e.MsgType;
            var remoteClient = e.RemoteClient;
            var ignoreClient = e.IgnoreClient;
            var text = e.Text;
            var number = e.Number;
            var number2 = e.Number2;
            var number3 = e.Number3;
            var number4 = e.Number4;
            var number5 = e.Number5;
            var number6 = e.Number6;
            var number7 = e.Number7;

            var args = new OnSendDataArgs();

            var result = _caller.OnSendDataAsync(args)
                .GetAwaiter().GetResult();

            if (result.Handled)
                e.Result = HookResult.Cancel;

            e.MsgType = msgType;
            e.RemoteClient = remoteClient;
            e.IgnoreClient = ignoreClient;
            e.Text = text;
            e.Number = number;
            e.Number2 = number2;
            e.Number3 = number3;
            e.Number4 = number4;
            e.Number5 = number5;
            e.Number6 = number6;
            e.Number7 = number7;
        }

        private static void OnSendNetData(On.Terraria.Net.NetManager.orig_SendData orig, NetManager netmanager, Terraria.Net.Sockets.ISocket socket, NetPacket packet)
        {
            var args = new OnSendNetDataArgs();

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
            var args = new OnSendBytesArgs();

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
            if (FindNextOpenClientSlot() == -1)
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
    }
}
