using FluentMigrator;
using MyMusicLibrary.Infrastructure.Migrations.Versions;

namespace MyMusicLibrary.Infrastructure.DataAccess.Migrations.Versions;

[Migration(DatabaseVersions.TABLE_MUSIC_DROP_LEGACY_ARTIST_COLUMN, "Remove coluna legada 'Artist' da tabela Music (string)")]
public class Version0000016 : VersionBase
{
    public override void Up()
    {
        // Remove a coluna legada que causa conflito com a modelagem relacional
        // (faça backup do banco antes)
        Delete.Column("Artist").FromTable("Music");
    }
}