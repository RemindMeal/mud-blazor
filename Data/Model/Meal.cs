using System.ComponentModel.DataAnnotations;

namespace RemindMealData.Model;

public sealed class Meal : IModel
{
    public Guid Id { get; set; }
    
    [DataType(DataType.Date)]
    public DateTime Date { get; set; }

    // Relationships
    public ICollection<Presence> Presences { get; } = new List<Presence>();
    public ICollection<Dish> Dishes { get; } = new List<Dish>();

    public override string ToString() => $"Meal of {Date}";
}
