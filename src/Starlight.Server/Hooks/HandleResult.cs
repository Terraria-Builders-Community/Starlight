namespace Starlight
{
    public readonly struct HandleResult
    {
        public bool Handled { get; }

        private HandleResult(bool handled)
            => Handled = handled;

        public static HandleResult Continue()
            => new(true);

        public static HandleResult Break()
            => new(false);
    }
}
