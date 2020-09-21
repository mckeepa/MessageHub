    using System;
    using System.Collections.Generic;
    using System.Text;
    using Confluent.Kafka;
    
namespace coreWeb.Models 
{
    class Publisher
    {

        public void Publish(string type, string user, string body)
        {
            var config = new ProducerConfig { BootstrapServers = "localhost:9092"};

            Action<DeliveryReport<Null, string>> handler = r =>
                Console.WriteLine(!r.Error.IsError
                ? $"Delivered message to {r.TopicPartitionOffset}"
                : $"Delivery Error: {r.Error.Reason}");

            using (var producer = new ProducerBuilder<Null, string>(config).Build())
            {
                var stringValue = body;
                    stringValue += "👮🙉🍌 lol" + DateTime.Now.ToLongTimeString();

                var headers3 = new Headers();
                headers3.Add(new Header("test-header-a", new byte[] { 111 } ));
                headers3.Add(new Header("test-header-b", new byte[] { 112 } ));
                headers3.Add(new Header("test-header-a", new byte[] { 113 } ));
                headers3.Add(new Header("test-header-b", new byte[] { 114 } ));
                headers3.Add(new Header("test-header-c", new byte[] { 115 } ));

                producer.ProduceAsync("banana-topic", new Message<Null, string> { Value = stringValue,  Headers = headers3 });
         
                producer.Flush(TimeSpan.FromSeconds(10));
            }    
        }
    }
}