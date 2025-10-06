using FluentMigrator;
using MyMusicLibrary.Infrastructure.Migrations.Versions;

namespace MyMusicLibrary.Infrastructure.DataAccess.Migrations.Versions;

[Migration(DatabaseVersions.TABLE_MUSIC_ARTIST_NULLABLE, "Torna a coluna Artist da tabela Music nullable para evitar erro ao salvar sem valor.")]
public class Version0000015 : VersionBase
{
    public override void Up()
    {
        // Torna a coluna Artist nullable (mantendo o tipo e tamanho atual)
        Alter.Column("Artist")
            .OnTable("Music")
            .AsString(100)
            .Nullable();
    }
}