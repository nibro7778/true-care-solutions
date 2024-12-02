namespace Clients.Application;

public static class DbConstants
{
    public static readonly string Schema = "Clients".ToLowerInvariant() + "_module";
    public const string ClientTable = "client";
    public const string QueueName="clients_queue";
}