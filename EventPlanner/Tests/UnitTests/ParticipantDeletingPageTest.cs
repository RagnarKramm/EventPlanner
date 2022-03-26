using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using WebApp.DAL;
using WebApp.Domain;
using WebApp.Pages.Businesses;
using Xunit;

namespace Tests.UnitTests;

public class ParticipantDeletingPageTest
{
    [Fact]
    public async Task OnPostDeleteBusinessAsync_ReturnsARedirectToPageResult()
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
    
    // [Fact]
    // public async Task OnPostDeleteBusinessAsync_ReturnsARedirectToPageResult_AndDeletesBusiness()
    // {
    //     // Arrange
    //     var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>()
    //         .UseInMemoryDatabase("InMemoryDb");
    //     
    //     var mockAppDbContext = new Mock<AppDbContext>(optionsBuilder.Options);
    //     var expectedBusinesses = new List<Business>
    //     {
    //         new Business
    //         {
    //             BusinessName = "Puud koju OÜ",
    //             RegisterCode = "19472819",
    //             ParticipantCount = 31,
    //             AdditionalInfo = "Meie firmast tuleb kaks inimest, kes soovivad taimset toitu.",
    //             EventId = 1
    //         },
    //         new Business
    //         {
    //             BusinessName = "Toidud teele AS",
    //             RegisterCode = "19472819",
    //             ParticipantCount = 4,
    //             AdditionalInfo = "Meie toome omalt poolt ka toitu.",
    //             EventId = 2
    //         },
    //         new Business
    //         {
    //             BusinessName = "Timmu Söödik FIE",
    //             RegisterCode = "19472819",
    //             ParticipantCount = 1,
    //             AdditionalInfo = "Minu nimi on Timmu Söödik ja ma olen võistlussööja.",
    //             EventId = 1
    //         }
    //     };
    //     
    //
    //     mockAppDbContext.Setup(db => db.GetBusinessesAsync()).Returns(Task.FromResult(expectedBusinesses));
    //     
    //     var pageModel = new DeleteModel(mockAppDbContext.Object);
    //     
    //     var recId = 1;
    //
    //     // Act
    //     var result = await pageModel.OnPostAsync(recId);
    //
    //     // Assert
    //     Assert.IsType<RedirectToPageResult>(result);
    //     
    // }

    
    [Fact]
    public async Task OnPostDeletePersonAsync_ReturnsARedirectToPageResult()
    {
        // Arrange
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase("InMemoryDb");
        var mockAppDbContext = new Mock<AppDbContext>(optionsBuilder.Options);
        var pageModel = new WebApp.Pages.Persons.DeleteModel(mockAppDbContext.Object);
        var recId = 1;

        // Act
        var result = await pageModel.OnPostAsync(recId);

        // Assert
        Assert.IsType<RedirectToPageResult>(result);
    }
}