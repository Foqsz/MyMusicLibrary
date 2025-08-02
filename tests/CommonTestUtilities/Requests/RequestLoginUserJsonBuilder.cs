using Bogus;
using MyMusicLibrary.Communication.Request;

namespace CommonTestUtilities.Requests;
public class RequestLoginUserJsonBuilder
{
    public static RequestDoLoginJson Build(int passwordLenght = 10)
    {
        return new Faker<RequestDoLoginJson>() 
            .RuleFor(user => user.Email, (f, user) => f.Internet.Email())
            .RuleFor(user => user.Password, (f) => f.Internet.Password(passwordLenght));
    }
}
