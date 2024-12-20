using FluentMigrator;

namespace Staffs.Infrastructure.Migrations;

[Migration(202412201653)]
public class Migration202412201653 : Migration
{
    public override void Up()
    {
        Create.Table(StaffTable)
            .WithColumn("Id").AsGuid().PrimaryKey()
            .WithColumn("FirstName").AsString().NotNullable()
            .WithColumn("LastName").AsString().Nullable();
    }

    public override void Down()
    {
        Delete.Table(StaffTable);
    }
}