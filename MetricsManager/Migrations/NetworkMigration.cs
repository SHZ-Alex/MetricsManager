using FluentMigrator;

namespace MetricsAgent.Migrations
{
    [Migration(3)]
    public class NetworkMigration : Migration
    {

        /// <summary>
        /// Выполняется в случае применения миграции
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public override void Up()
        {
            Create.Table("NetworkMetric")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("AgentId").AsInt32()
                .WithColumn("Value").AsInt32()
                .WithColumn("Time").AsInt64();
        }

        /// <summary>
        /// Выполняется в случае отката миграции
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public override void Down()
        {
            Delete.Table("NetworkMetric");
        }


    }
}
