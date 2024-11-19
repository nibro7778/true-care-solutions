namespace Clients.Application;

public static class DbConstants
{
    public static readonly string Schema = "Clients".ToLowerInvariant() + "_module";
    public const string IdColumn = "id";
    public const string NameColumn = "name";
    public const string PriceColumn = "price";
    public const string WidgetsTable = "widgets";
    public const string QueueName="widgets_queue";
}