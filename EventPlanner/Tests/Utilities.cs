using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebApp.DAL;
using WebApp.Domain;

namespace Tests;

public static class Utilities
{
    public static DbContextOptions<AppDbContext> TestDbContextOptions()
    {
        // Create a new service provider to create a new in-memory database.
        var serviceProvider = new ServiceCollection()
            .AddEntityFrameworkInMemoryDatabase()
            .BuildServiceProvider();

        // Create a new options instance using an in-memory database and 
        // IServiceProvider that the context should resolve all of its 
        // services from.
        var builder = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .UseInternalServiceProvider(serviceProvider);

        return builder.Options;
    }

    public static void AddSeedDataToDatabase(AppDbContext db)
    {
        
        db.Events.AddRange(new List<Event>
        {
            new Event
            {
                Name = "Toidu TESTIMISE mess",
                Description = "See üritus on loodud, et testida toitu ja ka ürituse lisamist anbmebaasi.",
                HappeningAt = DateTime.UtcNow.AddMonths(1),
                Location = "Tammsaare park.",
                Id = 1
            },
            new Event
            {
                Name = "Tootmise testimise järelpidu",
                Description = "Peale pikka testimist tuleb veidi pidutseda ka.",
                HappeningAt = DateTime.UtcNow.AddDays(4),
                Location = "Klubi Pubi.",
                Id = 2
            }
        });
        db.PaymentOptions.AddRange(new List<PaymentOption>
        {
            new PaymentOption
            {
                Name = "Sularaha",
                Description = "Makse sularahaga",
                Id = 1
            },
            new PaymentOption
            {
                Name = "Pangaülekanne",
                Description = "Makse pangaülekandega",
                Id = 2
            }
        });
        db.Persons.AddRange(new List<Person>
        {
            new Person()
            {
                FirstName = "Martin",
                LastName = "Maasikas",
                IdCode = "50211244205",
                ParticipantCount = 1,
                AdditionalInfo = "Mulle meeldivad vaarikad ja muud marjad.",
                EventId = 1
            },
            new Person()
            {
                FirstName = "Mari",
                LastName = "Vaarikas",
                IdCode = "60211244205",
                ParticipantCount = 1,
                AdditionalInfo = "Mulle meeldivad maasikad ja muud marjad.",
                EventId = 1
            }
        });
        
        db.Businesses.AddRange(new List<Business>
        {
            new Business
            {
                BusinessName = "Puud koju OÜ",
                RegisterCode = "19472819",
                ParticipantCount = 31,
                AdditionalInfo = "Meie firmast tuleb kaks inimest, kes soovivad taimset toitu.",
                EventId = 1
            },
            new Business
            {
                BusinessName = "Toidud teele AS",
                RegisterCode = "19472819",
                ParticipantCount = 4,
                AdditionalInfo = "Meie toome omalt poolt ka toitu.",
                EventId = 2
            },
            new Business
            {
                BusinessName = "Timmu Söödik FIE",
                RegisterCode = "19472819",
                ParticipantCount = 1,
                AdditionalInfo = "Minu nimi on Timmu Söödik ja ma olen võistlussööja.",
                EventId = 1
            }
        });

        db.SaveChanges();
    }
}