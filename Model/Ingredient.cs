namespace MudBlazorTest.Model
{
    public sealed class Ingredient
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Recipe> Recipes { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}