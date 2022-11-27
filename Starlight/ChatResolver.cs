namespace Starlight
{
    public class ChatResolver : HookResolver
    {
        public ChatResolver()
        {
            Order = 1;
        }

        public override Task<HandleResult> OnChatAsync(OnChatArgs args)
        {
            return base.OnChatAsync(args);
        }
    }
}
