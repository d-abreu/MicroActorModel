using System;
using System.Collections.Generic;

public class Startup
{

    public static void Main()
    {
        var actorSystem = new ActorSystem();
        var ceo = actorSystem.CreateActor<Ceo>();

        for (int i = 0; i <= 20; i++)
        {
            ceo.Tell("msgNr. " + i);
        }

        Console.WriteLine("Click ENTER to close");
        Console.ReadLine();
    }
}

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

public class Junior : Actor
{
    public Junior()
    {
        Handles<string>(obj =>
        {
            if(obj.Contains("13"))
                throw new Exception("Junior failed");
            Console.WriteLine($"{this.GetType().Name} received {obj}");
        });
    }
}


