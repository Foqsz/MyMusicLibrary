namespace CommonTestUtilities.Requests;
using Bogus;
using MyMusicLibrary.Communication.Request;
using MyMusicLibrary.Domain.Security.Cryptography;

public class RequestRegisterUserJsonBuilder
{
    public static RequestRegisterUserJson Build(int passwordLenght = 10)
    {
        return new Faker<RequestRegisterUserJson>()
            .RuleFor(user => user.Name, (f) => f.Person.FirstName)
            .RuleFor(user => user.Email, (f, user) => f.Internet.Email(user.Name))
            .RuleFor(user => user.Password, (f) => f.Internet.Password(passwordLenght));
    }
}
