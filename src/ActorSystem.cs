using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

internal class ActorSystem
{
    private Dictionary<Actor, BlockingCollection<object>> Mailboxes { get; }
    = new Dictionary<Actor, BlockingCollection<object>>();

    public ActorSystem()
    {

    }

    public IActor CreateActor<T>() where T : Actor
    {
        var actor = Activator.CreateInstance<T>();
        Mailboxes.Add(actor, actor.Mailbox);
        return actor;
    }
}
