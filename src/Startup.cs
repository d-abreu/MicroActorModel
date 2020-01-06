using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class Startup
{
    public static void Main()
    {
        var actorSystem = new ActorSystem();
        var ceo = actorSystem.CreateActor<Ceo>();

        Parallel.For(0,31,(i) => {
            ceo.Tell("msgNr. " + i);
        });
        var ceo2 = actorSystem.CreateActor<Ceo>();

        Parallel.For(0,31,(i) => {
            ceo2.Tell("msgNr. " + i);
        });

        var ceo3 = actorSystem.CreateActor<Ceo>();

        Parallel.For(0,31,(i) => {
            ceo3.Tell("msgNr. " + i);
        });

        Console.WriteLine("Click ENTER to close");
        Console.ReadLine();
    }
}


