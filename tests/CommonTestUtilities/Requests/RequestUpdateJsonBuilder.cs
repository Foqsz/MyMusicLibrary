using Bogus;

namespace CommonTestUtilities.Requests;
public class RequestUpdateJsonBuilder
{
    public static MyMusicLibrary.Domain.Entities.User Build()
    {
        return new Faker<MyMusicLibrary.Domain.Entities.User>()
            .RuleFor(user => user.Name, (f) => f.Person.FirstName)
            .RuleFor(user => user.Email, (f, user) => f.Internet.Email(user.Name));
    }
}
