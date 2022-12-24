namespace RemindMealData.Model;

public sealed class Presence
{
    public Guid FriendId { get; set; }
    public Guid MealId { get; set; }

    // Relationships
    public Friend? Friend { get; set; }
    public Meal? Meal { get; set; }

    public override string ToString() => $"{Friend} at {Meal?.Date}";
}
