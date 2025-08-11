namespace HouseholdLedger.ConsoleApp;

public interface IState
{
    List<AppCommand> GetActions(AppContext context);
}
