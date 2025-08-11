using HouseholdLedger.Application.Commands;
using HouseholdLedger.Application.Interfaces;
using HouseholdLedger.Domain.Entities;

namespace HouseholdLedger.Application.Handlers;

public class AddBookingHandler(ILedgerRepository repository)
{
    public async Task Handle(AddBookingCommand command)
    {
        var ledger = await repository.GetByIdAsync(command.LedgerId) ?? throw new Exception("Ledger not found");
        var booking = new Booking(
            Guid.NewGuid(),
            command.Date,
            command.Amount,
            command.Currency
        );
        var updatedLedger = ledger.AddBooking(booking);
        await repository.SaveAsync(updatedLedger);
    }
}
