using System.ComponentModel.DataAnnotations;

namespace WebApp.Domain;

public class Person : Participant
{
    [MaxLength(32)]
    [Display(Name = "Eesnimi")]
    public string FirstName { get; set; } = default!;

    [MaxLength(32)]
    [Display(Name = "Perekonnanimi")]
    public string LastName { get; set; } = default!;

    [MaxLength(16)]
    [Display(Name = "Isikukood")]
    public string IdCode { get; set; } = default!;
    
    [MaxLength(1500)]
    [Display(Name = "Lisainfo")]
    public string? AdditionalInfo { get; set; }
    
}