using System.ComponentModel.DataAnnotations;

namespace WebApp.Domain;

public class Event : BaseEntity
{
    [MaxLength(256)]
    public string Name { get; set; } = default!;
    
    [MaxLength(256)]
    public string Location { get; set; } = default!;
    
    public DateTime HappeningAt { get; set; } = default!;
    
    [MaxLength(1000)]
    public string Description { get; set; } = default!;

    public ICollection<Participant>? Participants { get; set; } 
}