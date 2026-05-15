using Bogus;
using MyMusicLibrary.Communication.Request;

namespace CommonTestUtilities.Requests;
public class RequestUserChangePasswordBuilder
{
    public static RequestUserChangePassword Build()
    {
        return new Faker<RequestUserChangePassword>()
            .RuleFor(user => user.CurrentPassword, (f) => f.Internet.Password(7))
            .RuleFor(user => user.NewPassword, (f) => f.Internet.Password(8));
    }
}
