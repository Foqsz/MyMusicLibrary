using FluentMigrator;
using MyMusicLibrary.Infrastructure.DataAccess.Migrations;
using MyMusicLibrary.Infrastructure.Migrations.Versions;

[Migration(DatabaseVersions.TABLE_MUSICARTISTSX, "Create tables Music and Artist with foreign key")]
public class Version0000006 : VersionBase
{
    public override void Up()
    {
        // Cria tabela Music
        CreateTable("Music")
            .WithColumn("Name").AsString(255).NotNullable()
            .WithColumn("Album").AsString(100).NotNullable()
            .WithColumn("UserId").AsInt64().NotNullable()
                .ForeignKey("FK_Music_User_Id", "users", "Id")
                .OnDelete(System.Data.Rule.Cascade);

        // Cria tabela Artist
        CreateTable("Artist")
            .WithColumn("Name").AsString(255).NotNullable()
            .WithColumn("Genre").AsString(100).NotNullable()
            .WithColumn("MusicId").AsInt64().NotNullable()
                .ForeignKey("FK_Artist_Music_Id", "music", "Id");
    }
}