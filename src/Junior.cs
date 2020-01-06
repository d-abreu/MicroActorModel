using System;

public class Junior : Actor
{
    public Junior()
    {
        Handles<string>(obj =>
        {
            Console.WriteLine($"{nameof(Junior)} received {obj}");
        });
    }
}


