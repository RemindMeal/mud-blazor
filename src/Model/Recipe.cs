namespace RemindMeal.Model
{
    public sealed class Recipe : IModel
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
            return Name;
        }
    }
}
