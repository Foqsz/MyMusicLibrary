namespace MyMusicLibrary.Exceptions.ExceptionsBase;
public class ErrorOnValidationException : MyMusicLibraryException
{
    public IList<string> ErrorMessages { get; set; }

    public ErrorOnValidationException(IList<string> errorMessages)
    {
        ErrorMessages = errorMessages;
    }
}
