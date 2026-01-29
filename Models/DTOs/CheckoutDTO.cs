namespace Library.Models.DTO;

using System.ComponentModel.DataAnnotations;

public class CheckoutDTO
{
    public int Id { get; set; }
    [Required]
    public int PatronId { get; set; }
    [Required]
    public DateTime CheckoutDate { get; set; }
    public DateTime ReturnDate { get; set; }
    public PatronDTO? Patron { get; set; }
    public MaterialDTO? Material { get; set; }


    public int? DaysCheckedOut
    {
        get
        {
            return ReturnDate > new DateTime() ? null : (DateTime.Now - CheckoutDate).Days;
        }
    }
}