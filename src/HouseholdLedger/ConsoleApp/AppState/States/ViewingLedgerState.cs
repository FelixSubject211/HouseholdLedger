namespace HouseholdLedger.ConsoleApp.AppState.States;

public sealed class ViewingLedgerState(Guid ledgerId) : IState
{
    public List<AppCommand> GetActions(AppContext context)
    {
        var ledger = context.GetLedgerById.Handle(ledgerId).Result;

        if (ledger is null)
        {
            return
            [
                new AppCommand.NoInputAction("Ledger not found", async () =>
                {
                    await context.SetState(new StartState());
                })
            ];
        }

        return
        [
            new AppCommand.Display($"Ledger: {ledger.Name} ({ledger.Currency.Code})"),
            .. ledger.Bookings.Select(b =>
                new AppCommand.Display($"{b.Date:yyyy-MM-dd}: {b.Amount} {b.Currency.Code}")
            ),
            new AppCommand.NoInputAction("Add booking", async () =>
            {
                await context.SetState(new AddBookingState(ledger));
            }),
            new AppCommand.NoInputAction("Back to start", async () =>
            {
                await context.SetState(new StartState());
            })
        ];
    }
}
