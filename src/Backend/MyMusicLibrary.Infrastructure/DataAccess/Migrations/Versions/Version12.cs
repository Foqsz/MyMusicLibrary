using FluentMigrator;
using MyMusicLibrary.Infrastructure.Migrations.Versions;

namespace MyMusicLibrary.Infrastructure.DataAccess.Migrations.Versions;
[Migration(DatabaseVersions.TABLE_MUSIC_FAVORITES_2, "No-op: migração substituída pela Version11.")]
public class Version0000012 : VersionBase
{
    public override void Up()
    {
        // Intencionalmente vazio para evitar recriação da tabela já criada na Version11.
    }
}
