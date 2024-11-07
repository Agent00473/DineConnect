using RabbitMQ.Client;

namespace RAbbitTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            var factory = new ConnectionFactory
            {
                HostName = "localhost"
            };
            _rabbitMQueuePublisher = new RabbitMQueuePublisher(factory.CreateConnection());
            _rabbitMQueueSubscriber = new RabbitMQueueSubscriber(factory.CreateConnection());
            _rabbitMQueueConsumerService = new RabbitMQueueConsumerService(_rabbitMQueueSubscriber, listView1);
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            _rabbitMQueueConsumerService.StartAsync(new CancellationToken());
        }

        private RabbitMQueueConsumerService _rabbitMQueueConsumerService;
        private RabbitMQueuePublisher _rabbitMQueuePublisher;
        private RabbitMQueueSubscriber _rabbitMQueueSubscriber;
        private RabbitMQConfig _rabbitMQConfig = new RabbitMQConfig("TestExchange", "Direct", "SampleQueue", ["Sample.Test"]);

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
            var testEvent = new TestEventMessage
            {
                Data = GenerateRandomString(10)
            };
            _rabbitMQueuePublisher.SendMessage(_rabbitMQConfig.QueueName, testEvent);
        }
    }


}
