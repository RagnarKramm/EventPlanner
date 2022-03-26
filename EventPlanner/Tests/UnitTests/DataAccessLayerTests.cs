using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApp.DAL;
using WebApp.Domain;
using Xunit;

namespace Tests.UnitTests;

public class DataAccessLayerTests
{
    [Fact]
    public async Task TestCreateReadUpdateDeleteOnEvent()
    {
        await using var db = new AppDbContext(Utilities.TestDbContextOptions());
        // Arrange
        var id = 99;
        var testEvent = new Event
        {
            Name = "Toidu TESTIMISE mess",
            Description = "See üritus on loodud, et testida toitu ja ka ürituse lisamist anbmebaasi.",
            HappeningAt = DateTime.UtcNow.AddMonths(1),
            Location = "Tammsaare park.",
            Id = id
        };
        db.Events.Add(testEvent);
        await db.SaveChangesAsync();


        // Act
        var eventFromDb = await db.Events.FirstAsync(item => item.Id == id);

        // Assert 
        Assert.Equal(
            testEvent.Name, 
            eventFromDb.Name);
        Assert.Equal(
            testEvent.Description, 
            eventFromDb.Description);
        Assert.Equal(
            testEvent.HappeningAt, 
            eventFromDb.HappeningAt);
        Assert.Equal(
            testEvent.Location, 
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
        var paymentOptionCash = new PaymentOption
        {
            Name = "Sularaha",
            Description = "Makse sularahaga",
            Id = id
        };
        db.PaymentOptions.Add(paymentOptionCash);
        await db.SaveChangesAsync();
        
        // Act
        var paymentOptionFromDb = await db.PaymentOptions.FirstAsync(item => item.Id == id);

        // Assert 
        Assert.Equal(
            paymentOptionCash.Name, 
            paymentOptionFromDb.Name);
        Assert.Equal(
            paymentOptionCash.Description, 
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
        var person = new Person()
        {
            FirstName = "Martin",
            LastName = "Maasikas",
            IdCode = "50211244205",
            ParticipantCount = 1,
            AdditionalInfo = "Mulle meeldivad vaarikad ja muud marjad.",
            Id = id
        };
        db.Persons.Add(person);
        await db.SaveChangesAsync();
        
        // Act
        var personFromDb = await db.Persons.FirstAsync(item => item.Id == id);

        // Assert 
        Assert.Equal(
            person.FirstName, 
            personFromDb.FirstName);
        Assert.Equal(
            person.LastName, 
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
        var business = new Business
        {
            BusinessName = "Puud koju OÜ",
            RegisterCode = "19472819",
            ParticipantCount = 31,
            AdditionalInfo = "Meie firmast tuleb kaks inimest, kes soovivad taimset toitu.",
            Id = id
        };
        db.Businesses.Add(business);
        await db.SaveChangesAsync();
        
        // Act
        var businessFromDb = await db.Businesses.FirstAsync(item => item.Id == id);

        // Assert 
        Assert.Equal(
            business.BusinessName, 
            businessFromDb.BusinessName);
        Assert.Equal(
            business.RegisterCode, 
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
        var businessToDelete = await db.Businesses.FirstAsync(item => item.Id == id);
        
        // Act
        db.Businesses.Remove(businessToDelete);
        await db.SaveChangesAsync();

        // Assert
        Assert.Empty(db.Businesses);
        
    }
}