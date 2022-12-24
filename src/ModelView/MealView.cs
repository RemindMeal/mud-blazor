using System.ComponentModel.DataAnnotations;

namespace RemindMeal.ModelView;

public record class MealView
{
    [Required]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
    public DateTime? Date { get; set; }

    public ICollection<FriendView> Guests { get; } = new List<FriendView>();
    public ICollection<RecipeView> Recipes { get; } = new List<RecipeView>();

    public override string ToString() => $"Meal view of {Date}";
}
