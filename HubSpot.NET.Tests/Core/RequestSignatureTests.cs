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

            Assert.Equal(64, signature.Length);
            Assert.Equal("baba38450498515eae69e2f9982d10738b7814893ec500adba0f8b2ea879993a", signature);
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
