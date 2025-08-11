using HouseholdLedger.Domain.Entities;

namespace HouseholdLedger.Application.Commands;

public record CreateLedgerCommand(string Name, Currency Currency);
