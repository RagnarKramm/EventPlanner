using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApp.DAL;
using WebApp.Domain;
using WebApp.Pages;
using Xunit;
using Xunit.Sdk;

namespace Tests.UnitTests;

public class UnitTests
{
    
    // seed data
    private readonly Event _testEvent = new Event
    {
        Name = "Toidu TESTIMISE mess",
        Description = "See üritus on loodud, et testida toitu ja ka ürituse lisamist anbmebaasi.",
        HappeningAt = DateTime.UtcNow.AddMonths(1),
        Location = "Tammsaare park."
    };

    private readonly PaymentOption _paymentOptionCash = new PaymentOption
    {
        Name = "Sularaha",
        Description = "Makse sularahaga"
    };
    
    private readonly PaymentOption _paymentOptionTransfer = new PaymentOption
    {
        Name = "Pangaülekanne",
        Description = "Makse pangaülekandega"
    };

    private readonly Person _person = new Person()
    {
        FirstName = "Martin",
        LastName = "Maasikas",
        IdCode = "50211244205",
        ParticipantCount = 1,
        AdditionalInfo = "Mulle meeldivad vaarikad ja muud marjad."
    };
    
    private readonly Business _business = new Business
    {
        BusinessName = "Puud koju OÜ",
        RegisterCode = "19472819",
        ParticipantCount = 31,
        AdditionalInfo = "Meie firmast tuleb kaks inimest, kes soovivad taimset toitu."
    };
    
    [Fact]
    public async Task TestCreateReadUpdateDeleteOnEvent()
    {
        await using var db = new AppDbContext(Utilities.TestDbContextOptions());
        // Arrange
        var id = 99;
        _testEvent.Id = id;
        db.Events.Add(_testEvent);
        await db.SaveChangesAsync();


        // Act
        var eventFromDb = await db.Events.FirstAsync(item => item.Id == id);

        // Assert 
        Assert.Equal(
            _testEvent.Name, 
            eventFromDb.Name);
        Assert.Equal(
            _testEvent.Description, 
            eventFromDb.Description);
        Assert.Equal(
            _testEvent.HappeningAt, 
            eventFromDb.HappeningAt);
        Assert.Equal(
            _testEvent.Location, 
            eventFromDb.Location);
        
        // Update
        var newDescription = "Uus info, mida varem siin ei olnud!";
        eventFromDb.Description = newDescription;
        db.Attach(eventFromDb).State = EntityState.Modified;
        await db.SaveChangesAsync();
        
        // Act
        var updatedEvent = await db.Events.FirstAsync(item => item.Id == id);
        
        
        // Assert
        Assert.Equal(newDescription, updatedEvent.Description);
        
        
        // Delete
        var eventToDelete = await db.Events.FirstAsync(item => item.Id == id);
        
        // Act
        db.Events.Remove(eventToDelete);
        await db.SaveChangesAsync();

        // Assert
        Assert.Empty(db.Events);

    }

    [Fact]
    public async Task TestCreateReadUpdateDeleteOnPaymentOption()
    {
        await using var db = new AppDbContext(Utilities.TestDbContextOptions());
        // Arrange
        var id = 42;
        _paymentOptionCash.Id = id;
        db.PaymentOptions.Add(_paymentOptionCash);
        await db.SaveChangesAsync();
        
        // Act
        var paymentOptionFromDb = await db.PaymentOptions.FirstAsync(item => item.Id == id);

        // Assert 
        Assert.Equal(
            _paymentOptionCash.Name, 
            paymentOptionFromDb.Name);
        Assert.Equal(
            _paymentOptionCash.Description, 
            paymentOptionFromDb.Description);
        
        // Update
        var newDescription = "Uus info, mida varem siin ei olnud!";
        paymentOptionFromDb.Description = newDescription;
        db.Attach(paymentOptionFromDb).State = EntityState.Modified;
        await db.SaveChangesAsync();
        
        // Act
        var updatedPaymentOptions = await db.PaymentOptions.FirstAsync(item => item.Id == id);
        
        // Assert
        Assert.Equal(newDescription, updatedPaymentOptions.Description);
        
        // Delete
        var paymentOption = await db.PaymentOptions.FirstAsync(item => item.Id == id);
        
        // Act
        db.PaymentOptions.Remove(paymentOption);
        await db.SaveChangesAsync();

        // Assert
        Assert.Empty(db.PaymentOptions);
    }
    
