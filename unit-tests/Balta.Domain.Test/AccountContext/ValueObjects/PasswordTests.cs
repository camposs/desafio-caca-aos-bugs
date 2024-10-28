using Balta.Domain.AccountContext.ValueObjects;
using Balta.Domain.AccountContext.ValueObjects.Exceptions;
using Balta.Domain.SharedContext;
using System.Net.Mail;

namespace Balta.Domain.Test.AccountContext.ValueObjects;

public class PasswordTests
{
    [Fact]
    public void ShouldFailIfPasswordIsNull()
    {

        string plainText = null;

        var exceptionType = typeof(InvalidPasswordException);

        Assert.Throws(exceptionType, () =>
        {
            Password.ShouldCreate(plainText);
        });
    }

    [Fact]
    public void ShouldFailIfPasswordIsEmpty()
    {

        string plainText = "";

        var exceptionType = typeof(InvalidPasswordException);

        Assert.Throws(exceptionType, () =>
        {
            Password.ShouldCreate(plainText);
        });
    }

    [Fact]
    public void ShouldFailIfPasswordIsWhiteSpace()
    {

        string plainText = "aaa   ";

        var exceptionType = typeof(InvalidPasswordException);

        Assert.Throws(exceptionType, () =>
        {
            Password.ShouldCreate(plainText);
        });
    }

    [Fact]
    public void ShouldFailIfPasswordLenIsLessThanMinimumChars()
    {

        string plainText = "1234567";

        var exceptionType = typeof(InvalidPasswordException);

        Assert.Throws(exceptionType, () =>
        {
            Password.ShouldCreate(plainText);
        });
    }

    [Fact]
    public void ShouldFailIfPasswordLenIsGreaterThanMaxChars()
    {

        string plainText = "12345678901234567890123456789012345678901234567890";

        var exceptionType = typeof(InvalidPasswordException);

        Assert.Throws(exceptionType, () =>
        {
            Password.ShouldCreate(plainText);
        });
    }

    [Fact]
    public void ShouldHashPassword()
    {
        string plainText = "12345678901234567890";

        var pass = Password.ShouldCreate(plainText);

        Assert.True(pass.Hash != "");
    }

    [Fact]
    public void ShouldVerifyPasswordHash()
    {
        string plainText = "12345678";
        string hash = "10000.b6t41V3ohPuTMC8pfEIOWw==.kbmt52SQDxEW9DnaBHZTRj9BkZ8waz4qKVfs8ls3hUA=";

        var match = Password.ShouldMatch(hash, plainText);

        Assert.True(match);
    }

    [Fact]
    public void ShouldGenerateStrongPassword()
    {
        var plainText = Password.ShouldGenerate();

        var pass = Password.ShouldCreate(plainText);

        Assert.True(pass.Hash != "");

    }

    [Fact]
    public void ShouldImplicitConvertToString()
    {
        string plainText = "12345678901234567890";

        var pass = Password.ShouldCreate(plainText);

        string pass2 = pass;

        Assert.Equal(pass.Hash, pass2);
    }

    [Fact]
    public void ShouldReturnHashAsStringWhenCallToStringMethod()
    {
        string plainText = "12345678901234567890";

        var pass = Password.ShouldCreate(plainText);

        Assert.Equal(pass.Hash, pass.ToString());
    }

    [Fact]
    public void ShouldMarkPasswordAsExpired() => Assert.Fail();

    [Fact]
    public void ShouldFailIfPasswordIsExpired()
    {
        string plainText = "12345678901234567890";

        var pass = Password.ShouldCreate(plainText);

        Assert.True(pass.Hash != "");
    }

    [Fact]
    public void ShouldMarkPasswordAsMustChange() => Assert.Fail();

    [Fact]
    public void ShouldFailIfPasswordIsMarkedAsMustChange()
    {
        Assert.Fail();

        //string plainText = "12345678901234567890";

        //var pass = Password.ShouldCreate(plainText);

        //Assert.False(pass.MustChange);
    }
}