using System.ComponentModel.DataAnnotations;

namespace kolos2.Models;

public class Items
{
    [Key]
    public int Id { get; set; }
    [Required]
    [MaxLength(100)]
    public string Name { get; set; }
    [Required]
    public int Weight { get; set; }
    
    public ICollection<Backpacks> Backpacks { get; set; }
}