using FluentMigrator;
using MyMusicLibrary.Infrastructure.DataAccess.Migrations;
using MyMusicLibrary.Infrastructure.Migrations.Versions;

[Migration(DatabaseVersions.TABLE_MUSIC_BUCKETNAME_S3, "Criando a tabela AwsS3BucketName pra adicionar o bucket name da amazon s3.")]
public class Version14 : VersionBase
{
    public override void Up()
    {
        Alter.Table("Music")
            .AddColumn("AwsS3BucketName")
            .AsString(355)
            .Nullable();
    }
}
