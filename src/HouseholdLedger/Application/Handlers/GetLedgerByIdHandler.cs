using HouseholdLedger.Application.Interfaces;
using HouseholdLedger.Domain.Entities;

namespace HouseholdLedger.Application.Handlers;

public class GetLedgerByIdHandler(ILedgerRepository repository)
{
    public async Task<Ledger?> Handle(Guid id)
    {
        return await repository.GetByIdAsync(id);
    }
}
