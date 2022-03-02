using System.ComponentModel.DataAnnotations;

namespace MudBlazorTest.Model
{
    public sealed class Category
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Le groupe doit avoir un nom")]
        public string Name { get; set; }

        public ICollection<Recipe> Recipes { get; set; } = new List<Recipe>();

        public override string ToString() => Name;
    }
}