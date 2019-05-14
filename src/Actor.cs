using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

public abstract class Actor : IActor
{
    private Action<object> onReceiving { get; set; } = (msg) => { };

    internal BlockingCollection<object> Mailbox { get; } = new BlockingCollection<object>();
    public Actor()
    {
        StartReceiving();
    }

    protected void Handles(Action<object> messageHandler)
    {
        onReceiving = messageHandler;
    }

    private void StartReceiving()
    {
        Task.Run(() =>
        {
            foreach (var msg in Mailbox.GetConsumingEnumerable())
            {
                onReceiving(msg);
            }
        });
    }

    public void Tell<T>(T message) where T : class
    {
        Mailbox.Add(message);
    }
}