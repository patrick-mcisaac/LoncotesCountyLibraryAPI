namespace Library.Models;

using System.ComponentModel.DataAnnotations;

public class Material
{
    public int Id { get; set; }
    [Required]
    public string? MaterialName { get; set; }
    [Required]
    public int MaterialTypeId { get; set; }
    [Required]
    public int GenreId { get; set; }
    public DateTime OutOfCirculationSince { get; set; }
    public Genre Genre { get; set; }
    public MaterialType MaterialType { get; set; }

    public ICollection<Checkout> Checkouts { get; set; }
}