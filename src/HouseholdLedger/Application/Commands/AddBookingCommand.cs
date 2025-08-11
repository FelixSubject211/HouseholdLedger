using HouseholdLedger.Domain.Entities;

namespace HouseholdLedger.Application.Commands;

public record AddBookingCommand(
    Guid LedgerId,
    DateTime Date,
    decimal Amount,
    Currency Currency
);
