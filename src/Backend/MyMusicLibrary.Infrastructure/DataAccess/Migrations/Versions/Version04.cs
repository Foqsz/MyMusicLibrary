using FluentMigrator;
using MyMusicLibrary.Infrastructure.DataAccess.Migrations;

namespace MyMusicLibrary.Infrastructure.Migrations.Versions;

[Migration(DatabaseVersions.TABLE_MUSICARTIST, "Create table Artist with foreign key to Music")]
public class Version0000004 : VersionBase
{
    public override void Up()
    {
        // Cria apenas a tabela Artist
        CreateTable("Artist")
            .WithColumn("Name").AsString(255).NotNullable()
            .WithColumn("Genre").AsString(100).NotNullable()
            .WithColumn("Music").AsString(100).NotNullable()
            .WithColumn("MusicId").AsInt64().NotNullable()
                .ForeignKey("FK_Artist_Music_Id", "Music", "Id");
    }
}
