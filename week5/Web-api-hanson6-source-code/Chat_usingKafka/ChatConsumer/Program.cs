using System;
using Confluent.Kafka;

class Program
{
    static void Main()
    {
        var config = new ConsumerConfig
        {
            BootstrapServers = "localhost:9092",
            GroupId = "chat-consumer-group",
            AutoOffsetReset = AutoOffsetReset.Earliest
        };

        using var consumer = new ConsumerBuilder<Ignore, string>(config).Build();
        consumer.Subscribe("chat-topic");

        Console.WriteLine("Kafka Chat Consumer Started. Waiting for messages...");

        while (true)
        {
            var consumeResult = consumer.Consume();
            Console.WriteLine($"Friend: {consumeResult.Message.Value}");
        }
    }
}
