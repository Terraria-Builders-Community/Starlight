using Starlight.Entities;
using Starlight.Entities.Bans.Entity;
using Starlight.Entities.Bans.Object;

namespace Starlight
{
    public class Starlight
    {
        public async Task EntryAsync()
        {
            var wallban = await IModel.GetAsync<WallBan>(x => x.Id == 1);

            await wallban.DeleteAsync();

            // want to change the length of a ban?
            var ban = await IModel.GetAsync<UserBan>(x => x.Name == "rozen");

            if (ban is null)
                return;
        }
    }
}