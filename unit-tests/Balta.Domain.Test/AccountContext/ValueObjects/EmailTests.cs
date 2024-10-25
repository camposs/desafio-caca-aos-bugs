using Balta.Domain.AccountContext.ValueObjects;
using Balta.Domain.AccountContext.ValueObjects.Exceptions;
using Balta.Domain.SharedContext;
using Balta.Domain.SharedContext.Abstractions;
using Balta.Domain.SharedContext.Extensions;
using System.Net.Mail;

namespace Balta.Domain.Test.AccountContext.ValueObjects;
public class EmailTests
{
    [Fact]
    public void ShouldLowerCaseEmail()
    {
        string emailAddress = "TESTE@BUGS.COM";
        var dateTimeProvider = new DateTimeProvider();

        var email = Email.ShouldCreate(emailAddress, dateTimeProvider);

        Assert.Equal("teste@bugs.com", email);
    }

    [Fact]
    public void ShouldTrimEmail()
    {
        string emailAddress = "     teste@bugs.com                   ";
        var dateTimeProvider = new DateTimeProvider();

        var email = Email.ShouldCreate(emailAddress, dateTimeProvider);

        Assert.Equal("teste@bugs.com", email);
    }

    [Fact]
    public void ShouldFailIfEmailIsNull()
    {

        string emailAddress = null;
        var dateTimeProvider = new DateTimeProvider();
        var exceptionType = typeof(NullReferenceException);

        Assert.Throws(exceptionType, () =>
        {
            Email.ShouldCreate(emailAddress, dateTimeProvider);
        });
    }

    [Fact]
    public void ShouldFailIfEmailIsEmpty()
    {

        string emailAddress = "";
        var dateTimeProvider = new DateTimeProvider();
        var exceptionType = typeof(InvalidEmailException);

        Assert.Throws(exceptionType, () =>
        {
            Email.ShouldCreate(emailAddress, dateTimeProvider);
        });
    }

    [Fact]
    public void ShouldFailIfEmailIsInvalid()
    {

        string emailAddress = "teste";
        var dateTimeProvider = new DateTimeProvider();
        var exceptionType = typeof(InvalidEmailException);

        Assert.Throws(exceptionType, () =>
        {
            Email.ShouldCreate(emailAddress, dateTimeProvider);
        });
    }

    [Fact]
    public void ShouldPassIfEmailIsValid()
    {
        string emailAddress = "teste@bugs.com";
        var dateTimeProvider = new DateTimeProvider();

        var email = Email.ShouldCreate(emailAddress, dateTimeProvider);

        Assert.True(email != null);
    }

    [Fact]
    public void ShouldHashEmailAddress()
    {
        string emailAddress = "teste@bugs.com";
        var dateTimeProvider = new DateTimeProvider();

        var email = Email.ShouldCreate(emailAddress, dateTimeProvider);

        Assert.Equal(emailAddress.ToBase64(), email.Hash);
    }

    [Fact]
    public void ShouldExplicitConvertFromString()
    {
        string emailAddress = "teste@bugs.com";
        var dateTimeProvider = new DateTimeProvider();

        var email = Email.ShouldCreate(emailAddress, dateTimeProvider);

        var email2 = (Email)emailAddress;

        Assert.Equal(email.Address, email2.Address);
    }

    [Fact]
    public void ShouldExplicitConvertToString()
    {
        string emailAddress = "teste@bugs.com";
        var dateTimeProvider = new DateTimeProvider();

        var email = Email.ShouldCreate(emailAddress, dateTimeProvider);

        string email2 = Convert.ToString(email.Address);

        Assert.Equal(emailAddress, email2);

    }

    [Fact]
    public void ShouldReturnEmailWhenCallToStringMethod()
    {
        string emailAddress = "teste@bugs.com";
        var dateTimeProvider = new DateTimeProvider();

        var email = Email.ShouldCreate(emailAddress, dateTimeProvider);

        string email2 = email.ToString();

        Assert.Equal(emailAddress, email2);

    }
}