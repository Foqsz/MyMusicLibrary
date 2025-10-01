using FluentMigrator;
using MyMusicLibrary.Infrastructure.DataAccess.Migrations;

namespace MyMusicLibrary.Infrastructure.Migrations.Versions;

[Migration(DatabaseVersions.TABLE_MUSICARTIST, "Create table Artist with foreign key to Music")]
public class Version0000004 : VersionBase
{
    public override void Up()
    {
        // Se já existe qualquer uma das tabelas, não tenta recriar nada nesta versão
        if (Schema.Table("Artist").Exists() || Schema.Table("artist").Exists()
            || Schema.Table("Music").Exists() || Schema.Table("music").Exists())
        {
            return;
        }

        CreateTable("Music")
            .WithColumn("Name").AsString(255).NotNullable()
            .WithColumn("Artist").AsString(100).NotNullable()
            .WithColumn("Album").AsString(100).NotNullable()
            .WithColumn("UserId").AsInt64().NotNullable()
                .ForeignKey("FK_Music_User_Id", "Users", "Id")
                .OnDelete(System.Data.Rule.Cascade);

        CreateTable("Artist")
            .WithColumn("Name").AsString(255).NotNullable()
            .WithColumn("Genre").AsString(100).NotNullable()
            .WithColumn("Music").AsString(100).NotNullable()
            .WithColumn("MusicId").AsInt64().NotNullable()
                .ForeignKey("FK_Artist_Music_Id", "Music", "Id");
    }
}
