using System.ComponentModel.DataAnnotations;

namespace WebApp.Domain;

public class PaymentOption : BaseEntity
{
    [MaxLength(128)]
    public string Name { get; set; } = default!;

    [MaxLength(1024)]
    public string Description { get; set; } = default!;
    
    public ICollection<Participant>? Participants { get; set; }
}