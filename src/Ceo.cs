using System;

public class Ceo : Actor
{
    private Lazy<IActor> Junior { get; }
    public Ceo()
    {
        Junior = new Lazy<IActor>(() => this.CreateActor<Junior>());
        Handles<string>(obj =>
        {
            Junior.Value.Tell(obj);
        });
        Handles<Exception>(ex => Console.WriteLine($"Failed {ex}"));
    }
}

