namespace MyMusicLibrary.Infrastructure.DataAccess.Migrations;
public abstract class DatabaseVersions
{
    public const int TABLE_USER = 1;
    public const int TABLE_MUSIC = 2;
    public const int TABLE_ARTIST = 3;
    public const int TABLE_MUSICARTIST = 4;
    public const int TABLE_MUSICARTISTS = 5;
    public const int TABLE_MUSICARTISTSX = 6;
    public const int TABLE_MUSICARTISTSXx = 7;
    public const int TABLE_MUSICARTISTS_CASCADE = 8;
    public const int TABLE_PLAYLIST = 9;
    public const int TABLE_PLAYLIST_MUSICS = 10;
    public const int TABLE_MUSIC_FAVORITES = 11;
    public const int TABLE_MUSIC_FAVORITES_2 = 12;
    public const int TABLE_MUSIC_KEY_S3 = 13;
    public const int TABLE_MUSIC_BUCKETNAME_S3 = 14;
    public const int TABLE_MUSIC_ARTIST_NULLABLE = 15;
    public const int TABLE_MUSIC_DROP_LEGACY_ARTIST_COLUMN = 16;
    public const int TABLE_ARTIST_MAKE_MUSIC_NULLABLE = 17;
    public const int TABLE_ARTIST_GENRE_NULLABLE = 18;
    public const int REFRESH_TOKEN_EXPIRATION = 19;
}
