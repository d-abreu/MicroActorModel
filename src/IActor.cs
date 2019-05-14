public interface IActor
{
    void Tell<T>(T message) where T: class;
}
