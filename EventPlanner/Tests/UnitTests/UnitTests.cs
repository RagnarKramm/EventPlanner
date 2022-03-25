using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApp.DAL;
using WebApp.Domain;
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

    private readonly ParticipantType _participantTypePerson= new ParticipantType
    {
        Name = "Eraisik",
        DescriptionLimit = 1500,
        Line1Header = "Eesnimi",
        Line2Header = "Perekonnanimi",
        Line3Header = "Isikukood"
    };
    
    private readonly ParticipantType _participantTypeBusiness= new ParticipantType
    {
        Name = "Ettevõte",
        DescriptionLimit = 5000,
        Line1Header = "Juriidiline nimi",
        Line2Header = "Registrikood",
        Line3Header = "Osavõtjate arv"
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

    private readonly Participant _participantPerson = new Participant
    {
        ParticipantLine1 = "Martin",
        ParticipantLine2 = "Maasikas",
        ParticipantLine3 = "50211244205",
        AdditionalInformation = "Mulle meeldivad vaarikad ja muud marjad."
    };
    
    private readonly Participant _participantBusiness = new Participant
    {
        ParticipantLine1 = "Puud koju OÜ",
        ParticipantLine2 = "19472819",
        ParticipantLine3 = "31",
        AdditionalInformation = "Meie firmast tuleb kaks inimest, kes soovivad taimset toitu."
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
    public async Task TestCreateReadUpdateDeleteOnParticipantType()
    {
        await using var db = new AppDbContext(Utilities.TestDbContextOptions());
        // Arrange
        var id = 0;
        _participantTypePerson.Id = id;
        db.ParticipantTypes.Add(_participantTypePerson);
        await db.SaveChangesAsync();
        
        // Act
        var participantTypesFromDb = await db.ParticipantTypes.FirstAsync(item => item.Id == id);

        // Assert 
        Assert.Equal(
            _participantTypePerson.Name, 
            participantTypesFromDb.Name);
        Assert.Equal(
            _participantTypePerson.Line2Header, 
            participantTypesFromDb.Line2Header);
        
        // Update
        var newLine2Header = "Registreerimise aeg";
        participantTypesFromDb.Line2Header = newLine2Header;
        db.Attach(participantTypesFromDb).State = EntityState.Modified;
        await db.SaveChangesAsync();
        
        // Act
        var updatedParticipantType = await db.ParticipantTypes.FirstAsync(item => item.Id == id);
        
        // Assert
        Assert.Equal(newLine2Header, updatedParticipantType.Line2Header);
        
        // Delete
        var participantType = await db.ParticipantTypes.FirstAsync(item => item.Id == id);
        
        // Act
        db.ParticipantTypes.Remove(participantType);
        await db.SaveChangesAsync();

        // Assert
        Assert.Empty(db.ParticipantTypes);
    }
    
    [Fact]
    public async Task TestCreateReadUpdateDeleteOnParticipantBusiness()
    {
        await using var db = new AppDbContext(Utilities.TestDbContextOptions());
        // Arrange
        var id = 52;
        _participantBusiness.Id = id;
        db.Participants.Add(_participantBusiness);
        await db.SaveChangesAsync();
        
        // Act
        var participantFromDb = await db.Participants.FirstAsync(item => item.Id == id);

        // Assert 
        Assert.Equal(
            _participantBusiness.ParticipantLine1, 
            participantFromDb.ParticipantLine1);
        Assert.Equal(
            _participantBusiness.ParticipantLine3, 
            participantFromDb.ParticipantLine3);
        
        // Update
        var newBusinessName = "Puit tuppa AS";
        participantFromDb.ParticipantLine1 = newBusinessName;
        db.Attach(participantFromDb).State = EntityState.Modified;
        await db.SaveChangesAsync();
        
        // Act
        var updatedParticipant = await db.Participants.FirstAsync(item => item.Id == id);
        
        // Assert
        Assert.Equal(newBusinessName, updatedParticipant.ParticipantLine1);
        
        // Delete
        var participant = await db.Participants.FirstAsync(item => item.Id == id);
        
        // Act
        db.Participants.Remove(participant);
        await db.SaveChangesAsync();

        // Assert
        Assert.Empty(db.Participants);
        
    }
    
    [Fact]
    public async Task? AddingEventAndParticipantsToDb_ParticipantCountForEventCorrect()
    {
        await using var db = new AppDbContext(Utilities.TestDbContextOptions());

        // Assign
        var eventId = 2;
        var participantTypeBusinessId = 4;
        var participantTypePersonId = 1;

        _testEvent.Id = eventId;
        db.Events.Add(_testEvent);
        _participantTypeBusiness.Id = participantTypeBusinessId;
        _participantTypePerson.Id = participantTypePersonId;
        db.ParticipantTypes.Add(_participantTypeBusiness);
        db.ParticipantTypes.Add(_participantTypePerson);
        
        _participantBusiness.ParticipantTypeId = participantTypeBusinessId;
        _participantBusiness.EventId = eventId;
        _participantPerson.ParticipantTypeId = participantTypePersonId;
        _participantPerson.EventId = eventId;
        db.Participants.Add(_participantBusiness);
        db.Participants.Add(_participantPerson);
        
        // Act
        var participants = new WebApp.Pages.IndexModel(db).GetParticipantCount(_testEvent);
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