using Xunit;
using SafeVault.API.Services;

namespace SafeVault.Tests
{
    public class SecurityTests
    {
        [Fact]
        public void Sanitize_ShouldPreventXSS_WhenScriptTagIsProvided()
        {
            // Arrange
            string maliciousInput = "<script>alert('hackeado')</script>";

            // Act
            string result = InputSanitizer.Sanitize(maliciousInput);

            // Assert
            Assert.DoesNotContain("<script>", result);
            Assert.Contains("&lt;script&gt;", result);
        }

        [Fact]
        public void Sanitize_ShouldReturnEmpty_WhenInputIsNull()
        {
            // Act
            string result = InputSanitizer.Sanitize(null!);

            // Assert
            Assert.Null(result);
        }
    }
}
