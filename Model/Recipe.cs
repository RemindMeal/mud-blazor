namespace MudBlazorTest.Model
{
    public sealed class Recipe
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public Category Category { get; set; }

        // Relationships

        public ICollection<Dish> Dishes { get; set; } = new List<Dish>();
        public ICollection<Ingredient> Ingredients { get; set; } = new List<Ingredient>();

        public override string ToString()
        {
            return $"Model(Name = {Name}, Description = {Description}, Category = {Category}";
        }
    }
}
