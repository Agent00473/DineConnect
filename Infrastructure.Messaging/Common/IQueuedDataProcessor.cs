namespace Infrastructure.Messaging.Common
{
    public interface IQueuedDataProcessor<TData>
    {
        void AddData(TData data);
        void Dispose();
        void Pause();
        void Start();
        void Stop();
    }
}