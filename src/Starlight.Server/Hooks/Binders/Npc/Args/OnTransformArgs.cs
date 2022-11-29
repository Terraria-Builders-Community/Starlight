namespace Starlight
{
    public sealed class OnTransformArgs
    {
        public int NpcId { get; set; }

        public OnTransformArgs(int npcId)
        {
            NpcId = npcId;
        }
    }
}
