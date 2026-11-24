using HubSpot.NET.Core;
using Xunit;

namespace HubSpot.NET.Tests.Core
{
    public class RequestSignatureTests
    {
        [Fact]
        public void Compute_ReturnsExpectedHexDigest_ForKnownInput()
        {
            string signature = RequestSignature.Compute("app-secret", "{\"vid\":123}");

            Assert.Equal(32, signature.Length);
            Assert.Equal("38a48af1140c609a6a0028196b33fca7", signature);
        }

        [Fact]
        public void Compute_ReturnsDifferentDigests_ForDifferentInputs()
        {
            string first = RequestSignature.Compute("app-secret", "{\"vid\":123}");
            string otherBody = RequestSignature.Compute("app-secret", "{\"vid\":124}");
            string otherSecret = RequestSignature.Compute("other-secret", "{\"vid\":123}");

            Assert.NotEqual(first, otherBody);
            Assert.NotEqual(first, otherSecret);
        }
    }
}
