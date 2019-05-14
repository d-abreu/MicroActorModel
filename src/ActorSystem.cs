using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

internal class ActorSystem
{
    private Dictionary<Actor, BlockingCollection<object>> Mailboxes { get; }
    = new Dictionary<Actor, BlockingCollection<object>>();
    public IActor root { get; }

    public ActorSystem()
    {
        root = CreateActor<RootActor>();
    }

    public IActor CreateActor<T>() where T : Actor
    {
        return CreateChildActor(typeof(T), root);
    }
    internal IActor CreateChildActor(Type actorType, IActor parent)
    {
        var actor = (Actor)Activator.CreateInstance(actorType);
        actor.Parent = parent;
        actor.System = this;
        Mailboxes.Add(actor, actor.Mailbox);
        return actor;
    }

    private class RootActor : Actor
    {

    } 
}
