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
using WebApp.Pages.Persons;
using Xunit;

namespace Tests.UnitTests;

public class ParticipantAddingPageTests
{

    [Fact]
    public async Task OnPostAddPersonAsync_ReturnsAPageResult_WhenModelStateIsInvalid()
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
        
        var persons =  new List<Person>
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
        
        mockAppDbContext.Setup(db => db.GetPersonsAsync()).Returns(Task.FromResult(persons));
        mockAppDbContext.Setup(db => db.GetEventById(testEvent.Id)).Returns(Task.FromResult(testEvent));

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
        pageModel.ModelState.AddModelError("Person.FirstName", "The FirstName field is required.");

        // Act
        var result = await pageModel.OnPostAsync();

        // Assert
        Assert.IsType<PageResult>(result);
    }

    [Fact]
    public async Task OnPostAddBusinessAsync_ReturnsAPageResult_WhenModelStateIsInvalid()
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
        var businesses = new List<Business>
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
        };
        
        mockAppDbContext.Setup(db => db.GetEventById(testEvent.Id)).Returns(Task.FromResult(testEvent));
        mockAppDbContext.Setup(db => db.GetBusinessAsync()).Returns(Task.FromResult(businesses));
        
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
        var pageModel = new WebApp.Pages.Businesses.CreateModel(mockAppDbContext.Object)
        {
            PageContext = pageContext,
            TempData = tempData,
            Url = new UrlHelper(actionContext)
        };
        pageModel.ModelState.AddModelError("Business.RegisterCode", "The RegisterCode field is required.");

        // Act
        var result = await pageModel.OnPostAsync();

        // Assert
        Assert.IsType<PageResult>(result);
    }

    [Fact]
    public async Task OnPostAddPersonAsync_ReturnsARedirectToPageResult_WhenModelStateIsValid()
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
        
        var persons =  new List<Person>
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

        mockAppDbContext.Setup(db => db.GetPersonsAsync()).Returns(Task.FromResult(persons));
        mockAppDbContext.Setup(db => db.GetEventById(testEvent.Id)).Returns(Task.FromResult(testEvent));
        
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
    

    [Fact]
    public async Task OnPostAddBusinessAsync_ReturnsARedirectToPageResult_WhenModelStateIsValid()
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
        var businesses = new List<Business>
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
        };
        
        mockAppDbContext.Setup(db => db.GetEventById(testEvent.Id)).Returns(Task.FromResult(testEvent));
        mockAppDbContext.Setup(db => db.GetBusinessAsync()).Returns(Task.FromResult(businesses));

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
        var pageModel = new WebApp.Pages.Businesses.CreateModel(mockAppDbContext.Object)
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