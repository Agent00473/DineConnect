using System.Diagnostics;
using System.Threading;

namespace Infrastructure.Messaging.Common
{
    /// <summary>
    /// Base class for processing jobs in a queued order with configurable worker threads.
    /// </summary>
    /// <typeparam name="TData"></typeparam>
    public abstract class QueuedDataProcessor<TData> : IDisposable, IQueuedDataProcessor<TData>
    {
        private readonly Queue<TData> _queue = new();
        private readonly AutoResetEvent _itemAddedEvent = new(false);
        private readonly List<Thread> _workerThreads = new();
        private readonly object _lock = new();
        private bool _isRunning = true;
        private bool _isStarted = false;

        private bool _disposedValue;
        private ManualResetEvent _pauseEvent = new ManualResetEvent(true); // Initially set to true (not paused)

        private async Task Consume()
        {
            while (true)
            {
                _pauseEvent.WaitOne();
                // Wait for a signal
                Debug.WriteLine($"_pauseEvent.WaitOne() Queue Count = {_queue.Count}");
                _itemAddedEvent.WaitOne();
                Debug.WriteLine($"_itemAddedEvent.WaitOne() Queue Count = {_queue.Count}");

                TData item;

                lock (_lock)
                {
                    Debug.WriteLine($"Queue Count = {_queue.Count}");

                    if (_queue.Count == 0 && !_isRunning)
                        return;
                    if (_queue.Count == 0)
                        continue; // Handle spurious wakeups
                    item = _queue.Dequeue();
                }
                await ProcessData(item);
                // Process the item outside the lock
                Debug.WriteLine($"Consumed: {item} on Thread {Thread.CurrentThread.ManagedThreadId}");
            }
        }

        private void StartConsuming()
        {
            Consume().GetAwaiter().GetResult();
        }

        protected abstract Task<bool> ProcessData(TData data);

        public QueuedDataProcessor(int threadCount = 2)
        {
            for (int i = 0; i < threadCount; i++)
            {
                var workerThread = new Thread(StartConsuming);
                _workerThreads.Add(workerThread);
            }
        }


        public virtual void Start()
        {
            if (!_isStarted)
            {
                foreach (var thread in _workerThreads)
                {
                    thread.Start();
                }
            }
            else
            {
                _itemAddedEvent.Set();
            }
            _isStarted = true;
            _pauseEvent.Set();
        }
        public virtual void Pause()
        {
            _pauseEvent.Reset();
        }

        public void AddData(TData data)
        {
            lock (_lock)
            {
                _queue.Enqueue(data);
            }
            _itemAddedEvent.Set(); // Signal that an item has been added
        }

        public virtual void Stop()
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
