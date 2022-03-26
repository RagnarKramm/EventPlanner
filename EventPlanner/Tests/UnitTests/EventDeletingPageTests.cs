using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using WebApp.DAL;
using WebApp.Domain;
using WebApp.Pages.Events;
using Xunit;

namespace Tests.UnitTests;

public class EventDeletingPageTests
{
    [Fact]
    public async Task OnPostDeleteEventAsync_ReturnsARedirectToPageResult()
    {
        // Arrange
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase("InMemoryDb");
        var mockAppDbContext = new Mock<AppDbContext>(optionsBuilder.Options);
        var pageModel = new DeleteModel(mockAppDbContext.Object);
        var recId = 1;

        // Act
        var result = await pageModel.OnPostAsync(recId);

        // Assert
        Assert.IsType<RedirectToPageResult>(result);
        var redirectToPageResult = result as RedirectToPageResult;
        Assert.NotNull(redirectToPageResult!.PageName);
        Assert.Equal("/Index", redirectToPageResult.PageName);
    }

    [Fact]
    public async Task OnPostDeleteEventAsync_ReturnsARedirectToPageResult_NoParticipantsRemaining()
    {
        // Arrange
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase("InMemoryDb");
        var appDbContext = new AppDbContext(optionsBuilder.Options);
        var testEvent = new Event
        {
            Name = "Tootmise testimise järelpidu",
            Description = "Peale pikka testimist tuleb veidi pidutseda ka.",
            HappeningAt = DateTime.UtcNow.AddDays(4),
            Location = "Klubi Pubi.",
            Id = 2
        };
        Utilities.AddSeedDataToDatabase(appDbContext);

        var pageModel = new DeleteModel(appDbContext);
        // Act
        pageModel.Event = testEvent;
        var result = await pageModel.OnPostAsync(testEvent.Id);
        

        // Assert
        Assert.IsType<RedirectToPageResult>(result);
        Assert.Equal(1, appDbContext.Events.Count());
        
        // There was one business with EventId = 2 from 3 total businesses, after deletion there is 2 remaining.
        Assert.Equal(2, appDbContext.Businesses.Count());
        Assert.Equal(2, appDbContext.Persons.Count());
        
        // No participants with EventId = 2 exist
        Assert.Equal(0, appDbContext.Businesses.Count(b => b.EventId == 2));
        Assert.Equal(0, appDbContext.Persons.Count(p => p.EventId == 2));
    }
}