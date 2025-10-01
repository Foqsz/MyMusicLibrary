using FluentMigrator;
using MyMusicLibrary.Infrastructure.DataAccess.Migrations;

namespace MyMusicLibrary.Infrastructure.Migrations.Versions;

[Migration(DatabaseVersions.TABLE_MUSICARTISTSX, "Create tables Music and Artist with foreign key")]
public class Version0000006 : VersionBase
{
    public override void Up()
    {
        if (!Schema.Table("Music").Exists() && !Schema.Table("music").Exists())
        {
            // Cria tabela Music
            CreateTable("Music")
                .WithColumn("Name").AsString(255).NotNullable()
                .WithColumn("Album").AsString(100).NotNullable()
                .WithColumn("UserId").AsInt64().NotNullable()
                    .ForeignKey("FK_Music_User_Id", "users", "Id")
                    .OnDelete(System.Data.Rule.Cascade);
        }

        if (!Schema.Table("Artist").Exists() && !Schema.Table("artist").Exists())
        {
            // Cria tabela Artist
            CreateTable("Artist")
                .WithColumn("Name").AsString(255).NotNullable()
                .WithColumn("Genre").AsString(100).NotNullable()
                .WithColumn("MusicId").AsInt64().NotNullable()
                    .ForeignKey("FK_Artist_Music_Id", "music", "Id")
                    .OnDelete(System.Data.Rule.Cascade);
        }
    }
}