using RabbitMQ.Client;

namespace Infrastructure.Messaging.Implementation.RabbitMQ
{
    public abstract class RabbitMQueueBase : IDisposable, IMessageServiceBase
    {
        #region Private & Protected Fields
        private readonly IConnection _connection;
        private IModel _channel;
        private bool disposedValue;
        protected bool _initialized = false;
        protected string _exchangeName = string.Empty;
        #endregion

        #region Private & Protected Methods

        #endregion

        #region Constructors
        protected RabbitMQueueBase(IConnection connection)
        {
            _connection = connection;
        }
        #endregion

        #region Public & Protected Properties
        protected IModel Channel
        {
            get
            {
                if (_channel == null)
                {
                    _channel = _connection.CreateModel();
                    _channel.ConfirmSelect();
                }

                return _channel;
            }
        }
        #endregion

        #region Public Methods
        #endregion

        #region IDispose Implementations
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _channel?.Dispose();
                }
                disposedValue = true;
            }
        }
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        ~RabbitMQueueBase()
        {
            Dispose(disposing: false);
        }
        #endregion
    }
}

