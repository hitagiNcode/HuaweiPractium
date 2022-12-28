using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CertiEx.Domain;

public class BaseEntity
{
    public DateTime? CreatedOn { get; set; } = DateTime.UtcNow;

    public DateTime? ModifiedOn { get; set; } = DateTime.UtcNow;

    public string CreatedBy { get; set; } = "System";

    public string ModifiedBy { get; set; } = "System";

    public bool IsDeleted { get; set; } = false;
}