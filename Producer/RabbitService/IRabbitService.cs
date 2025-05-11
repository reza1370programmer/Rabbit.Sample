namespace Producer.RabbitService
{
    public interface IRabbitService
    {
        Task RabbitProducer(string Body, string RoutingKey, string QueueName);
        Task RabbitConsumer();
    }
}
