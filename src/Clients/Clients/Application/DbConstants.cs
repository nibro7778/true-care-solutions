namespace Clients.Application;

public static class DbConstants
{
    public static readonly string Database = "clients".ToLowerInvariant() + "_module";
    public const string ClientTable = "Clients";
    public const string QueueName="clients_queue";
}