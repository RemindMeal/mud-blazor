using System.ComponentModel.DataAnnotations;

namespace RemindMealData.Model;

public sealed class Recipe : IModel
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public required Category Category { get; set; }

    // Relationships
    public ICollection<Dish> Dishes { get; set; } = new List<Dish>();

    public override string ToString() => Name;
}
