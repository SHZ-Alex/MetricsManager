using FluentMigrator;

namespace MetricsAgent.Migrations
{
    [Migration(5)]
    public class AgentInfoMigration : Migration
    {

        /// <summary>
        /// Выполняется в случае применения миграции
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public override void Up()
        {
            Create.Table("AgentInfo")
                .WithColumn("AgentId").AsInt32().PrimaryKey().Identity()
                .WithColumn("AgentAddress").AsString();
        }

        /// <summary>
        /// Выполняется в случае отката миграции
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public override void Down()
        {
            Delete.Table("AgentInfo");
        }


    }
}
