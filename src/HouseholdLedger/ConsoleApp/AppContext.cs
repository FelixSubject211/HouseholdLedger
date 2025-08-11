using HouseholdLedger.Application.Handlers;

namespace HouseholdLedger.ConsoleApp;

public sealed class AppContext
{
    public CreateLedgerHandler CreateLedger
    {
        get;
    }
    public AddBookingHandler AddBooking
    {
        get;
    }
    public GetAllLedgersHandler GetAllLedgers
    {
        get;
    }
    public GetLedgerByIdHandler GetLedgerById
    {
        get;
    }

    private readonly Action<IState> _setState;

    public AppContext(
        CreateLedgerHandler createLedger,
        AddBookingHandler addBooking,
        GetAllLedgersHandler getAllLedgers,
        GetLedgerByIdHandler getLedgerById,
        Action<IState> setState)
    {
        CreateLedger = createLedger;
        AddBooking = addBooking;
        GetAllLedgers = getAllLedgers;
        GetLedgerById = getLedgerById;
        _setState = setState;
    }

    public async Task SetState(IState next)
    {
        await Task.Yield();
        _setState(next);
    }
}
