namespace HouseholdLedger.Domain.Entities;

public record Booking(
    Guid Id,
    DateTime Date,
    decimal Amount,
    Currency Currency
);
