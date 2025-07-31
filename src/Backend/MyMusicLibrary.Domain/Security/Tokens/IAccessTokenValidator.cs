namespace MyMusicLibrary.Domain.Security.Tokens;
public interface IAccessTokenValidator
{
    Guid ValidateAnGetUserIdentifier(string token);
}
