namespace Starlight
{
    public sealed class OnGreetPlayerArgs
    {
        public int UserId { get; set; }

        public OnGreetPlayerArgs(int userId)
        {
            UserId = userId;
        }
    }
}
