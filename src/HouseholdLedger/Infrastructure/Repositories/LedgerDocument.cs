using HouseholdLedger.Domain.Entities;

using LiteDB;

namespace HouseholdLedger.Infrastructure.Repositories;

public class LedgerDocument
{
    [BsonId]
    public Guid Id
    {
        get; set;
    }

    public string Name { get; set; } = string.Empty;

    public Currency Currency { get; set; } = new("");

    public List<Booking> Bookings { get; set; } = [];

    public static LedgerDocument FromDomain(Ledger ledger) => new()
    {
        Id = ledger.Id,
        Name = ledger.Name,
        Currency = ledger.Currency,
        Bookings = [.. ledger.Bookings]
    };

    public Ledger ToDomain() => new(Id, Name, Currency, [.. Bookings]);
}
