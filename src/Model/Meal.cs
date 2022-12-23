using System.ComponentModel.DataAnnotations;

namespace RemindMeal.Model
{
    public sealed class Meal : IModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DataType(DataType.Date)]
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
}