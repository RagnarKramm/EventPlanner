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

public class ParticipantEditingPageTests
{
    [Fact]
    public async Task OnPostEditPersonAsync_ReturnsAPageResult_WhenModelStateIsInvalid()
    {
        // Arrange
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase("InMemoryDb");
        
        var mockAppDbContext = new Mock<AppDbContext>(optionsBuilder.Options);
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
        var pageModel = new EditModel(mockAppDbContext.Object)
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
    public async Task OnPostEditBusinessAsync_ReturnsAPageResult_WhenModelStateIsInvalid()
    {
         // Arrange
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase("InMemoryDb");
        
        var mockAppDbContext = new Mock<AppDbContext>(optionsBuilder.Options);
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
        var pageModel = new WebApp.Pages.Businesses.EditModel(mockAppDbContext.Object)
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
    public async Task OnPostEditPersonAsync_ReturnsARedirectToPageResult_WhenModelStateIsValid()
    {
        // Arrange
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase("InMemoryDb");
        var mockAppDbContext = new Mock<AppDbContext>(optionsBuilder.Options);
        var testPerson = new Person()
        {
            FirstName = "Mari",
            LastName = "Vaarikas",
            IdCode = "60211244205",
            ParticipantCount = 1,
            AdditionalInfo = "Mulle meeldivad maasikad ja muud marjad.",
            EventId = 1
        };
        mockAppDbContext.Setup(db => db.EditPersonAsync(testPerson)).Returns(Task.FromResult(testPerson));
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
        var pageModel = new EditModel(mockAppDbContext.Object)
        {
            PageContext = pageContext,
            TempData = tempData,
            Url = new UrlHelper(actionContext)
        };
        pageModel.Person = testPerson;

        // Act
        // A new ModelStateDictionary is valid by default.
        var result = await pageModel.OnPostAsync();

        // Assert
        Assert.IsType<RedirectToPageResult>(result);
        var redirectToPageResult = result as RedirectToPageResult;
        Assert.NotNull(redirectToPageResult!.PageName);
        Assert.Equal("./Details", redirectToPageResult.PageName);
    }
    
    [Fact]
    public async Task OnPostEditBusinessAsync_ReturnsARedirectToPageResult_WhenModelStateIsValid()
    {
        
        // Arrange
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase("InMemoryDb");
        var mockAppDbContext = new Mock<AppDbContext>(optionsBuilder.Options);
        var testBusiness = new Business
        {
            BusinessName = "Puud koju OÜ",
            RegisterCode = "19472819",
            ParticipantCount = 31,
            AdditionalInfo = "Meie firmast tuleb kaks inimest, kes soovivad taimset toitu.",
            EventId = 1
        };
        mockAppDbContext.Setup(db => db.EditBusinessAsync(testBusiness)).Returns(Task.FromResult(testBusiness));
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
        var pageModel = new WebApp.Pages.Businesses.EditModel(mockAppDbContext.Object)
        {
            PageContext = pageContext,
            TempData = tempData,
            Url = new UrlHelper(actionContext)
        };
        pageModel.Business = testBusiness;

        // Act
        // A new ModelStateDictionary is valid by default.
        var result = await pageModel.OnPostAsync();

        // Assert
        Assert.IsType<RedirectToPageResult>(result);
        var redirectToPageResult = result as RedirectToPageResult;
        Assert.NotNull(redirectToPageResult!.PageName);
        Assert.Equal("./Details", redirectToPageResult.PageName);
    }
}