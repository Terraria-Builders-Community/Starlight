using Microsoft.Xna.Framework;
using Terraria.Localization;
namespace Starlight
{
    public sealed class OnBroadcastArgs
    {
        public NetworkText Message { get; set; }

        public Color Color { get; set; }

        public OnBroadcastArgs(NetworkText message, Color color)
        {
            Message = message;
            Color = color;
        }
    }
}