    [Fact]
    public async Task TestCreateReadUpdateDeleteOnParticipantPerson()
    {
        await using var db = new AppDbContext(Utilities.TestDbContextOptions());
        // Arrange
        var id = 52;
        _person.Id = id;
        db.Persons.Add(_person);
        await db.SaveChangesAsync();
        
        // Act
        var personFromDb = await db.Persons.FirstAsync(item => item.Id == id);

        // Assert 
        Assert.Equal(
            _person.FirstName, 
            personFromDb.FirstName);
        Assert.Equal(
            _person.LastName, 
            personFromDb.LastName);
        
        // Update
        var newAdditionalInfo = "Mulle meeldivad tegelikult maasikad.";
        personFromDb.AdditionalInfo = newAdditionalInfo;
        db.Attach(personFromDb).State = EntityState.Modified;
        await db.SaveChangesAsync();
        
        // Act
        var updatedPerson = await db.Persons.FirstAsync(item => item.Id == id);
        
        // Assert
        Assert.Equal(newAdditionalInfo, updatedPerson.AdditionalInfo);
        
        // Delete
        var participant = await db.Persons.FirstAsync(item => item.Id == id);
        
        // Act
        db.Persons.Remove(participant);
        await db.SaveChangesAsync();

        // Assert
        Assert.Empty(db.Persons);
        
    }
    
    [Fact]
    public async Task TestCreateReadUpdateDeleteOnParticipantBusiness()
    {
        await using var db = new AppDbContext(Utilities.TestDbContextOptions());
        // Arrange
        var id = 12;
        _business.Id = id;
        db.Businesses.Add(_business);
        await db.SaveChangesAsync();
        
        // Act
        var businessFromDb = await db.Businesses.FirstAsync(item => item.Id == id);

        // Assert 
        Assert.Equal(
            _business.BusinessName, 
            businessFromDb.BusinessName);
        Assert.Equal(
            _business.RegisterCode, 
            businessFromDb.RegisterCode);
        
        // Update
        var newBusinessName = "Puit tuppa AS";
        businessFromDb.BusinessName = newBusinessName;
        db.Attach(businessFromDb).State = EntityState.Modified;
        await db.SaveChangesAsync();
        
        // Act
        var updatedBusiness = await db.Businesses.FirstAsync(item => item.Id == id);
        
        // Assert
        Assert.Equal(newBusinessName, updatedBusiness.BusinessName);
        
        // Delete
        var business = await db.Businesses.FirstAsync(item => item.Id == id);
        
        // Act
        db.Businesses.Remove(business);
        await db.SaveChangesAsync();

        // Assert
        Assert.Empty(db.Businesses);
        
    }
    
    [Fact]
    public async Task? AddingEventAndParticipantsToDb_ParticipantCountForEventCorrect()
    {
        await using var db = new AppDbContext(Utilities.TestDbContextOptions());

        // Assign
        var eventId = 2;
        var cashPaymentId = 4;
        var transferPaymentId = 1;

        _testEvent.Id = eventId;
        db.Events.Add(_testEvent);
        _paymentOptionCash.Id = cashPaymentId;
        _paymentOptionTransfer.Id = transferPaymentId;
        db.PaymentOptions.Add(_paymentOptionCash);
        db.PaymentOptions.Add(_paymentOptionTransfer);
        
        _business.PaymentOption = _paymentOptionCash;
        _business.PaymentOptionId = cashPaymentId;
        _business.EventId = eventId;

        _person.PaymentOption = _paymentOptionTransfer;
        _person.PaymentOptionId = transferPaymentId;
        _person.EventId = eventId;
        
        db.Businesses.Add(_business);
        db.Persons.Add(_person);
        
        // Act
        var participants = IndexModel.GetParticipantCount(_testEvent);
        var viewModel = new WebApp.Pages.Events.CreateModel(db).OnGet();
        
        //Assert
        Assert.Equal(32, participants);
    }

    [Fact]
    public async Task ParticipantCountForEvent()
    {
        await using var db = new AppDbContext(Utilities.TestDbContextOptions());

    }
}