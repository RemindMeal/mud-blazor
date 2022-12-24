namespace RemindMealData.Model;

public record class Dish
{
    public Guid MealId { get; set; }
    public Guid RecipeId { get; set; }

    // Relationships
    public Meal? Meal { get; set; }
    public Recipe? Recipe { get; set; }
}
