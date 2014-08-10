using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentMigrator;

namespace cumulo.ninja.Infrastructure.Migrations
{
    [Migration(201408101152, "ASP.NET Identity")]
    public class Migration_201408101152 : Migration
    {
        public override void Up()
        {
            Create.Schema("Identity");

            Create.Table("User").InSchema("Identity")
                .WithColumn("Id").AsString(128).NotNullable().PrimaryKey()
                .WithColumn("Username").AsString(255).Unique().NotNullable()
                .WithColumn("Email").AsString(255).Unique().NotNullable()
                .WithColumn("PasswordHash").AsString(255).NotNullable()
                .WithColumn("SecurityStamp").AsString(255)
                .WithColumn("ConfirmationToken").AsString(255).Nullable()
                .WithColumn("PasswordResetToken").AsString(255).Nullable();

            Create.Table("Role").InSchema("Identity")
                .WithColumn("Id").AsString(128).NotNullable().PrimaryKey()
                .WithColumn("Name").AsString(255);

            Create.Table("UserRole").InSchema("Identity")
                .WithColumn("UserId").AsString(128).NotNullable().PrimaryKey().ForeignKey("UserRoleToUser", "Identity", "User", "Id")
                .WithColumn("RoleId").AsString(128).NotNullable().PrimaryKey().ForeignKey("UserRoleToRole", "Identity", "Role", "Id");

            Create.Table("Login").InSchema("Identity")
                .WithColumn("Id").AsInt64().NotNullable().PrimaryKey().Identity()
                .WithColumn("LoginProvider").AsString(255)
                .WithColumn("ProviderKey").AsString(255)
                .WithColumn("UserId").AsString(128).NotNullable().ForeignKey("LoginToUser", "Identity", "User", "Id");

            Create.Table("Claim").InSchema("Identity")
                .WithColumn("Id").AsInt64().NotNullable().PrimaryKey().Identity()
                .WithColumn("ClaimType").AsString(255)
                .WithColumn("ClaimValue").AsString(255)
                .WithColumn("UserId").AsString(128).NotNullable().ForeignKey("ClaimToUser", "Identity", "User", "Id");
        }

        public override void Down()
        {
            Delete.Table("Claim").InSchema("Identity");
            Delete.Table("Login").InSchema("Identity");
            Delete.Table("UserRole").InSchema("Identity");
            Delete.Table("Role").InSchema("Identity");
            Delete.Table("User").InSchema("Identity");

            Delete.Schema("Identity");
        }
    }
}
