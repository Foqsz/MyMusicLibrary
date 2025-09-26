using FluentMigrator;
using MyMusicLibrary.Infrastructure.DataAccess.Migrations;
using MyMusicLibrary.Infrastructure.Migrations.Versions;

[Migration(DatabaseVersions.TABLE_MUSIC_KEY_S3, "Criando a tabela musicKey pra adicionar a key da amazon s3.")]
public class Version13 : VersionBase
{
    public override void Up()
    {
        Alter.Table("Music")
            .AddColumn("MusicKey")
            .AsString(355)
            .Nullable();
    }
}
