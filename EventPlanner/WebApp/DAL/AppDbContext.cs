using Microsoft.EntityFrameworkCore;
using WebApp.Domain;

namespace WebApp.DAL;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Event> Events { get; set; } = default!;

    public DbSet<Person> Persons { get; set; } = default!;

    public DbSet<Business> Businesses { get; set; } = default!;

    public DbSet<PaymentOption> PaymentOptions { get; set; } = default!;
}