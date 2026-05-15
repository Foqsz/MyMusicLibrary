namespace MyMusicLibrary.Domain.Security.Tokens;
public interface IRefreshTokenGenerator
{
    string Generate();
}
