namespace MyMusicLibrary.Domain.Extensions;
public static class BooleanExtension
{
    public static bool IsFalse(this bool value) => !value;
    public static bool IsTrue(this bool value) => value;
    public static bool NotEmpty(this string? value) => string.IsNullOrWhiteSpace(value).IsFalse();
}
