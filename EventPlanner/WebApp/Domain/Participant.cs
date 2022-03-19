using System.ComponentModel.DataAnnotations;

namespace WebApp.Domain;

public class Participant : BaseEntity
{
    [MaxLength(128)]
    public string ParticipantLine1 { get; set; } = default!;
    
    [MaxLength(128)]
    public string ParticipantLine2 { get; set; } = default!;
    
    [MaxLength(128)]
    public string ParticipantLine3 { get; set; } = default!;
    
    [MaxLength(5000)]
    public string AdditionalInformation { get; set; } = default!;

    public int EventId { get; set; }
    public Event Event { get; set; } = default!;

    public int ParticipantTypeId { get; set; }
    public ParticipantType ParticipantType { get; set; } = default!;

    public int PaymentOptionId { get; set; }
    public PaymentOption PaymentOption { get; set; } = default!;

}