using Terraria.Localization;

namespace Starlight
{
    public sealed class OnSendDataArgs
    {
        public PacketType PacketType { get; set; }

        public int RemoteClient { get; set; }

        public int IgnoreClient { get; set; }

        public NetworkText Text { get; set; }

        public int Number { get; set; }

        public float Number2 { get; set; }

        public float Number3 { get; set; }

        public float Number4 { get; set; }

        public int Number5 { get; set; }

        public int Number6 { get; set; }

        public int Number7 { get; set; }

        public OnSendDataArgs(PacketType packetType, int remoteClient, int ignoreClient, NetworkText text, int number, float number2, float number3, float number4, int number5, int number6, int number7)
        {
            PacketType = packetType;
            RemoteClient = remoteClient;
            IgnoreClient = ignoreClient;
            Text = text;
            Number = number;
            Number2 = number2;
            Number3 = number3;
            Number4 = number4;
            Number5 = number5;
            Number6 = number6;
            Number7 = number7;
        }
    }
}
