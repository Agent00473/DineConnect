using Infrastructure.Messaging;
using Infrastructure.Messaging.Implementation.RabbitMQ;
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

            _rabbitMQueuePublisher = RabbitMQueuePublisher<string>.Create();
            _rabbitMQueueSubscriber = RabbitMQueueSubscriber.Create();
            _rabbitMQueueConsumerService = new QueueConsumerService<string>(_rabbitMQueueSubscriber, UpdateListView);
        }

        private void button1_Click(object sender, EventArgs e)
        {
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
            _rabbitMQueuePublisher.SendMessage(_rabbitMQConfig.RoutingKeys[0], testEvent);
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
    }


}
