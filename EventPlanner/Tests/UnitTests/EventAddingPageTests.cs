using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Moq;
using WebApp.DAL;
using WebApp.Domain;
using WebApp.Pages;
using WebApp.Pages.Events;
using Xunit;

namespace Tests.UnitTests;

public class EventAddingPageTests
{
    [Fact]
    public async Task OnPostAddEventAsync_ReturnsAPageResult_WhenModelStateIsInvalid()
    {
        // Arrange
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase("InMemoryDb");
        
        var mockAppDbContext = new Mock<AppDbContext>(optionsBuilder.Options);
        var expectedEvents =  new List<Event>
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
        
        mockAppDbContext.Setup(db => db.GetEventsAndParticipantsAsync()).Returns(Task.FromResult(expectedEvents));
        
        var httpContext = new DefaultHttpContext();
        var modelState = new ModelStateDictionary();
        var actionContext = new ActionContext(httpContext, new RouteData(), new PageActionDescriptor(), modelState);
        var modelMetadataProvider = new EmptyModelMetadataProvider();
        var viewData = new ViewDataDictionary(modelMetadataProvider, modelState);
        var tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());
        var pageContext = new PageContext(actionContext)
        {
            ViewData = viewData
        };
        var pageModel = new CreateModel(mockAppDbContext.Object)
        {
            PageContext = pageContext,
            TempData = tempData,
            Url = new UrlHelper(actionContext)
        };
        pageModel.ModelState.AddModelError("Event.Name", "The Name field is required.");

        // Act
        var result = await pageModel.OnPostAsync();

        // Assert
        Assert.IsType<PageResult>(result);
    }
    
    
    [Fact]
    public async Task OnPostAddMessageAsync_ReturnsARedirectToPageResult_WhenModelStateIsValid()
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
        
        mockAppDbContext.Setup(db => db.GetEventsAndParticipantsAsync()).Returns(Task.FromResult(expectedEvents));
        var httpContext = new DefaultHttpContext();
        var modelState = new ModelStateDictionary();
        var actionContext = new ActionContext(httpContext, new RouteData(), new PageActionDescriptor(), modelState);
        var modelMetadataProvider = new EmptyModelMetadataProvider();
        var viewData = new ViewDataDictionary(modelMetadataProvider, modelState);
        var tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());
        var pageContext = new PageContext(actionContext)
        {
            ViewData = viewData
        };
        var pageModel = new CreateModel(mockAppDbContext.Object)
        {
            PageContext = pageContext,
            TempData = tempData,
            Url = new UrlHelper(actionContext)
        };

        // Act
        // A new ModelStateDictionary is valid by default.
        var result = await pageModel.OnPostAsync();

        // Assert
        Assert.IsType<RedirectToPageResult>(result);
    }
    
}