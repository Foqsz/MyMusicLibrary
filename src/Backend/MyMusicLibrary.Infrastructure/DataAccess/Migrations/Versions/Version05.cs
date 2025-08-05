using FluentMigrator;
using MyMusicLibrary.Infrastructure.DataAccess.Migrations;

namespace MyMusicLibrary.Infrastructure.Migrations.Versions;

[Migration(DatabaseVersions.TABLE_MUSICARTISTS, "Create tables Music and Artist with foreign key")]
public class Version0000005 : VersionBase
{
    public override void Up()
    {
        // Cria primeiro a tabela Music, pois será referenciada por Artist
        CreateTable("Music")
            .WithColumn("Name").AsString(255).NotNullable()
            .WithColumn("Album").AsString(100).NotNullable()
            .WithColumn("Artist").AsString(100).NotNullable()
            .WithColumn("UserId").AsInt64().NotNullable()
                .ForeignKey("FK_Music_User_Id", "users", "Id")
                .OnDelete(System.Data.Rule.Cascade);

        // Depois cria a tabela Artist, que depende de Music
        CreateTable("Artist")
            .WithColumn("Name").AsString(255).NotNullable()
            .WithColumn("Genre").AsString(100).NotNullable()
            .WithColumn("Music").AsString(100).NotNullable()
            .WithColumn("MusicId").AsInt64().NotNullable()
                .ForeignKey("FK_Artist_Music_Id", "music", "Id");
    }
}
