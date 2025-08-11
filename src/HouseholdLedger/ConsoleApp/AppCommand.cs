namespace HouseholdLedger.ConsoleApp;

public abstract record AppCommand(string Description)
{
    public abstract Task Execute(string? input = null);

    public virtual bool RequiresInput => false;

    public sealed record Display(string Description)
        : AppCommand(Description)
    {
        public override bool RequiresInput => false;
        public override Task Execute(string? input = null) => Task.CompletedTask;
    }

    public sealed record InputAction(string Description, Func<string?, Task> Handler)
        : AppCommand(Description)
    {
        public override bool RequiresInput => true;
        public override Task Execute(string? input) => Handler(input);
    }

    public sealed record NoInputAction(string Description, Func<Task> Handler)
        : AppCommand(Description)
    {
        public override Task Execute(string? _) => Handler();
    }
}
