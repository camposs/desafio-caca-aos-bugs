using System.Net;
using System.Text.RegularExpressions;
using Balta.Domain.AccountContext.ValueObjects.Exceptions;
using Balta.Domain.SharedContext;
using Balta.Domain.SharedContext.Abstractions;
using Balta.Domain.SharedContext.Extensions;
using Balta.Domain.SharedContext.ValueObjects;

namespace Balta.Domain.AccountContext.ValueObjects;

public partial record Email : ValueObject
{
    #region Constants

    private const string Pattern = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";

    #endregion

    #region Constructors

    private Email(string address, string hash, VerificationCode verificationCode)
    {
        Address = address;
        Hash = hash;
        VerificationCode = verificationCode;
    }

    #endregion

    #region Factories

    public static Email ShouldCreate(string address, IDateTimeProvider dateTimeProvider)
    {
        address = address.Trim();
        address = address.ToLower();

        if (!EmailRegex().IsMatch(address))
            throw new InvalidEmailException();

        var verificationCode = VerificationCode.ShouldCreate(dateTimeProvider);

        return new Email(address, address.ToBase64(), verificationCode);
    }

    #endregion

    #region Properties

    public string Address { get; }
    public string Hash { get; }
    public VerificationCode VerificationCode { get; }

    #endregion

    #region Methods

    public void ShouldVerify(string verificationCode) => VerificationCode.ShouldVerify(verificationCode);

    #endregion

    #region Operators

    public static implicit operator string(Email email)
        => email.ToString();

    public static implicit operator Email(string email)
    {
        var dateTimeProvider = new DateTimeProvider();
        var verificationCode = VerificationCode.ShouldCreate(dateTimeProvider);

        return new Email(email, email.ToBase64(), verificationCode);

    }


    #endregion

        #region Overrides

    public override string ToString() => Address;

    #endregion

    #region Other

    [GeneratedRegex(Pattern)]
    private static partial Regex EmailRegex();

    #endregion
}