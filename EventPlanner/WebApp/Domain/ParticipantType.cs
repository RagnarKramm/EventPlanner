using System.ComponentModel.DataAnnotations;

namespace WebApp.Domain;

public class ParticipantType : BaseEntity
{
    [MaxLength(128)]
    public string Name { get; set; } = default!;

    [MaxLength(64)]
    public string Line1Header { get; set; } = default!;
    
    [MaxLength(64)]
    public string Line2Header { get; set; } = default!;
    
    [MaxLength(64)]
    public string Line3Header { get; set; } = default!;

    public int DescriptionLimit { get; set; }

    public ICollection<Participant>? Participants { get; set; }
}