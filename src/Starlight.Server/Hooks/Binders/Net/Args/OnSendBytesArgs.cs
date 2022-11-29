using Terraria;

namespace Starlight
{
    public sealed class OnSendBytesArgs
    {
        public RemoteClient Socket { get; set; }

        public byte[] Buffer { get; set; }
        
        public int Offset { get; set; }

        public int Count { get; set; }

        public OnSendBytesArgs(RemoteClient socket, byte[] buffer, int offset, int count)
        {
            Socket = socket;
            Buffer = buffer;
            Offset = offset;
            Count = count;
        }
    }
}
