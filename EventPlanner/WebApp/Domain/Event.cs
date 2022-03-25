using System.ComponentModel.DataAnnotations;

namespace WebApp.Domain;

public class Event : BaseEntity
{
    [MaxLength(32)]
    [Display(Name = "Ürituse nimi")]
    public string Name { get; set; } = default!;
    
    [MaxLength(64)]
    [Display(Name = "Asukoht")]
    public string Location { get; set; } = default!;
    
    [Display(Name = "Toimumisaeg")]
    public DateTime HappeningAt { get; set; } = default!;
    
    [MaxLength(1000)]
    [Display(Name = "Lisainfo")]
    public string Description { get; set; } = default!;

    public ICollection<Business>? Businesses { get; set; } 
    public ICollection<Person>? Persons { get; set; } 
}