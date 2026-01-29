namespace Library.Models;

using System.ComponentModel.DataAnnotations;

public class Checkout
{
    public int Id { get; set; }
    [Required]
    public int MaterialId { get; set; }
    [Required]
    public int PatronId { get; set; }
    [Required]
    public DateTime CheckoutDate { get; set; }
    public DateTime ReturnDate { get; set; }

    public Patron Patron { get; set; }
    public Material Material { get; set; }
}