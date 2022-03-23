using System.ComponentModel.DataAnnotations;

namespace WebApp.Domain;

public class Event : BaseEntity
{
    [MaxLength(256)]
    [Display(Name = "Ürituse nimi")]
    public string Name { get; set; } = default!;
    
    [MaxLength(256)]
    [Display(Name = "Asukoht")]
    public string Location { get; set; } = default!;
    
    [Display(Name = "Toimumisaeg")]
    public DateTime HappeningAt { get; set; } = default!;
    
    [MaxLength(1000)]
    [Display(Name = "Lisainfo")]
    public string Description { get; set; } = default!;

    public ICollection<Participant>? Participants { get; set; } 
}