using System.ComponentModel.DataAnnotations;

namespace RemindMealData.Model;

public record Friend : IModel
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Surname { get; set; } = string.Empty;

    public string FullName => $"{Name} {Surname}";

    // Relationships
    public ICollection<Presence> Presences { get; set; } = new List<Presence>();

    public override string ToString() => FullName;
}
