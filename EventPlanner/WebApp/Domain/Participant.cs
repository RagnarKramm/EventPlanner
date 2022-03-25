using System.ComponentModel.DataAnnotations;

namespace WebApp.Domain;

public class Participant : BaseEntity
{
    [Display(Name = "Osalejaid")]
    public int ParticipantCount { get; set; }

    public int PaymentOptionId { get; set; }
    public PaymentOption PaymentOption { get; set; } = default!;

    public int EventId { get; set; }

    public Event Event { get; set; } = default!;
}