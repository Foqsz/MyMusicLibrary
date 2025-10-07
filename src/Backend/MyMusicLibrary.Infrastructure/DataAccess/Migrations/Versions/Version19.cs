using FluentMigrator;
using MyMusicLibrary.Infrastructure.Migrations.Versions;

namespace MyMusicLibrary.Infrastructure.DataAccess.Migrations.Versions;

[Migration(DatabaseVersions.REFRESH_TOKEN_EXPIRATION, "Implementando refresh token.")]
public class Version19 : VersionBase
{
    public override void Up()
    {
        CreateTable("RefreshTokens")
            .WithColumn("Value").AsString().NotNullable()
            .WithColumn("UserId").AsInt64().NotNullable().ForeignKey("FK_RefreshTokens_User_Id", "Users", "Id");
    }
}
