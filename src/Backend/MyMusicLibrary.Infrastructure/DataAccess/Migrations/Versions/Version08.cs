using FluentMigrator;
using MyMusicLibrary.Infrastructure.DataAccess.Migrations;

namespace MyMusicLibrary.Infrastructure.Migrations.Versions
{
    [Migration(DatabaseVersions.TABLE_MUSICARTISTS_CASCADE, "Update Artist FK to cascade on delete")]
    public class Version0000008 : VersionBase
    {
        public override void Up()
        {
            // Handle inconsistent FK names from earlier migrations:
            // Version0000003 created "FK_Music_User_Id" on Artist(MusicId) -> Music(Id)
            // Other versions referenced "FK_Artist_Music_Id".
            // Drop whichever exists, then recreate a single consistent FK with ON DELETE CASCADE.

            if (Schema.Table("Artist").Constraint("FK_Music_User_Id").Exists())
            {
                Delete.ForeignKey("FK_Music_User_Id").OnTable("Artist");
            }

            if (Schema.Table("Artist").Constraint("FK_Artist_Music_Id").Exists())
            {
                Delete.ForeignKey("FK_Artist_Music_Id").OnTable("Artist");
            }

            Create.ForeignKey("FK_Artist_Music_Id")
                .FromTable("Artist").ForeignColumn("MusicId")
                .ToTable("Music").PrimaryColumn("Id")
                .OnDelete(System.Data.Rule.Cascade);
        }
    }
}
