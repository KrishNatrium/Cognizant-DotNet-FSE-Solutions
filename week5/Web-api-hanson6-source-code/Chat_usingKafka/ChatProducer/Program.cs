using System;
using System.Threading.Tasks;
using Confluent.Kafka;

class Program
{
    static async Task Main()
    {
        var config = new ProducerConfig { BootstrapServers = "localhost:9092" };
        using var producer = new ProducerBuilder<Null, string>(config).Build();

        string topic = "chat-topic";

        Console.WriteLine("Kafka Chat Producer Started. Type messages and press Enter. Type 'exit' to quit.");

        while (true)
        {
            Console.Write("You: ");
            var message = Console.ReadLine();
            if (message == null || message.Trim().ToLower() == "exit") break;

            try
            {
                var dr = await producer.ProduceAsync(topic, new Message<Null, string> { Value = message });
                Console.WriteLine($"Delivered '{message}' to '{dr.TopicPartitionOffset}'");
            }
            catch (ProduceException<Null, string> e)
            {
                Console.WriteLine($"Delivery failed: {e.Error.Reason}");
            }
        }
    }
}
