using RemindMeal.Data;
using RemindMeal.Model;

namespace RemindMeal.Services;

public sealed class FriendRepository : AsyncRepository<Friend, (string, string)>
{
    public FriendRepository(RemindMealDbContext context) : base(context, context.Friends)
    { }

    public async override Task<Friend> UpdateAsync(int id, Friend newFriend)
    {
        var friend = await _dbSet.FindAsync(id);
        if (friend == null)
            return null;

        friend.Name = newFriend.Name;
        friend.Surname = newFriend.Surname;

        _dbSet.Update(friend);
        await _context.SaveChangesAsync();

        return friend;
    }

    public override async Task<Friend> DeleteAsync(Friend model)
    {
        return await DeleteAsync(model.Id);
    }

    protected override (string, string) OrderKeySelector(Friend t) => (t.Surname, t.Name);
}
