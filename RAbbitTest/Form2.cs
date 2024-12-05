using Infrastructure.IntegrationEvents;
using Infrastructure.IntegrationEvents.Common;
using Infrastructure.IntegrationEvents.Database;
using Infrastructure.IntegrationEvents.Entities;
using Infrastructure.Messaging;
using Infrastructure.Messaging.Entities;
using Infrastructure.Messaging.Implementation.RabbitMQ;
using InfraTest.Events;


namespace InfraTest
{
    public partial class Form2 : Form
    {
        private QueuedEventDispatcher _eventDispatcher;
        private IRabbitMQConfigurationManager _rabbitMQConfigurationManager;
        public Form2()
        {
            InitializeComponent();
            listView1.Scrollable = true;
            listView1.FullRowSelect = true;
            listView1.Columns[0].Width = listView1.ClientSize.Width;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            var configs = RabbitMQConfigLoader.LoadFromXml(".\\App.config");
            _rabbitMQConfigurationManager = new RabbitMQConfigurationManager(configs);
            _rabbitMQConfigurationManager.Initialize();
            _eventDispatcher = QueuedEventDispatcher.Create(connectionString, "IntegrationExchange", _rabbitMQConfigurationManager);


            _rabbitMQueueCustEventConsumer = RabbitMQueueSubscriber.Create("CustomerQueue", _rabbitMQConfigurationManager);
            _rabbitMQueueCustEventConsumerService = new QueueConsumerService<CustomerEvent>(_rabbitMQueueCustEventConsumer, UpdateListView);

            _rabbitMQueueOrderEventConsumer = RabbitMQueueSubscriber.Create("OrderQueue", _rabbitMQConfigurationManager);
            _rabbitMQueueOrderEventConsumerService = new QueueConsumerService<OrderEvent>(_rabbitMQueueOrderEventConsumer, UpdateOrderListView);

            _rabbitMQueuePulseConsumer = RabbitMQueueSubscriber.Create("HeartBeatQueue", _rabbitMQConfigurationManager);
            _rabbitMQueuePulseEventConsumerService = new QueueConsumerService<HeartBeatEvent>(_rabbitMQueuePulseConsumer, UpdatePulseListView);


            _context = new IntegrationEventDataContext(connectionString);
            _service = new IntegrationEventManagerService(_context);

        }

        private void button11_Click(object sender, EventArgs e)
        {
            _eventDispatcher.Start();
            button10.Enabled = true;

        }

        private void button10_Click(object sender, EventArgs e)
        {
            button11.Enabled = true;
            button10.Enabled = false;
            _eventDispatcher.Stop();
        }

        private RabbitMQueueSubscriber _rabbitMQueueCustEventConsumer;
        private QueueConsumerService<CustomerEvent> _rabbitMQueueCustEventConsumerService;


        private RabbitMQueueSubscriber _rabbitMQueueOrderEventConsumer;
        private QueueConsumerService<OrderEvent> _rabbitMQueueOrderEventConsumerService;

        //HeartBeatEvent
        private RabbitMQueueSubscriber _rabbitMQueuePulseConsumer;
        private QueueConsumerService<HeartBeatEvent> _rabbitMQueuePulseEventConsumerService;

        private void button9_Click(object sender, EventArgs e)
        {
            try
            {
                button9.Enabled = false;
                _rabbitMQueueCustEventConsumerService.StartAsync(new CancellationToken());
                _rabbitMQueueOrderEventConsumerService.StartAsync(new CancellationToken());
                _rabbitMQueuePulseEventConsumerService.StartAsync(new CancellationToken());

            }
            finally
            {
                button9.Enabled = true;
            }
        }


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

                CustomerEvent obj = data as CustomerEvent;
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
                var lv = new ListViewItem(result);
                listView1.Items.Add(lv);
            }
        }

        private void UpdatePulseListView(EventMessage data)
        {
            if (listView1.InvokeRequired)
            {
                // Invoke on the UI thread if called from a background thread
                listView1.Invoke(new Action(() => UpdatePulseListView(data)));
            }
            else
            {
                var result = $"Pulse Event ID: {data.Id}\nCreated On: {data.CreationDate}\n DataType : {data.GetType().Name}";
                listView1.Items.Add(new ListViewItem(result));

                var obj = data as HeartBeatEvent;
                result = $" Message: {obj.Message}";
                var lv = new ListViewItem(result);
                lv.BackColor = Color.LightBlue;
                lv.Font = new Font(lv.Font, FontStyle.Bold | FontStyle.Italic);
                listView1.Items.Add(new ListViewItem(result));
                listView1.Items.Add(lv);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    button1.Enabled = false;
            //    _rabbitMQueueCustEventConsumerService.StopAsync(new CancellationToken());
            //    _rabbitMQueueOrderEventConsumerService.StopAsync(new CancellationToken());
            //}
            //finally
            //{
            //    button1.Enabled = true;
            //}

        }
        private IntegrationEventDataContext _context;
        IntegrationEventManagerService _service;

        private async void button2_Click(object sender, EventArgs e)
        {
            try
            {
                button2.Enabled = false;
                var trnasaction = _context.Database.BeginTransaction();
                var events = EventGenerator.GenerateRandomEvents();

                foreach (var item in events)
                {
                    await _service.SaveIntegrationEventAsync(item, trnasaction);
                }
                var tid = trnasaction.TransactionId;
                trnasaction.Commit();
                _eventDispatcher.AddData(tid);
            }
            finally
            {
                button2.Enabled = true;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            listView1.Clear();
        }
    }
}
