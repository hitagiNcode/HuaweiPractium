using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace CertiEx.Domain.User;

public class ApplicationUser : IdentityUser
{
    [Display(Name = "First Name")]
    [StringLength(30)]
    public string? FirstName { get; set; }

    [Display(Name = "Last Name")]
    [StringLength(30)]
    public string? LastName { get; set; }

    [Display(Name = "Registered Date")]
    public DateTime RegisterDate { get; set; } = DateTime.UtcNow;
    
    [MaxLength]
    public string ImgFile { get; set; }  
}