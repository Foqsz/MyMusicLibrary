namespace MyMusicLibrary.Communication.Request;
public class RequestUserChangePassword
{
    public string CurrentPassword { get; set; } = string.Empty;
    public string NewPassword { get; set; } = string.Empty;
}
