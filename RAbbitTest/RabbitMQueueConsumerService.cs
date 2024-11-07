using Microsoft.Extensions.Hosting;
using System.Text.Json;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RAbbitTest
{
    public class RabbitMQueueConsumerService : BackgroundService
    {
        private readonly ListView _listView1;
        private readonly RabbitMQueueSubscriber _consumer;
        private JsonSerializerOptions _options = new JsonSerializerOptions { WriteIndented = false };
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _consumer.AddListener((_, args) =>
            {
                var body = args.Body.ToArray();
                var data = DeserializeMessage(body, typeof(TestEventMessage)) as TestEventMessage;

                _listView1.Invoke(new Action(() => UpdateListView(data)));
            });

            return Task.CompletedTask;
        }

        public RabbitMQueueConsumerService(RabbitMQueueSubscriber consumer, ListView lv)
        {
            _listView1 = lv;
            _consumer = consumer;
        }

        private EventMessage DeserializeMessage(byte[] data, Type eventType)
        {
            return JsonSerializer.Deserialize(data, eventType, _options) as EventMessage;
        }

        //Temporary
        private void UpdateListView(TestEventMessage data)
        {
            if (_listView1.InvokeRequired)
            {
                // Invoke on the UI thread if called from a background thread
                _listView1.Invoke(new Action(() => UpdateListView(data)));
            }
            else
            {
                var result = $"Event ID: {data.Id}\nCreated On: {data.CreationDate}\nData: {data.Data}";
                // Add the new item to the ListView
                _listView1.Items.Add(new ListViewItem(result));
            }
        }
    }

}
