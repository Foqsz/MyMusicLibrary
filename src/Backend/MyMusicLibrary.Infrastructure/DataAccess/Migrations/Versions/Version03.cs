using FluentMigrator;
using MyMusicLibrary.Infrastructure.DataAccess.Migrations;

namespace MyMusicLibrary.Infrastructure.Migrations.Versions;

[Migration(DatabaseVersions.TABLE_ARTIST, "Create table to save the artist informations")]
public class Version0000003 : VersionBase
{
    public override void Up()
    {
        CreateTable("Artist")
            .WithColumn("Name").AsString(255).NotNullable()
            .WithColumn("Genre").AsString(100).NotNullable()
            .WithColumn("Music").AsString(100).NotNullable()
            .WithColumn("MusicId").AsInt64().NotNullable().ForeignKey("FK_Music_User_Id", "Music", "Id");
    }
}