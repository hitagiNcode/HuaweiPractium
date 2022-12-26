using System.ComponentModel.DataAnnotations;

namespace CertiEx.Domain.User;

public class ApplicationUser
{
    [Display(Name = "First Name")]
    [StringLength(30)]
    public string? FirstName { get; set; }

    [Display(Name = "Last Name")]
    [StringLength(30)]
    public string? LastName { get; set; }

    [Display(Name = "Registered Date")]
    public DateTime RegisterDate { get; set; } = DateTime.UtcNow;
}