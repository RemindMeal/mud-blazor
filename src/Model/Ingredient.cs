namespace RemindMeal.Model;

public sealed class Ingredient : IModel
{
    public int Id { get; set; }

    public string Name { get; set; }

    public ICollection<Recipe> Recipes { get; set; }

    public override string ToString()
    {
        return Name;
    }
}
