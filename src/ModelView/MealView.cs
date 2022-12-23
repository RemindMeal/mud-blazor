using System.ComponentModel.DataAnnotations;
using RemindMeal.Model;

namespace RemindMeal.ModelView;

public sealed class MealView
{
    [Required]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
    public DateTime? Date { get; set; }

    // Relationships
    public ICollection<Presence> Presences { get; } = new List<Presence>();
    public ICollection<Dish> Dishes { get; } = new List<Dish>();

    public override string ToString()
    {
        return $"Meal of {Date}";
    }
}
