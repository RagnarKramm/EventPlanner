using System.ComponentModel.DataAnnotations;

namespace WebApp.Domain;

public class PaymentOption : BaseEntity
{
    [MaxLength(64)]
    public string Name { get; set; } = default!;

    [MaxLength(512)]
    public string Description { get; set; } = default!;
    
    public ICollection<Business>? Businesses { get; set; } 
    public ICollection<Person>? Persons { get; set; } }