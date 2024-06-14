using System.ComponentModel.DataAnnotations;

namespace kolos2.Models;

public class Characters
{
    [Key]
    public int Id { get; set; }
    [Required]
    [MaxLength(50)]
    public string FirstName { get; set; }
    [Required]
    [MaxLength(120)]
    public string LastName { get; set; }
    [Required]
    public int CurrentWeight { get; set; }
    [Required]
    public int MaxWeight { get; set; }
    
    public ICollection<Backpacks> Backpacks { get; set; }
    public ICollection<Character_titles> Titles { get; set; }
}