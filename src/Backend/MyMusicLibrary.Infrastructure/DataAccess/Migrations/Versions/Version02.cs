using FluentMigrator;
using MyMusicLibrary.Infrastructure.DataAccess.Migrations;

namespace MyMusicLibrary.Infrastructure.Migrations.Versions;

[Migration(DatabaseVersions.TABLE_MUSIC, "Create table to save the music's songs informations")]
public class Version0000002 : VersionBase
{
    public override void Up()
    {
        CreateTable("Music")
            .WithColumn("Name").AsString(255).NotNullable()
            .WithColumn("Artist").AsString(100).NotNullable()
            .WithColumn("Album").AsString(100).NotNullable()
            .WithColumn("UserId").AsInt64().NotNullable().ForeignKey("FK_User_Music_Id", "Users", "Id");
    }
}