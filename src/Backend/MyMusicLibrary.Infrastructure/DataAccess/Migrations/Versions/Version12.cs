using FluentMigrator;
using MyMusicLibrary.Infrastructure.DataAccess.Migrations;
using MyMusicLibrary.Infrastructure.Migrations.Versions;

[Migration(DatabaseVersions.TABLE_MUSIC_FAVORITES_2, "Atualiza UserFavoritesMusic adicionando Active e CreatedOn")]
public class Version12 : VersionBase
{
    public override void Up()
    {
        Create.Table("UserFavoritesMusic")
            .WithColumn("Id").AsInt32().PrimaryKey().Identity()
            .WithColumn("UserId").AsInt64().NotNullable()
            .WithColumn("MusicId").AsInt64().NotNullable()
            .WithColumn("Active").AsBoolean().NotNullable().WithDefaultValue(true) // adiciona Active
            .WithColumn("CreatedOn").AsDateTime().NotNullable().WithDefault(SystemMethods.CurrentUTCDateTime); // adiciona CreatedOn

        // Garante que um usuário não favorite a mesma música duas vezes
        Create.UniqueConstraint("UX_UserFavoritesMusic_UserId_MusicId")
            .OnTable("UserFavoritesMusic")
            .Columns("UserId", "MusicId");

        // Foreign Key -> Users.Id
        Create.ForeignKey("FK_UserFavoritesMusic_Users")
            .FromTable("UserFavoritesMusic").ForeignColumn("UserId")
            .ToTable("Users").PrimaryColumn("Id");

        // Foreign Key -> Music.Id
        Create.ForeignKey("FK_UserFavoritesMusic_Music")
            .FromTable("UserFavoritesMusic").ForeignColumn("MusicId")
            .ToTable("Music").PrimaryColumn("Id");
    }
}
