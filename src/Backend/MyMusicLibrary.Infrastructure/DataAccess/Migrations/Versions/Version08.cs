using FluentMigrator;
using MyMusicLibrary.Infrastructure.DataAccess.Migrations;

namespace MyMusicLibrary.Infrastructure.Migrations.Versions
{
    [Migration(DatabaseVersions.TABLE_MUSICARTISTS_CASCADE, "Update Artist FK to cascade on delete")]
    public class Version0000008 : VersionBase
    {
        public override void Up()
        {
            // Remove a FK antiga
            Delete.ForeignKey("FK_Artist_Music_Id").OnTable("Artist");

            // Recria com ON DELETE CASCADE
            Create.ForeignKey("FK_Artist_Music_Id")
                .FromTable("Artist").ForeignColumn("MusicId")
                .ToTable("Music").PrimaryColumn("Id")
                .OnDelete(System.Data.Rule.Cascade);
        } 
    }
}
