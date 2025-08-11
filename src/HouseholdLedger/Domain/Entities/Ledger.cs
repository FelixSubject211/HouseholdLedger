using System.Collections.Immutable;

namespace HouseholdLedger.Domain.Entities;

public record Ledger(
    Guid Id,
    string Name,
    Currency Currency,
    ImmutableList<Booking> Bookings
)
{
    public Ledger(string name, Currency currency) : this(Guid.NewGuid(), name, currency, []) { }

    public Ledger AddBooking(Booking booking) => this with { Bookings = Bookings.Add(booking) };
}
