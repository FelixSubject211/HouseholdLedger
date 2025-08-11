using HouseholdLedger.Application.Commands;
using HouseholdLedger.Domain.Entities;

namespace HouseholdLedger.ConsoleApp.AppState.States;

public sealed class AddBookingState(Ledger ledger, decimal? amount = null) : IState
{
    public List<AppCommand> GetActions(AppContext context)
    {
        if (amount == null)
        {
            return
            [
                new AppCommand.InputAction("Enter booking amount", async input =>
                {
                    if (decimal.TryParse(input, out var amount))
                    {
                        await context.SetState(new AddBookingState(ledger, amount));
                    }
                    else
                    {
                        await context.SetState(new AddBookingState(ledger));
                    }
                })
            ];
        }

        if (amount <= 0)
        {
            return
            [
                new AppCommand.NoInputAction("Amount must be greater than zero", async () =>
                {
                    await context.SetState(new AddBookingState(ledger));
                })
            ];
        }

        return
        [
            new AppCommand.NoInputAction("Confirm booking", async () =>
            {
                await context.AddBooking.Handle(new AddBookingCommand(
                    ledger.Id,
                    DateTime.Now,
                    amount.Value,
                    ledger.Currency
                ));

                await context.SetState(new ViewingLedgerState(ledger.Id));
            }),
            new AppCommand.NoInputAction("Cancel", async () =>
            {
                await context.SetState(new ViewingLedgerState(ledger.Id));
            })
        ];
    }
}
