using Confluent.Kafka;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KafkaChatApp
{
    public partial class Form1 : Form
    {
        private IProducer<Null, string> _producer;
        private CancellationTokenSource _cts = new CancellationTokenSource();
        private string topic = "chat-topic";
        private string bootstrapServers = "localhost:9092"; // or your Docker IP

        public Form1()
        {
            InitializeComponent();
            InitializeKafkaProducer();
            StartKafkaConsumer();
            SendButton.Click += SendButton_Click;

        }

        private void InitializeKafkaProducer()
        {
            var config = new ProducerConfig
            {
                BootstrapServers = bootstrapServers
            };

            _producer = new ProducerBuilder<Null, string>(config).Build();
        }

        private async void SendButton_Click(object sender, EventArgs e)
        {
            string message = $"{Environment.UserName}: {MessageTextBox.Text}";
            if (!string.IsNullOrWhiteSpace(message))
            {
                await _producer.ProduceAsync(topic, new Message<Null, string> { Value = message });
                MessageTextBox.Clear();
            }
        }

        private void StartKafkaConsumer()
        {
            Task.Run(() =>
            {
                var config = new ConsumerConfig
                {
                    BootstrapServers = bootstrapServers,
                    GroupId = Guid.NewGuid().ToString(), // random so each client gets all messages
                    AutoOffsetReset = AutoOffsetReset.Earliest
                };

                var consumer = new ConsumerBuilder<Ignore, string>(config).Build();
                consumer.Subscribe(topic);

                try
                {
                    while (!_cts.Token.IsCancellationRequested)
                    {
                        try
                        {
                            var cr = consumer.Consume(_cts.Token);
                            AppendChat(cr.Message.Value);
                        }
                        catch (ConsumeException ex)
                        {
                            AppendChat("Consume error: " + ex.Message);
                        }
                    }
                }
                catch (OperationCanceledException) { }
                finally
                {
                    consumer.Close();
                    consumer.Dispose();
                }
            });
        }

        private void AppendChat(string message)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<string>(AppendChat), new object[] { message });
                return;
            }
            ChatListBox.Items.Add(message); // Or use TextBox.AppendText
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            _cts.Cancel();
            base.OnFormClosing(e);
        }

    }
}
