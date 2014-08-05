using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentMigrator;

namespace Advocado.Migrations
{
    [Migration(201407291839, "Create Question table")]
    public class Migration_201407291845_Create_Question_table : Migration
    {
        public override void Up()
        {
            Create.Table("Question").InSchema("dbo")
                .WithColumn("Id").AsInt64().Identity().PrimaryKey()
                .WithColumn("Question").AsString(255).NotNullable()
                .WithColumn("CreatedDateTime").AsDateTime().WithDefault(SystemMethods.CurrentUTCDateTime)
                .WithColumn("DeletedDateTime").AsDateTime().Nullable();
        }

        public override void Down()
        {
            Delete.Table("Question").InSchema("dbo");
        }
    }
}
