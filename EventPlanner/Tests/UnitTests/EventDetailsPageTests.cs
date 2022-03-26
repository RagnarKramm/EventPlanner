using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Moq;
using WebApp.DAL;
using WebApp.Domain;
using WebApp.Pages.Events;
using Xunit;

namespace Tests.UnitTests;

public class EventDetailsPageTests
{
    [Fact]
    public async Task OnGetAsync_PopulatesThePageModel_WithAListOfBusinesses()
    {
        // Arrange
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase("InMemoryDb");
        
        var mockAppDbContext = new Mock<AppDbContext>(optionsBuilder.Options);
        var testEvent = new Event
        {
            Name = "Toidu TESTIMISE mess",
            Description = "See üritus on loodud, et testida toitu ja ka ürituse lisamist anbmebaasi.",
            HappeningAt = DateTime.UtcNow.AddMonths(1),
            Location = "Tammsaare park.",
            Id = 1
        };
        
        var expectedBusinesses = new List<Business>
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
                EventId = 1
            },
            new Business
            {
                BusinessName = "Timmu Söödik FIE",
                RegisterCode = "19472819",
                ParticipantCount = 1,
                AdditionalInfo = "Minu nimi on Timmu Söödik ja ma olen võistlussööja.",
                EventId = 1
            }
        };
        
        mockAppDbContext.Setup(
            db => db.GetEventById(testEvent.Id)).Returns(Task.FromResult(testEvent));
        mockAppDbContext.Setup(db => db.GetBusinessesForEventAsync(testEvent.Id)).Returns(Task.FromResult(expectedBusinesses));
        
        var pageModel = new DetailsModel(mockAppDbContext.Object);
        
        // Act
        await pageModel.OnGetAsync(testEvent.Id);
        
        // Assert
        var actualBusinesses = Assert.IsAssignableFrom<List<Business>>(pageModel.Businesses);
        Assert.Equal(
            expectedBusinesses.OrderBy(b => b.Id).Select(b => b.BusinessName), 
            actualBusinesses.OrderBy(b => b.Id).Select(b => b.BusinessName));
        Assert.Equal(
            expectedBusinesses.OrderBy(b => b.Id).Select(b => b.RegisterCode), 
            actualBusinesses.OrderBy(b => b.Id).Select(b => b.RegisterCode));
    }
    
    [Fact]
    public async Task OnGetAsync_PopulatesThePageModel_WithAListOfPersons()
    {
        
        // Arrange
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase("InMemoryDb");
        
        var mockAppDbContext = new Mock<AppDbContext>(optionsBuilder.Options);
        var testEvent =  new Event
            {
                Name = "Toidu TESTIMISE mess",
                Description = "See üritus on loodud, et testida toitu ja ka ürituse lisamist anbmebaasi.",
                HappeningAt = DateTime.UtcNow.AddMonths(1),
                Location = "Tammsaare park.",
                Id = 1
            };

        var expectedPersons = new List<Person>
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
        };
        
        mockAppDbContext.Setup(
            db => db.GetEventById(testEvent.Id)).Returns(Task.FromResult(testEvent));
        mockAppDbContext.Setup(db => db.GetPersonsForEventAsync(testEvent.Id)).Returns(Task.FromResult(expectedPersons));
        
        var pageModel = new DetailsModel(mockAppDbContext.Object);
        
        // Act
        await pageModel.OnGetAsync(testEvent.Id);
        
        // Assert
        var actualPersons = Assert.IsAssignableFrom<List<Person>>(pageModel.Persons);
        Assert.Equal(
            expectedPersons.OrderBy(p => p.Id).Select(p => p.FirstName + p.LastName), 
            actualPersons.OrderBy(p => p.Id).Select(p => p.FirstName + p.LastName));
        Assert.Equal(
            expectedPersons.OrderBy(p => p.Id).Select(p => p.IdCode), 
            actualPersons.OrderBy(p => p.Id).Select(p => p.IdCode));
    }
}