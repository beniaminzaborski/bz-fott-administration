using FluentMigrator;

namespace Bz.Fott.Administration.Infrastructure.Persistence.Migrations;

[Migration(202302011635)]
public class _202302011635_CreateTable_Checkpoint : Migration
{
    public override void Down()
    {
        Delete.Table("checkpoints");
    }

    public override void Up()
    {
        Create.Table("checkpoints")
            .WithColumn("id").AsGuid().NotNullable()
            .WithColumn("trackPointAmount").AsDecimal().NotNullable()
            .WithColumn("trackPointUnit").AsByte().NotNullable()
            .WithColumn("competitionId").AsGuid().NotNullable();

        Create.PrimaryKey($"PK__checkpoints__id")
            .OnTable("checkpoints").Column("id");

        Create.ForeignKey("FK__checkpoints__competitionId__competitions__id")
            .FromTable("checkpoints").ForeignColumn("competitionId")
            .ToTable("competitions").PrimaryColumn("id");
    }
}
