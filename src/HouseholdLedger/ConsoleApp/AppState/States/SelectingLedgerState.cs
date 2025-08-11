namespace HouseholdLedger.ConsoleApp.AppState.States;

public sealed class SelectingLedgerState : IState
{
    public List<AppCommand> GetActions(AppContext context)
    {
        var ledgers = context.GetAllLedgers.Handle().Result;

        if (ledgers.Count == 0)
        {
            return
            [
                new AppCommand.NoInputAction("No ledgers found", async () =>
                {
                    await context.SetState(new StartState());
                })
            ];
        }

        return
        [
            .. ledgers.Select(ledger =>
                (AppCommand)new AppCommand.NoInputAction(
                    $"Open ledger: {ledger.Name} ({ledger.Currency.Code})",
                    async () => await context.SetState(new ViewingLedgerState(ledger.Id))
                )
            ),
            new AppCommand.NoInputAction("Back to start", async () =>
            {
                await context.SetState(new StartState());
            })
        ];
    }
}
