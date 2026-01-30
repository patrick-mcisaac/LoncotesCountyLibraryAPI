namespace Library.Models.DTO;

using System.ComponentModel.DataAnnotations;

public class CheckoutWithLateFeesDTO
{
    public int Id { get; set; }
    [Required]
    public int PatronId { get; set; }
    [Required]
    public DateTime CheckoutDate { get; set; }
    public DateTime ReturnDate { get; set; }
    public PatronDTO Patron { get; set; }
    public MaterialDTO Material { get; set; }


    public int? DaysCheckedOut
    {
        get
        {
            return ReturnDate > new DateTime() ? null : (DateTime.Now - CheckoutDate).Days;
        }
    }

    private static decimal _lateFeePerDay = .50m;

    public decimal? LateFee
    {
        get
        {
            DateTime dueDate = CheckoutDate.AddDays(Material.MaterialType.CheckoutDays);
            DateTime returnDate = ReturnDate > new DateTime(0001, 01, 01, 0, 0, 0) ? ReturnDate : DateTime.Now;
            int daysLate = (returnDate - dueDate).Days;

            decimal fee = daysLate * _lateFeePerDay;

            return daysLate > 0 ? fee : null;

        }
    }

}