using System.ComponentModel.DataAnnotations;

namespace WebApp.Domain;

public class Participant : BaseEntity
{
    [Display(Name = "Osalejaid")]
    public int ParticipantCount { get; set; }

    [Display(Name = "Makseviis")]
    public int PaymentOptionId { get; set; }
    public PaymentOption? PaymentOption { get; set; }

    public int EventId { get; set; }

    public Event? Event { get; set; }
}