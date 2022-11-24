using Microsoft.Xna.Framework;

namespace Starlight.Structures
{
    public readonly struct Polygon
    {
        public Vector2[] Points { get; }

        public Polygon(params Vector2[] points)
        {
            Points = points;
        }
    }
}
