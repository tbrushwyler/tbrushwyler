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
            Create.Table("IdentityUser")
                .WithColumn("Id").AsString(128).NotNullable().PrimaryKey()
                .WithColumn("Username").AsString(255).Unique().NotNullable()
                .WithColumn("Email").AsString(255).Unique().NotNullable()
                .WithColumn("PasswordHash").AsString(255).Nullable()
                .WithColumn("SecurityStamp").AsString(255).Nullable()
                .WithColumn("IsConfirmedByUser").AsBoolean().NotNullable().WithDefaultValue(false)
                .WithColumn("ConfirmationToken").AsString(255).Nullable();

            Create.Table("IdentityRole")
                .WithColumn("Id").AsString(128).NotNullable().PrimaryKey()
                .WithColumn("Name").AsString(255).NotNullable();

            Create.Table("IdentityUserRole")
                .WithColumn("UserId").AsString(128).NotNullable().PrimaryKey().ForeignKey("UserRoleToUser", "IdentityUser", "Id")
                .WithColumn("RoleId").AsString(128).NotNullable().PrimaryKey().ForeignKey("UserRoleToRole", "IdentityRole", "Id");

            Create.Table("IdentityLogin")
                .WithColumn("Id").AsInt64().NotNullable().PrimaryKey().Identity()
                .WithColumn("LoginProvider").AsString(255).NotNullable()
                .WithColumn("ProviderKey").AsString(255).NotNullable()
                .WithColumn("UserId").AsString(128).NotNullable().ForeignKey("LoginToUser", "IdentityUser", "Id");

            Create.Table("IdentityClaim")
                .WithColumn("Id").AsInt64().NotNullable().PrimaryKey().Identity()
                .WithColumn("ClaimType").AsString(255).NotNullable()
                .WithColumn("ClaimValue").AsString(255).NotNullable()
                .WithColumn("UserId").AsString(128).NotNullable().ForeignKey("ClaimToUser", "IdentityUser", "Id");
        }

        public override void Down()
        {
            Delete.Table("IdentityClaim");
            Delete.Table("IdentityLogin");
            Delete.Table("IdentityUserRole");
            Delete.Table("IdentityRole");
            Delete.Table("IdentityUser");
        }
    }
}
