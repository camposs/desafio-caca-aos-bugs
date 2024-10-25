using Balta.Domain.AccountContext.ValueObjects;
using Balta.Domain.SharedContext;
using Balta.Domain.SharedContext.Extensions;

namespace Balta.Domain.Test.SharedContext.Extensions;

public class StringExtensionsTests
{
    [Fact]
    public void ShouldGenerateBase64FromString()
    {
        string emailAddress = "teste@bugs.com";
        string base64 = "dGVzdGVAYnVncy5jb20=";

        Assert.Equal(base64, emailAddress.ToBase64());
    }
}