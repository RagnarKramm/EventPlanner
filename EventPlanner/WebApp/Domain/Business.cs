using System.ComponentModel.DataAnnotations;

namespace WebApp.Domain;

public class Business : Participant
{
    [MaxLength(32)]
    [Display(Name = "Juriidiline nimi")]
    public string BusinessName { get; set; } = default!;
    
    [MaxLength(16)]
    [Display(Name = "Registrikood")]
    public string RegisterCode { get; set; } = default!;

    [MaxLength(5000)]
    [Display(Name = "Lisainfo")]
    public string? AdditionalInfo { get; set; }
    
}