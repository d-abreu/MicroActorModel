using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

public abstract class Actor : IActor
{
    private Dictionary<Type, dynamic> TypeHandlers {get;} = new Dictionary<Type, dynamic>();
    internal IActor Parent { get; set; }
    internal ActorSystem System { get; set; }
    internal BlockingCollection<dynamic> Mailbox { get; } = new BlockingCollection<dynamic>();
    public Actor()
    {
        StartReceiving();
    }

    protected IActor CreateActor<T>() where T: Actor
    {
        return System.CreateChildActor(typeof(T),this);
    }

    protected void Handles<T>(Action<T> messageHandler) where T: class
    {
        var tst = (dynamic)messageHandler;
        this.TypeHandlers.Add(typeof(T), tst);
    }

    private void StartReceiving()
    {
        Task.Run(() =>
        {
            foreach (var msg in Mailbox.GetConsumingEnumerable())
            {
                try
                {
                    TypeHandlers[msg.GetType()](msg);
                }
                catch (System.Exception ex)
                {
                    Parent.Tell(ex);
                }
            }
        });
    }

    public void Tell<T>(T message) where T : class
    {
        Mailbox.Add(message);
    }
}