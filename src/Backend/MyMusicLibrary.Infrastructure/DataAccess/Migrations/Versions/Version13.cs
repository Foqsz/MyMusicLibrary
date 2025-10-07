using FluentMigrator;
using MyMusicLibrary.Infrastructure.Migrations.Versions;

namespace MyMusicLibrary.Infrastructure.DataAccess.Migrations.Versions;
[Migration(DatabaseVersions.TABLE_MUSIC_KEY_S3, "Criando a tabela musicKey pra adicionar a key da amazon s3.")]
public class Version0000013 : VersionBase
{
    public override void Up()
    {
        Alter.Table("Music")
            .AddColumn("MusicKey")
            .AsString(355)
            .Nullable();
    }
}
