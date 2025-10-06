using FluentMigrator;
using MyMusicLibrary.Infrastructure.Migrations.Versions;

namespace MyMusicLibrary.Infrastructure.DataAccess.Migrations.Versions;

[Migration(DatabaseVersions.TABLE_PLAYLIST_MUSICS, "Fix Music-Playlist relation and remove broken PlaylistMusics")]
public class Version0000010 : VersionBase
{
    public override void Up()
    {
        if (Schema.Table("PlaylistMusics").Exists())
        {
            Delete.Table("PlaylistMusics");
        }

        // Adiciona a coluna PlaylistId na tabela Music se não existir
        if (!Schema.Table("Music").Column("PlaylistId").Exists())
        {
            Alter.Table("Music")
                .AddColumn("PlaylistId").AsInt64().Nullable();

            Create.ForeignKey("FK_Music_Playlist")
                .FromTable("Music").ForeignColumn("PlaylistId")
                .ToTable("Playlist").PrimaryColumn("Id")
                .OnDeleteOrUpdate(System.Data.Rule.Cascade);
        }
    }
}
