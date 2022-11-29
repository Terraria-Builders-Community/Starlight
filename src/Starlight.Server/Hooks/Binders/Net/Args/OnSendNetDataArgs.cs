using Terraria.Net;
using Terraria.Net.Sockets;

namespace Starlight
{
    public sealed class OnSendNetDataArgs
    {
        public NetManager NetManager { get; set; }

        public ISocket Socket { get; set; }

        public NetPacket Packet { get; set; }

        public OnSendNetDataArgs(NetManager netManager, ISocket socket, NetPacket packet)
        {
            NetManager = netManager;
            Socket = socket;
            Packet = packet;
        }
    }
}
