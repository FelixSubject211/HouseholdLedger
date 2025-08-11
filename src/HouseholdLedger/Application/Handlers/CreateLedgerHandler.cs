using HouseholdLedger.Application.Commands;
using HouseholdLedger.Application.Interfaces;
using HouseholdLedger.Domain.Entities;

namespace HouseholdLedger.Application.Handlers;

public class CreateLedgerHandler(ILedgerRepository repository)
{
    public async Task Handle(CreateLedgerCommand command)
    {
        var ledger = new Ledger(command.Name, command.Currency);
        await repository.SaveAsync(ledger);
    }
}
