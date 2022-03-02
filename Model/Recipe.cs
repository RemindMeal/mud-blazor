public sealed class Recipe
{
    public Recipe()
    {}

    public Recipe(string name, string description)
    {
        Name = name;
        Description = description;
    }

    public long Id { get; set; }

    public string Name { get; }
    public string Description { get; }
}