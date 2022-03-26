using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using WebApp.DAL;
using WebApp.Pages.Events;
using Xunit;

namespace Tests.UnitTests;

public class EventDeletingPageTests
{
    
    [Fact]
    public async Task OnPostDeleteMessageAsync_ReturnsARedirectToPageResult()
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
    }

    [Fact]
    public async Task OnPostDeleteMessageAsync_ReturnsARedirectToPageResult_NoParticipantsRemaining()
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
    }
}