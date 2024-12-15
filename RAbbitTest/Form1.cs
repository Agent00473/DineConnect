using Infrastructure.IntegrationEvents.DataAccess;
using Infrastructure.IntegrationEvents.DataAccess.Commands;
using Infrastructure.IntegrationEvents.DataAccess.Queries;
using Infrastructure.IntegrationEvents.Entities.Events;
using Infrastructure.IntegrationEvents.EventHandlers;
using Infrastructure.IntegrationEvents.EventHandlers.Implementations;
using Infrastructure.Messaging;
using Infrastructure.Messaging.Common;
using Infrastructure.Messaging.Entities;
using Infrastructure.Messaging.Implementation.RabbitMQ;
using InfraTest;
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
            //_rabbitMQConfigurationManager.Initialize();
            //_rabbitMQConfigurationManager.AddQueue(_rabbitMSampleQConfig);
            //_rabbitMQConfigurationManager.AddQueue(_rabbitMCustomerQConfig);


        }
        //private void ConfigureServices(IServiceCollection services)
        //{
        //    _rabbitMQConfigurationManager.
        //    services.Configure<RabbitMQSettings>(ConfigurationManager.GetSection("QueueConfigurations"));
        //    services.AddSingleton<RabbitMQConfigurationManager>();
        //}

        private void button1_Click(object sender, EventArgs e)
        {
            _rabbitMSampleQueuePublisher = RabbitMQueuePublisher.Create(_rabbitMQConfigurationManager);
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
            var rData = new RouteData(_rabbitMSampleQConfig.ExchangeName, _rabbitMCustomerQConfig.RoutingKey, Infrastructure.Messaging.Implementation.RabbitMQ.Configs.ExchangeCategory.Integration);
            var result = _rabbitMSampleQueuePublisher.SendMessage(rData, testEvent);
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
        private QueueConsumerService<CustomerIntegrationEvent> _rabbitMQueueCustEventConsumerService;
        private QueueConfiguration _rabbitMCustomerQConfig = new QueueConfiguration("IntegrationExchange", ExchangeType.Direct, "CustomerQueue", "CustomerIntegrationEvent", false, true);


        private RabbitMQueueSubscriber _rabbitMQueueOrderEventConsumer;
        private QueueConsumerService<OrderEvent> _rabbitMQueueOrderEventConsumerService;
        private QueueConfiguration _rabbitMOrderQConfig = new QueueConfiguration("IntegrationExchange", ExchangeType.Direct, "OrderQueue", "OrderEvent", false, true);

        private void UpdateListView(EventMessage data)
        {
            if (listView1.InvokeRequired)
            {
                // Invoke on the UI thread if called from a background thread
                listView1.Invoke(new Action(() => UpdateListView(data)));
            }
            else
            {
                var result = $"Customer Event ID: {data.Id}\nCreated On: {data.CreationDate}\n DataType : {data.GetType().Name}";
                listView1.Items.Add(new ListViewItem(result));

                CustomerIntegrationEvent obj = data as CustomerIntegrationEvent;
                result = $" Customer Name: {obj.Name} \n email : {obj.Email}";
                listView1.Items.Add(new ListViewItem(result));
            }
        }

        private void UpdateOrderListView(EventMessage data)
        {
            if (listView1.InvokeRequired)
            {
                // Invoke on the UI thread if called from a background thread
                listView1.Invoke(new Action(() => UpdateOrderListView(data)));
            }
            else
            {
                var result = $"Order Event ID: {data.Id}\nCreated On: {data.CreationDate}\n DataType : {data.GetType().Name}";
                listView1.Items.Add(new ListViewItem(result));

                OrderEvent obj = data as OrderEvent;
                result = $" Name: {obj.Name} \n Category : {obj.Category}";
                listView1.Items.Add(new ListViewItem(result));
            }
        }

        public IntegrationEventPublisher CreateIntegrationEventPublisher(string connectionString, IMessagePublisher messagePublisher, IRabbitMQConfigurationManager configurationManager)
        {
            var qryHandler = IntegrationEventsQueryHandler.Create(connectionString);
            var addHandler = AddIntegrationEventCommandHandler.Create(connectionString);
            var dispatcher = IntegrationEventPublisher.Create(qryHandler, addHandler, messagePublisher, configurationManager, connectionString);

            return dispatcher;
        }
        private IntegrationEventsQueryHandler _service;
        private void button1_Click_1(object sender, EventArgs e)
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["IntegrationEvents"].ConnectionString;

            _context = new IntegrationEventDataContext(connectionString);
            // _context.Database.EnsureCreated();


            //var resut = _context.EventDetails.Count().ToString();
            //listView1.Items.Add(new ListViewItem($"Event Count : {resut}"));
            _service =  IntegrationEventsQueryHandler.Create(connectionString);
            //Publisher
            _rabbitMQueueCustEventPublisher = RabbitMQueuePublisher.Create(_rabbitMQConfigurationManager);
            _customerIntegrationEventDispatcher = CreateIntegrationEventPublisher(connectionString, _rabbitMQueueCustEventPublisher, _rabbitMQConfigurationManager);

            ///Subscriber
            _rabbitMQueueCustEventConsumer = RabbitMQueueSubscriber.Create(_rabbitMCustomerQConfig.QueueName, _rabbitMQConfigurationManager);
            _rabbitMQueueCustEventConsumerService = new QueueConsumerService<CustomerIntegrationEvent>(_rabbitMQueueCustEventConsumer, UpdateListView);

            ///Subscriber Order
            _rabbitMQueueOrderEventConsumer = RabbitMQueueSubscriber.Create(_rabbitMOrderQConfig.QueueName, _rabbitMQConfigurationManager);
            _rabbitMQueueOrderEventConsumerService = new QueueConsumerService<OrderEvent>(_rabbitMQueueOrderEventConsumer, UpdateOrderListView);


            button2.Enabled = true;
            button3.Enabled = true;
            button4.Enabled = true;
            button5.Enabled = true;
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            //try
            //{
            //    button2.Enabled = false;
            //    var trnasaction = _context.Database.BeginTransaction();
            //    var data = new CustomerIntegrationEvent(Guid.NewGuid(), "Joe Doe", "JD@unknown.com", EventActionCategory.Created);
            //    await _service.SaveIntegrationEventAsync(data, trnasaction);

            //    var orderId = Guid.NewGuid();
            //    Guid customerId = Guid.NewGuid();
            //    string orderName = "Deleted Order for Tablet";
            //    EventActionCategory category = EventActionCategory.Deleted;
            //    OrderEvent deletedOrderEvent = new OrderEvent(orderId, customerId, orderName, category);
            //    await _service.SaveIntegrationEventAsync(deletedOrderEvent, trnasaction);


            //    data = new CustomerIntegrationEvent(Guid.NewGuid(), "Mary Doe", "MD@unknown.com", EventActionCategory.Updated);
            //    await _service.SaveIntegrationEventAsync(data, trnasaction);

            //    orderId = Guid.NewGuid();
            //    customerId = Guid.NewGuid();
            //    orderName = "Created Order for Laptop";
            //    category = EventActionCategory.Created;
            //    OrderEvent evt = new OrderEvent(orderId, customerId, orderName, category);
            //    await _service.SaveIntegrationEventAsync(evt, trnasaction);

            //    var tid = trnasaction.TransactionId;
            //    trnasaction.Commit();
            //}
            //finally
            //{
            //    button2.Enabled = true;
            //}
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
                var tids = data.Select(x => x.TransactionId).Distinct().ToList();

                foreach (var id in tids)
                {
                    var result = await _customerIntegrationEventDispatcher.Publish<CustomerIntegrationEvent>(id);
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
                _rabbitMQueueOrderEventConsumerService.StartAsync(new CancellationToken());
            }
            finally
            {
                button5.Enabled = true;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                button6.Enabled = false;
                var frm = new Form2();
                frm.StartPosition = FormStartPosition.CenterParent;
                frm.ShowDialog();
            }
            finally
            {
                button6.Enabled = true;

            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
