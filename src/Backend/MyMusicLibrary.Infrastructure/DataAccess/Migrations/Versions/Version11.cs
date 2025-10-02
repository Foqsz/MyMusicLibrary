using FluentMigrator;
using MyMusicLibrary.Infrastructure.DataAccess.Migrations;
using MyMusicLibrary.Infrastructure.Migrations.Versions;

namespace MyMusicLibrary.Migrations
{
    [Migration(DatabaseVersions.TABLE_MUSIC_FAVORITES, "Criação da tabela UserFavoritesMusic para armazenar músicas favoritas dos usuários.")]
    public class Version0000011 : VersionBase
    {
        public override void Up()
        {
            Create.Table("UserFavoritesMusic")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("UserId").AsInt64().NotNullable()
                .WithColumn("MusicId").AsInt64().NotNullable()
                .WithColumn("Active").AsBoolean().NotNullable().WithDefaultValue(true)
                .WithColumn("CreatedOn").AsDateTime().NotNullable().WithDefault(SystemMethods.CurrentUTCDateTime);

            // Garante que os tipos sejam BIGINT (64 bits) antes de criar FKs (mitiga mapeamentos inconsistentes)
            Execute.Sql("""
                ALTER TABLE `UserFavoritesMusic`
                MODIFY COLUMN `Id` BIGINT NOT NULL AUTO_INCREMENT,
                MODIFY COLUMN `UserId` BIGINT NOT NULL,
                MODIFY COLUMN `MusicId` BIGINT NOT NULL;
            """);

            // Único por usuário + música
            Create.UniqueConstraint("UX_UserFavoritesMusic_UserId_MusicId")
                .OnTable("UserFavoritesMusic")
                .Columns("UserId", "MusicId");

            // FKs
            Create.ForeignKey("FK_UserFavoritesMusic_Users")
                .FromTable("UserFavoritesMusic").ForeignColumn("UserId")
                .ToTable("Users").PrimaryColumn("Id");

            Create.ForeignKey("FK_UserFavoritesMusic_Music")
                .FromTable("UserFavoritesMusic").ForeignColumn("MusicId")
                .ToTable("Music").PrimaryColumn("Id");
        }
    }
}
