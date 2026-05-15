using FluentMigrator;
using MyMusicLibrary.Infrastructure.Migrations.Versions;

namespace MyMusicLibrary.Infrastructure.DataAccess.Migrations.Versions;
[Migration(DatabaseVersions.TABLE_ARTIST_MAKE_MUSIC_NULLABLE, "Torna a coluna 'Music' em Artist nullable e MusicId opcional para permitir inserts sem valor.")]
public class Version0000017 : VersionBase
{
    public override void Up()
    {
        // Torna a coluna 'Music' nullable (se existir)
        if (Schema.Table("Artist").Column("Music").Exists())
        {
            Alter.Column("Music")
                .OnTable("Artist")
                .AsString(100)
                .Nullable();
        }

        // Torna a FK 'MusicId' nullable (se existir)
        if (Schema.Table("Artist").Column("MusicId").Exists())
        {
            Alter.Column("MusicId")
                .OnTable("Artist")
                .AsInt64()
                .Nullable();
        }
    }
}
