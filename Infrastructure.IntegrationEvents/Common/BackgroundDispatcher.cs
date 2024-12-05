using Infrastructure.IntegrationEvents.Entities;

namespace Infrastructure.IntegrationEvents.Common
{
    public abstract class BackgroundDispatcher<TData> : IDisposable
    {
        private readonly Queue<Guid> _queue = new();
        private readonly AutoResetEvent _itemAddedEvent = new(false);
        private readonly List<Thread> _workerThreads = new();
        private readonly object _lock = new();
        private bool _isRunning = true;
        private bool _disposedValue;

        private async void Consume()
        {
            while (true)
            {
                // Wait for a signal
                _itemAddedEvent.WaitOne();
                Guid item;

                lock (_lock)
                {
                    if (_queue.Count == 0 && !_isRunning)
                        return;
                    if (_queue.Count == 0)
                        continue; // Handle spurious wakeups
                    item = _queue.Dequeue();
                }
                DispatchData(item).Wait();
                // Process the item outside the lock
                Console.WriteLine($"Consumed: {item} on Thread {Thread.CurrentThread.ManagedThreadId}");
            }
        }

        protected abstract Task<bool> DispatchData(Guid transactionId);

        public BackgroundDispatcher(int threadCount = 2)
        {
            for (int i = 0; i < threadCount; i++)
            {
                var workerThread = new Thread(Consume);
                _workerThreads.Add(workerThread);
            }
        }

        public void Start()
        {
            foreach (var thread in _workerThreads)
            {
                thread.Start();
            }
        }

        public void AddData(Guid data)
        {
            lock (_lock)
            {
                _queue.Enqueue(data);
            }
            _itemAddedEvent.Set(); // Signal that an item has been added
        }



        public void Stop()
        {
            const int TIME_OUT = 5000;
            lock (_lock)
            {
                _isRunning = false;
            }

            // Wake up all waiting threads
            foreach (var _ in _workerThreads)
            {
                _itemAddedEvent.Set();
            }

            foreach (var thread in _workerThreads)
            {
                //Max Wait Time to exit thread
                thread.Join(TIME_OUT);
            }
        }

        #region IDisposible Implementation
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    Stop();
                    _workerThreads.Clear();
                }
                _disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
        }
        #endregion IDsiposible Implementation
    }
}
