using Microsoft.Extensions.Hosting;

namespace Infrastructure.Messaging.Implementation.RabbitMQ
{
    public delegate void MessageHandler<TData>(EventMessage<TData> data);
    public class QueueConsumerService<TData> : BackgroundService
    {
        private readonly MessageHandler<TData> _handler;
        private readonly IMessageSubscriber _consumer;
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _consumer.AddListener((_, args) =>
            {
                var body = args.Body.ToArray();
                var data = SerializationHelper.DeserializeMessage<TData>(body, typeof(EventMessage<TData>));
                _handler?.Invoke(data);
                //_listView1.Invoke(new Action(() => UpdateListView(data)));
            });

            return Task.CompletedTask;
        }

        public QueueConsumerService(IMessageSubscriber consumer, MessageHandler<TData> handler)
        {
            _handler = handler;
            _consumer = consumer;
        }



    }

}
