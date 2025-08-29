using FluentMigrator;
using MyMusicLibrary.Infrastructure.DataAccess.Migrations;

namespace MyMusicLibrary.Infrastructure.Migrations.Versions
{
    [Migration(DatabaseVersions.TABLE_PLAYLIST, "Create playlist")]
    public class Version0000009 : VersionBase
    {
        public override void Up()
        {
            CreateTable("Playlist")
                .WithColumn("Name").AsString(100).NotNullable()
                .WithColumn("Description").AsString(1024).NotNullable()
                .WithColumn("UserId").AsInt64().NotNullable()
                    .ForeignKey("FK_Playlist_User_Id", "Users", "Id")
                    .OnDelete(System.Data.Rule.Cascade);
        }
    }
}