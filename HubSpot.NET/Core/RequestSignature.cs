namespace HubSpot.NET.Core
{
    using System;
    using System.Security.Cryptography;
    using System.Text;

    /// <summary>
    /// Computes the signature sent with webhook payloads so that a receiver can confirm the body
    /// was produced by the configured HubSpot application.
    /// </summary>
    public static class RequestSignature
    {
        public static string Compute(string appSecret, string body)
        {
            if (appSecret == null) throw new ArgumentNullException(nameof(appSecret));
            if (body == null) throw new ArgumentNullException(nameof(body));

            using (HMACSHA256 hmac = new HMACSHA256(Encoding.UTF8.GetBytes(appSecret)))
            {
                byte[] hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(body));
                StringBuilder sb = new StringBuilder(hash.Length * 2);
                foreach (byte b in hash)
                {
                    sb.Append(b.ToString("x2"));
                }
                return sb.ToString();
            }
        }
    }
}
