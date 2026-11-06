using System.Text.Encodings.Web;

namespace SafeVault.API.Services
{
    public static class InputSanitizer
    {
        public static string Sanitize(string input)
        {
            if (string.IsNullOrEmpty(input)) return input;
            // Codifica etiquetas HTML para prevenir ataques XSS
            return HtmlEncoder.Default.Encode(input);
        }
    }
}
