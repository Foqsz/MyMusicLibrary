using CommonTestUtilities.Entities;
using CommonTestUtilities.Repositores;
using CommonTestUtilities.Requests;
using CommonTestUtilities.Tokens.Generator;
using CommonTestUtilities.Tokens.Refresh;
using MyMusicLibrary.Application.UseCases.Token.RefreshToken;
using MyMusicLibrary.Communication.Request;
using MyMusicLibrary.Domain.Extensions;
using MyMusicLibrary.Exceptions;
using MyMusicLibrary.Exceptions.ExceptionsBase;
using Shouldly;
using Xunit;

namespace UseCases.Test.Token.RefreshToken;
public class UseRefreshTokenUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        (var user, var _) = UserBuilder.Build();
        var refreshToken = RequestNewTokenJsonBuilder.Build();

        var useCase = CreateUseCase(user, refreshToken, false, false);

        var result = await useCase.Execute(refreshToken); 

        result.RefreshToken.ShouldNotBeNullOrEmpty();
        result.AccessToken.ShouldNotBeNullOrEmpty();
    }

    [Fact]
    public async Task Error_RefreshToken_Null()
    {
        (var user, var _) = UserBuilder.Build();
        var refreshToken = RequestNewTokenJsonBuilder.Build();

        var useCase = CreateUseCase(user, refreshToken, true, false);  
        var result = await Should.ThrowAsync<RefreshTokenNotFoundException>(async () =>
        {
            await useCase.Execute(refreshToken);
        });

        result.Message.ShouldBe(ResourceMessagesException.EXPIRED_SESSION);
        result.ShouldNotBeNull();
    }

    [Fact]
    public async Task Error_RefreshToken_Expirated()
    {
        (var user, var _) = UserBuilder.Build();
        var refreshToken = RefreshTokenBuilder.Build(user);

        var token = new RequestNewTokenJson()
        {
            RefreshToken = refreshToken.Value
        };

        var useCase = CreateUseCase(user, token, false, true);

        var result = await Should.ThrowAsync<RefreshTokenExpiredException>(async () =>
        {
            await useCase.Execute(token);
        });

        result.Message.ShouldBe(ResourceMessagesException.INVALID_SESSION);
        result.ShouldNotBeNull();
    }

    private static UseRefreshTokenUseCase CreateUseCase(MyMusicLibrary.Domain.Entities.User user, RequestNewTokenJson refreshToken, bool tokenNull, bool createdExpirated)
    {
        var tokenRepository = new TokenRepositoryBuilder();
        var unitOfWork = UnitOfWorkBuilder.Build();
        var tokenGenerator = JwtTokenGeneratorBuilder.Build();
        var refreshTokenGenerator = RefreshTokenGeneratorBuilder.Build();

        if (user is not null && tokenNull.IsFalse() && createdExpirated.IsFalse())
        {
            tokenRepository.Get(user, refreshToken.RefreshToken, false);
            tokenRepository.SaveNewRefreshToken(user);
        }
        else if(user is not null && tokenNull.IsFalse() && createdExpirated is true)
        {
            tokenRepository.Get(user!, refreshToken.RefreshToken, true);
            tokenRepository.SaveNewRefreshToken(user!);
        }

        return new UseRefreshTokenUseCase(tokenRepository.Build(), unitOfWork, tokenGenerator, refreshTokenGenerator);
    }
}
