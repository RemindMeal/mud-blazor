using System.Data;
using System.ComponentModel.DataAnnotations;

namespace RemindMeal.Model
{
    public sealed class Category : IModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Le groupe doit avoir un nom")]
        public string Name { get; set; }

        public ICollection<Recipe> Recipes { get; set; } = new List<Recipe>();

        public override string ToString() => Name;
    }
}