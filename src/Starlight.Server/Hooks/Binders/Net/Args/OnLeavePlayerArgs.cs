namespace Starlight
{
    public sealed class OnLeavePlayerArgs : PlayerEvent
    {
        public override int PlayerIndex { get; set; }

        public OnLeavePlayerArgs(int playerIndex)
        {
            PlayerIndex = playerIndex;
        }
    }
}
