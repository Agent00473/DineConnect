using Infrastructure.IntegrationEvents;
using Infrastructure.IntegrationEvents.Database;
using Infrastructure.IntegrationEvents.Entities;
using Infrastructure.IntegrationEvents.EventHandlers;
using Infrastructure.Messaging;
using Infrastructure.Messaging.Entities;
using Infrastructure.Messaging.Implementation.RabbitMQ;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace RAbbitTest
{
    public partial class Form1 : Form
    {
        private QueueConfiguration _rabbitMSampleQConfig = new QueueConfiguration("TestExchange", ExchangeType.Direct, "SampleQueue", "Sample.Test", false, true);
        private RabbitMQConfigurationManager _rabbitMQConfigurationManager;

        private QueueConsumerService<StringDataMessage> _rabbitMSampleQueueConsumerService;
        private RabbitMQueuePublisher _rabbitMSampleQueuePublisher;
        private RabbitMQueueSubscriber _rabbitMSampleQueueSubscriber;
       

        public Form1()
        {
            InitializeComponent();
            listView1.Scrollable = true;
            listView1.FullRowSelect = true;
            listView1.Columns[0].Width = listView1.ClientSize.Width;
            var configs = RabbitMQConfigLoader.LoadFromXml(".\\App.config");
            _rabbitMQConfigurationManager = new RabbitMQConfigurationManager(configs);
            _rabbitMQConfigurationManager.AddQueue(_rabbitMSampleQConfig);
            _rabbitMQConfigurationManager.AddQueue(_rabbitMCustomerQConfig);


        }
        //private void ConfigureServices(IServiceCollection services)
        //{
        //    _rabbitMQConfigurationManager.
        //    services.Configure<RabbitMQSettings>(ConfigurationManager.GetSection("QueueConfigurations"));
        //    services.AddSingleton<RabbitMQConfigurationManager>();
        //}

        private void button1_Click(object sender, EventArgs e)
        {
            _rabbitMSampleQueuePublisher = RabbitMQueuePublisher.Create(_rabbitMSampleQConfig.ExchangeName, _rabbitMQConfigurationManager);

            _rabbitMSampleQueueSubscriber = RabbitMQueueSubscriber.Create(_rabbitMSampleQConfig.QueueName, _rabbitMQConfigurationManager);
            _rabbitMSampleQueueConsumerService = new QueueConsumerService<StringDataMessage>(_rabbitMSampleQueueSubscriber, UpdateListViewStringMessage);

            btnRabbitConsume.Enabled = true;
            btnRabbitPublish.Enabled = true;
            btnRabbitStop.Enabled = true;
            btnRabbitConfigure.Enabled = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            _rabbitMSampleQueueConsumerService.StartAsync(new CancellationToken());
        }



        private void button5_Click(object sender, EventArgs e)
        {
            _rabbitMSampleQueueConsumerService.StopAsync(new CancellationToken());
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
            var msg = textBox1.Text == string.Empty ? GenerateRandomString(10) : textBox1.Text;
            var testEvent = new StringDataMessage(msg);

            var result = _rabbitMSampleQueuePublisher.SendMessage(_rabbitMSampleQConfig.RoutingKey, testEvent);
            listView1.Items.Add(result.ToString());

        }

        private void button3_Click(object sender, EventArgs e)
        {
            listView1.Items.Add("Shine");
            listView1.Items.Add("Babu");

        }
        private void UpdateListViewStringMessage(EventMessage data)
        {
            if (listView1.InvokeRequired)
            {
                // Invoke on the UI thread if called from a background thread
                listView1.Invoke(new Action(() => UpdateListViewStringMessage(data)));
            }
            else
            {
                var result = $"Event ID: {data.Id}\n Created On: {data.CreationDate}";
                // Add the new item to the ListView
                listView1.Items.Add(new ListViewItem(result));
                result = $"Data: {((StringDataMessage)data).Data}";
                // Add the new item to the ListView
                listView1.Items.Add(new ListViewItem(result));

            }

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private IntegrationEventDataContext _context;
        private IEventPublisher _customerIntegrationEventDispatcher;
        private RabbitMQueuePublisher _rabbitMQueueCustEventPublisher;

        private RabbitMQueueSubscriber _rabbitMQueueCustEventConsumer;
        private QueueConsumerService<CustomerEvent> _rabbitMQueueCustEventConsumerService;
        private QueueConfiguration _rabbitMCustomerQConfig = new QueueConfiguration("TestCustomerExchange", ExchangeType.Direct, "CustomerQueue", "Customer.Test", false, true);

        private void UpdateListView(EventMessage data)
        {
            if (listView1.InvokeRequired)
            {
                // Invoke on the UI thread if called from a background thread
                listView1.Invoke(new Action(() => UpdateListView(data)));
            }
            else
            {
                var result = $"Event ID: {data.Id}\nCreated On: {data.CreationDate}\n DataType : {data.GetType().Name}";
                listView1.Items.Add(new ListViewItem(result));
                //CustomerEvent obj = data as CustomerEvent;
                //result = $" Name: {obj.Name} \n email : {obj.Email}";
                //listView1.Items.Add(new ListViewItem(result));
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
            _service = new IntegrationEventManagerService(_context);
            //Publisher
            _rabbitMQueueCustEventPublisher = RabbitMQueuePublisher.Create(_rabbitMCustomerQConfig.ExchangeName, _rabbitMQConfigurationManager);
            _customerIntegrationEventDispatcher = IntegrationEventDispatcher.Create(_context, _rabbitMQueueCustEventPublisher, _rabbitMQConfigurationManager);

            ///Subscriber
            _rabbitMQueueCustEventConsumer = RabbitMQueueSubscriber.Create(_rabbitMCustomerQConfig.QueueName, _rabbitMQConfigurationManager);
            _rabbitMQueueCustEventConsumerService = new QueueConsumerService<CustomerEvent>(_rabbitMQueueCustEventConsumer, UpdateListView);

            button2.Enabled = true;
            button3.Enabled = true;
            button4.Enabled = true;
            button5.Enabled = true;
        }

        IntegrationEventManagerService _service;
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
                    listView1.Items.Add(new ListViewItem($"{item.CreationTime} :: {item.EventId} :: {item.IntegrationEvent.CreationDate}"));
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
                    var result = await _customerIntegrationEventDispatcher.Publish<CustomerEvent>(id);
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
                _rabbitMQueueCustEventConsumerService.StartAsync(new CancellationToken());
            }
            finally
            {
                button5.Enabled = true;
            }
        }
    }
}
