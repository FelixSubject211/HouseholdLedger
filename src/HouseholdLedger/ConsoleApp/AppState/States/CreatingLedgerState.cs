using HouseholdLedger.Application.Commands;
using HouseholdLedger.Domain.Entities;

namespace HouseholdLedger.ConsoleApp.AppState.States;

public sealed class CreatingLedgerState(string? name = null, string? currency = null) : IState
{
    public List<AppCommand> GetActions(AppContext context)
    {
        if (name == null)
        {
            return
            [
                new AppCommand.InputAction("Enter ledger name", async input =>
                {
                    await context.SetState(new CreatingLedgerState(input, currency));
                })
            ];
        }

        if (currency == null)
        {
            return
            [
                new AppCommand.InputAction("Enter currency", async input =>
                {
                    var command = new CreateLedgerCommand(name, new Currency(input!));
                    await context.CreateLedger.Handle(command);

                    await context.SetState(new StartState());
                })
            ];
        }

        return
        [
            new AppCommand.NoInputAction("Back to start", async () =>
            {
                await context.SetState(new StartState());
            })
        ];
    }
}
