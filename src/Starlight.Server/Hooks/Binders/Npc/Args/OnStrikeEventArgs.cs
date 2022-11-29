using Terraria;

namespace Starlight
{
    public sealed class OnStrikeArgs
    {
        public Player Player { get; internal set; }

        public NPC NPC { get; internal set; }

        public int Damage { get; internal set; }

        public float Knockback { get; internal set; }

        public int HitDirection { get; internal set; }

        public bool Critical { get; internal set; }

        public bool NoEffect { get; internal set; }

        public bool FromNet { get; internal set; }

        public OnStrikeArgs(Player player, NPC nPC, int damage, float knockback, int hitDirection, bool critical, bool noEffect, bool fromNet)
        {
            Player = player;
            NPC = nPC;
            Damage = damage;
            Knockback = knockback;
            HitDirection = hitDirection;
            Critical = critical;
            NoEffect = noEffect;
            FromNet = fromNet;
        }
    }
}
