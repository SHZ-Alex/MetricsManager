using FluentMigrator;

namespace MetricsAgent.Migrations
{
    [Migration(1)]
    public class CpuMigration : Migration
    {

        /// <summary>
        /// Выполняется в случае применения миграции
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public override void Up()
        {
            Create.Table("cpumetrics")
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
            Delete.Table("cpumetrics");
        }


    }
}
