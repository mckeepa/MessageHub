namespace KafkaConsumer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using Confluent.Kafka;

    class Program
    {
        static void Main(string[] args)
        {
            var config = new ConsumerConfig
            {
                GroupId = "banana-consumers",
                BootstrapServers = "localhost:9092",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            using (var consumer = new ConsumerBuilder<Ignore, string>(config).Build())
            {
                consumer.Subscribe("banana-topic");

                CancellationTokenSource cts = new CancellationTokenSource();
                Console.CancelKeyPress += (_, e) => {
                    e.Cancel = true;
                    cts.Cancel();
                };

                try
                {
                    while (true) 
                    {
                        try 
                        {
                            var cr = consumer.Consume(cts.Token);

                            foreach (IHeader header in cr.Message.Headers)
                            {
                                Console.WriteLine(header.Key + ":" + header.GetValueBytes());
                            }
                            
                            Console.WriteLine(cr.Message.Value);
                            
                        }
                        catch ( ConsumeException e)
                        {
                            Console.WriteLine($"Error occured: {e.Error.Reason}");
                        }
                    }
                }
                catch (OperationCanceledException)
                {
                    consumer.Close();
                }
            }
        }
    }
}