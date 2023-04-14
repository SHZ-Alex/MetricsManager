using FluentMigrator;

namespace MetricsAgent.Migrations
{
    [Migration(4)]
    public class RamMigration : Migration
    {

        /// <summary>
        /// Выполняется в случае применения миграции
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public override void Up()
        {
            Create.Table("rammetrics")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("Value").AsInt32()
                .WithColumn("Time").AsInt64();
        }

        /// <summary>
        /// Выполняется в случае отката миграции
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public override void Down()
        {
            Delete.Table("rammetrics");
        }


    }
}
