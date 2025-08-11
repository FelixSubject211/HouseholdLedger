using HouseholdLedger.Application.Handlers;
using HouseholdLedger.ConsoleApp.AppState.States;

namespace HouseholdLedger.ConsoleApp.AppState;

public sealed class AppState
{
    private IState _current;

    private readonly AppContext _context;

    public AppState(
        CreateLedgerHandler createLedger,
        AddBookingHandler addBooking,
        GetAllLedgersHandler getAllLedgers,
        GetLedgerByIdHandler getLedgerById)
    {
        _current = new StartState();
        _context = new AppContext(
            createLedger,
            addBooking,
            getAllLedgers,
            getLedgerById,
            next => _current = next
        );
    }

    public List<AppCommand> GetActions() => _current.GetActions(_context);
}
