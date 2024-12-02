using FluentMigrator;

namespace Clients.Infrastructure.Migrations;

[Migration(202412021607)]
public class Migration202412021607 : Migration
{
    public override void Up()
    {
        Create.Table(ClientTable)
            .WithColumn("Id").AsGuid().PrimaryKey()
            .WithColumn("FirstName").AsString().NotNullable()
            .WithColumn("LastName").AsString().Nullable();
    }

    public override void Down()
    {
        Delete.Table(ClientTable);
    }
}