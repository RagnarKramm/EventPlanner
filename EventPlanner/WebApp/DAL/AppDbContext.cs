using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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
    
    public virtual async Task<List<Event>> GetEventsAndParticipantsAsync()
    {
        return await Events
            .Include(e => e.Businesses)
            .Include(e => e.Persons)
            .OrderBy(item => item.HappeningAt)
            .AsNoTracking()
            .ToListAsync();
    }

    public virtual async Task<Event?> GetEventById(int? id)
    {
        return await Events.FirstOrDefaultAsync(e => e.Id == id);
    }
    
    public virtual async Task AddEventAsync(Event e)
    {
        await Events.AddAsync(e);
        await SaveChangesAsync();
    }
    
    public virtual async Task EditEventAsync(Event e)
    {
        Attach(e).State = EntityState.Modified;
        await SaveChangesAsync();
    }
    
    public virtual async Task DeleteEventAsync(int id)
    {
        var e = await Events.FindAsync(id);

        if (e != null)
        {
            Events.Remove(e);
            await SaveChangesAsync();
        }
    }
    
    public virtual async Task<List<Business>> GetBusinessAsync()
    {
        return await Businesses
            .ToListAsync();
    }
    
    public virtual async Task<List<Business>> GetBusinessesForEventAsync(int? id)
    {
        return await Businesses.Where(b => b.EventId == id)
            .AsNoTracking()
            .ToListAsync();
    }
    
    public virtual async Task AddBusinessAsync(Business business)
    {
        await Businesses.AddAsync(business);
        await SaveChangesAsync();
    }
    
    public virtual async Task EditBusinessAsync(Business business)
    {
        Attach(business).State = EntityState.Modified;
        await SaveChangesAsync();
    }

    public virtual async Task DeleteBusinessAsync(int id)
    {
        var business = await Businesses.FindAsync(id);

        if (business != null)
        {
            Businesses.Remove(business);
            await SaveChangesAsync();
        }
    }
    
    public virtual async Task<List<Person>> GetPersonsAsync()
    {
        return await Persons
            .ToListAsync();
    }

    public virtual async Task<List<Person>> GetPersonsForEventAsync(int? id)
    {
        return await Persons.Where(p => p.EventId == id)
            .AsNoTracking()
            .ToListAsync();
    }

    public virtual async Task AddPersonAsync(Person person)
    {
        await Persons.AddAsync(person);
        await SaveChangesAsync();
    }

    public virtual async Task EditPersonAsync(Person person)
    {
        Attach(person).State = EntityState.Modified;
        await SaveChangesAsync();
    }

    public virtual async Task DeletePersonAsync(int id)
    {
        var person = await Persons.FindAsync(id);

        if (person != null)
        {
            Persons.Remove(person);
            await SaveChangesAsync();
        }
    }
    
}