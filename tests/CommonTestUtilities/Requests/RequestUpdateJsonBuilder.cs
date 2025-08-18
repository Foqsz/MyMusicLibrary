using Bogus;
using MyMusicLibrary.Communication.Request;

namespace CommonTestUtilities.Requests;
public class RequestUpdateJsonBuilder
{
    public static RequestUpdateUserJson Build()
    {
        return new Faker<RequestUpdateUserJson>()
            .RuleFor(user => user.Name, (f) => f.Person.FirstName)
            .RuleFor(user => user.Email, (f, user) => f.Internet.Email(user.Name));
    }
}
