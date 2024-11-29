using Infrastructure.IntegrationEvents;
using Infrastructure.IntegrationEvents.Database;
using Infrastructure.Messaging;
using Infrastructure.Messaging.Implementation.RabbitMQ;
using InfraTest.Events;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace RAbbitTest
{
    public partial class Form1 : Form
    {
        private QueueConsumerService<string> _rabbitMQueueConsumerService;
        private RabbitMQueuePublisher<string> _rabbitMQueuePublisher;
        private RabbitMQueueSubscriber _rabbitMQueueSubscriber;
        private QueueConfiguration _rabbitMQConfig = new QueueConfiguration("TestExchange", ExchangeType.Direct, "SampleQueue", ["Sample.Test"]);
        public Form1()
        {
            InitializeComponent();
            listView1.Scrollable = true;
            listView1.FullRowSelect = true;
            listView1.Columns[0].Width = listView1.ClientSize.Width;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _rabbitMQueuePublisher = RabbitMQueuePublisher<string>.Create();
            _rabbitMQueueSubscriber = RabbitMQueueSubscriber.Create();
            _rabbitMQueueConsumerService = new QueueConsumerService<string>(_rabbitMQueueSubscriber, UpdateListView);

            _rabbitMQueuePublisher.Configure(_rabbitMQConfig);
            _rabbitMQueueSubscriber.Configure(_rabbitMQConfig);
            btnRabbitConsume.Enabled = true;
            btnRabbitPublish.Enabled = true;
            btnRabbitStop.Enabled = true;
            btnRabbitConfigure.Enabled = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            _rabbitMQueueConsumerService.StartAsync(new CancellationToken());
        }



        private void button5_Click(object sender, EventArgs e)
        {
            _rabbitMQueueConsumerService.StopAsync(new CancellationToken());
        }
        public static string GenerateRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        private void button2_Click(object sender, EventArgs e)
        {

            var testEvent = new EventMessage<string>
            {
                Data = textBox1.Text == string.Empty ? GenerateRandomString(10) : textBox1.Text

            };
            var result = _rabbitMQueuePublisher.SendMessage(_rabbitMQConfig.RoutingKeys[0], testEvent);
            listView1.Items.Add(result.ToString());

        }

        private void button3_Click(object sender, EventArgs e)
        {
            listView1.Items.Add("Shine");
            listView1.Items.Add("Babu");

        }
        private void UpdateListView(EventMessage<string> data)
        {
            if (listView1.InvokeRequired)
            {
                // Invoke on the UI thread if called from a background thread
                listView1.Invoke(new Action(() => UpdateListView(data)));
            }
            else
            {
                var result = $"Event ID: {data.Id}\nCreated On: {data.CreationDate}\nData: {data.Data}";
                // Add the new item to the ListView
                listView1.Items.Add(new ListViewItem(result));
            }
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private IntegrationEventDataContext _context;
        private IEventPublisher _eventPublisher;
        private RabbitMQueuePublisher<CustomerEvent> _rabbitMQueueEventPublisher;

        private RabbitMQueueSubscriber _rabbitMQueueEventConsumer;
        private QueueConsumerService<CustomerEvent> _rabbitMQueueEventConsumerService;


        private void UpdateListView(EventMessage<CustomerEvent> data)
        {
            if (listView1.InvokeRequired)
            {
                // Invoke on the UI thread if called from a background thread
                listView1.Invoke(new Action(() => UpdateListView(data)));
            }
            else
            {
                var result = $"Event ID: {data.Id}\nCreated On: {data.CreationDate}\n";
                listView1.Items.Add(new ListViewItem(result));
                result = $" Name: {data.Data.Name} \n email : {data.Data.Email}";
                listView1.Items.Add(new ListViewItem(result));
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            var optionsBuilder = new DbContextOptionsBuilder<IntegrationEventDataContext>();
            optionsBuilder.UseNpgsql(connectionString)
             .LogTo(Console.WriteLine, LogLevel.Information);

            _context = new IntegrationEventDataContext(optionsBuilder.Options, null);
            // _context.Database.EnsureCreated();


            //var resut = _context.EventDetails.Count().ToString();
            //listView1.Items.Add(new ListViewItem($"Event Count : {resut}"));
            _service = new IntegrationEventCommandService(_context);
            //Publisher
            _rabbitMQueueEventPublisher = RabbitMQueuePublisher<CustomerEvent>.Create();
            _rabbitMQueueEventPublisher.Configure(_rabbitMQConfig);
            _eventPublisher = new EventPublisher<CustomerEvent>(_context, _rabbitMQueueEventPublisher);

            ///Subscriber
            _rabbitMQueueEventConsumer = RabbitMQueueSubscriber.Create();
            _rabbitMQueueEventConsumer.Configure(_rabbitMQConfig);
            _rabbitMQueueEventConsumerService = new QueueConsumerService<CustomerEvent>(_rabbitMQueueEventConsumer, UpdateListView);

            button2.Enabled = true;
            button3.Enabled = true;
            button4.Enabled = true;
            button5.Enabled = true;
        }

        IntegrationEventCommandService _service;
        private async void button2_Click_1(object sender, EventArgs e)
        {
            try
            {
                button2.Enabled = false;
                var trnasaction = _context.Database.BeginTransaction();
                var data = new CustomerEvent(Guid.NewGuid(), "Joe Doe", "JD@unknown.com", EventActionCategory.Created);
                await _service.SaveIntegrationEventAsync(data, trnasaction);
                data = new CustomerEvent(Guid.NewGuid(), "Mary Doe", "MD@unknown.com", EventActionCategory.Updated);
                await _service.SaveIntegrationEventAsync(data, trnasaction);
                var tid = trnasaction.TransactionId;
                trnasaction.Commit();
            }
            finally
            {
                button2.Enabled = true;
            }
        }

        private async void button3_Click_1(object sender, EventArgs e)
        {
            try
            {
                button3.Enabled = false;
                var result = await _service.RetrieveAllPendingEventLogsToPublishAsync();
                foreach (var item in result.ToList())
                {
                    listView1.Items.Add(new ListViewItem($"{item.CreationTime} :: {item.IntegrationEvent.Id} :: {item.IntegrationEvent.CreationDate}"));
                }

            }
            finally
            {
                button3.Enabled = true;
            }
        }

        private async void button4_Click_1(object sender, EventArgs e)
        {
            try
            {
                button4.Enabled = false;
                var data = await _service.RetrieveAllPendingEventLogsToPublishAsync();
                var tids = data.Select(x => x.TransactionId).ToList();

                foreach (var id in tids)
                {
                    var result = await _eventPublisher.Publish(id);
                    listView1.Items.Add(new ListViewItem($" Publish ID {id} Status : {result.ToString()}"));
                }
                listView1.Items.Add("--------Publish Done-----------");
            }
            finally
            {
                button4.Enabled = true;
            }
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            try
            {
                button5.Enabled = false;
                _rabbitMQueueEventConsumerService.StartAsync(new CancellationToken());
            }
            finally
            {
                button5.Enabled = true;
            }
        }
    }
}
