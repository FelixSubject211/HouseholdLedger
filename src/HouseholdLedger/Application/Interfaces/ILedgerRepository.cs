using HouseholdLedger.Domain.Entities;

namespace HouseholdLedger.Application.Interfaces;

public interface ILedgerRepository
{
    Task SaveAsync(Ledger ledger);
    Task<Ledger?> GetByIdAsync(Guid id);
    Task<List<Ledger>> GetAllAsync();
}
