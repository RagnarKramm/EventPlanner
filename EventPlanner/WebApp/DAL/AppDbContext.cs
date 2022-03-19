using Microsoft.EntityFrameworkCore;
using WebApp.Domain;

namespace WebApp.DAL;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Event> Events { get; set; } = default!;

    public DbSet<Participant> Participants { get; set; } = default!;

    public DbSet<ParticipantType> ParticipantTypes { get; set; } = default!;

    public DbSet<PaymentOption> PaymentOptions { get; set; } = default!;
}