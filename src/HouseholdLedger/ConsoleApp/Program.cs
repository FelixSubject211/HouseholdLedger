using HouseholdLedger.Application.Handlers;
using HouseholdLedger.ConsoleApp;
using HouseholdLedger.ConsoleApp.AppState;
using HouseholdLedger.Infrastructure.Repositories;

class Program
{
    static async Task Main()
    {
        var repo = new LiteLedgerRepository();

        var state = new AppState(
            new CreateLedgerHandler(repo),
            new AddBookingHandler(repo),
            new GetAllLedgersHandler(repo),
            new GetLedgerByIdHandler(repo)
        );

        while (true)
        {
            var actions = state.GetActions();
            var selectable = actions
                .Where(a => a is not AppCommand.Display)
                .ToList();

            switch (actions.Select(a => a is not AppCommand.Display).Count())
            {
                case 1:
                    {
                        var action = selectable[0];
                        WriteActionPrompt(action);

                        var input = action.RequiresInput ? Console.ReadLine() : ReadEmptyLine();
                        await action.Execute(input);
                        continue;
                    }

                default:
                    {
                        foreach (var action in actions)
                        {
                            if (action is AppCommand.Display)
                            {
                                Console.WriteLine(action.Description);
                            }
                            else
                            {
                                var number = selectable.IndexOf(action) + 1;
                                Console.WriteLine($"{number}. {action.Description}");
                            }
                        }

                        Console.Write("> ");
                        var selection = Console.ReadLine();

                        if (int.TryParse(selection, out var index) &&
                            index >= 1 && index <= selectable.Count)
                        {
                            var action = selectable[index - 1];

                            if (action.RequiresInput)
                            {
                                WriteActionPrompt(action);
                                string? input = Console.ReadLine();
                                await action.Execute(input);
                            }
                            else
                            {
                                await action.Execute();
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid selection. Try again.");
                        }

                        break;
                    }
            }
        }
    }

    static void WriteActionPrompt(AppCommand action)
    {
        var suffix = action.RequiresInput ? ":" : ".";
        Console.Write($"{action.Description}{suffix} ");
    }

    static string ReadEmptyLine()
    {
        Console.ReadLine();
        return "";
    }
}
