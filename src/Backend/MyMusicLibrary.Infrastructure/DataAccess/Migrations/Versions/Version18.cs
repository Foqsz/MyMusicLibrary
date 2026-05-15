using FluentMigrator;
using MyMusicLibrary.Infrastructure.Migrations.Versions;

namespace MyMusicLibrary.Infrastructure.DataAccess.Migrations.Versions;

[Migration(DatabaseVersions.TABLE_ARTIST_GENRE_NULLABLE, "Torna a coluna 'Genre' da tabela Artist nullable")]
public class Version0000018 : VersionBase
{
    public override void Up()
    {
        if (Schema.Table("Artist").Column("Genre").Exists())
        {
            Alter.Column("Genre")
                .OnTable("Artist")
                .AsString(100)
                .Nullable();
        }
    }
}