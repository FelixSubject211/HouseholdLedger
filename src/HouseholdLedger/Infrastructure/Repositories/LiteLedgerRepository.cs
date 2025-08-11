using HouseholdLedger.Application.Interfaces;
using HouseholdLedger.Domain.Entities;

using LiteDB;

namespace HouseholdLedger.Infrastructure.Repositories;

public class LiteLedgerRepository : ILedgerRepository
{
    private const string _databasePath = "ledger.db";

    public Task SaveAsync(Ledger ledger)
    {
        using var db = new LiteDatabase(_databasePath);
        var collection = db.GetCollection<LedgerDocument>("ledgers");

        var document = LedgerDocument.FromDomain(ledger);
        collection.Upsert(document);

        return Task.CompletedTask;
    }

    public Task<Ledger?> GetByIdAsync(Guid id)
    {
        using var db = new LiteDatabase(_databasePath);
        var collection = db.GetCollection<LedgerDocument>("ledgers");

        var document = collection.FindById(id);
        return Task.FromResult(document?.ToDomain());
    }

    public Task<List<Ledger>> GetAllAsync()
    {
        using var db = new LiteDatabase(_databasePath);
        var collection = db.GetCollection<LedgerDocument>("ledgers");

        var ledgers = collection
            .FindAll()
            .Select(doc => doc.ToDomain())
            .ToList();

        return Task.FromResult(ledgers);
    }
}
