namespace RemindMealData.Model;

public record Category : IModel
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public ICollection<Recipe> Recipes { get; set; } = new List<Recipe>();
}
