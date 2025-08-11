namespace HouseholdLedger.ConsoleApp.AppState.States;

public sealed class StartState : IState
{
    public List<AppCommand> GetActions(AppContext context)
    {
        return
        [
            new AppCommand.NoInputAction("Create new ledger", async () =>
            {
                await context.SetState(new CreatingLedgerState());
            }),
            new AppCommand.NoInputAction("Open existing ledger", async () =>
            {
                await context.SetState(new SelectingLedgerState());
            })
        ];
    }
}
