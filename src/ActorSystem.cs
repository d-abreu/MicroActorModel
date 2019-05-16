using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

internal class ActorSystem
{
    private Dictionary<Actor, BlockingCollection<object>> Mailboxes { get; }
    = new Dictionary<Actor, BlockingCollection<object>>();
    internal IActor Root { get; }
    internal IActor UnhandledMessages { get;}
    public ActorSystem()
    {
        Root = CreateActor<RootActor>();
        UnhandledMessages = CreateChildActor(typeof(UnhandledMessagesActor), Root);
    }

    public IActor CreateActor<T>() where T : Actor
    {
        return CreateChildActor(typeof(T), Root);
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

    private class UnhandledMessagesActor : Actor
    {
        public UnhandledMessagesActor()
        {
            Handles<UnhandledMessage>(obj => Console.WriteLine($"Unhandled message: {obj.Message}"));
        }
    }

    internal class UnhandledMessage
    {
        public object Message {get;set;}
    }
}
