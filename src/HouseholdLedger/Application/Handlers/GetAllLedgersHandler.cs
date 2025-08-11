using HouseholdLedger.Application.Interfaces;
using HouseholdLedger.Domain.Entities;

namespace HouseholdLedger.Application.Handlers;

public class GetAllLedgersHandler(ILedgerRepository repository)
{
    public async Task<List<Ledger>> Handle()
    {
        return await repository.GetAllAsync();
    }
}
