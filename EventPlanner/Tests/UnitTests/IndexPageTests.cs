using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Moq;
using WebApp.DAL;
using WebApp.Domain;
using WebApp.Pages;
using Xunit;

namespace Tests.UnitTests;

public class IndexPageTests
{
    
    [Fact]
    public async Task AddingEventAndParticipantsToDb_ParticipantCountForEventCorrect()
    {
        await using var db = new AppDbContext(Utilities.TestDbContextOptions());

        // Assign
        Utilities.AddSeedDataToDatabase(db);

        // Act
        var testEvent = await db.Events.FirstAsync(item => item.Id == 1);
        var participants = IndexModel.GetParticipantCount(testEvent);
        
        //Assert
        Assert.Equal(34, participants);
    }

    [Fact]
    public async Task IsTimeInFutureTest()
    {
        // Assign
        var futureTime = DateTime.Now.AddHours(21);
        var pastTime = DateTime.MinValue;
        
        // Act
        var isInFuture = IndexModel.IsInFuture(futureTime);
        var isNotInFuture = IndexModel.IsInFuture(pastTime);

        // Assert
        Assert.True(isInFuture);
        Assert.False(isNotInFuture);
    }
    
    [Fact]
    public async Task OnGetAsync_PopulatesThePageModel_WithAListOfEvents()
    {
        // Arrange
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase("InMemoryDb");
        
        var mockAppDbContext = new Mock<AppDbContext>(optionsBuilder.Options);
        var expectedEvents = new List<Event>
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
        };
        
        mockAppDbContext.Setup(
            db => db.GetEventsAndParticipantsAsync()).Returns(Task.FromResult(expectedEvents));
        
        var pageModel = new IndexModel(mockAppDbContext.Object);
        
        // Act
        await pageModel.OnGetAsync();
        
        // Assert
        var actualEvents = Assert.IsAssignableFrom<List<Event>>(pageModel.Event);
        Assert.Equal(
            expectedEvents.OrderBy(e => e.Id).Select(e => e.Name), 
            actualEvents.OrderBy(e => e.Id).Select(e => e.Name));
    }
}