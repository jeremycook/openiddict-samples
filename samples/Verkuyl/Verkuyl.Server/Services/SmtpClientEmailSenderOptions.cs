namespace Verkuyl.Server.Services
{
    public class SmtpClientEmailSenderOptions
    {
        public string From { get; set; } = "no-reply@example.com";

        public string Host { get; set; } = "localhost";

        public int Port { get; set; } = 25;

        public bool EnableSsl { get; set; } = false;

        public bool UseDefaultCredentials { get; set; } = false;

        public SmtpClientEmailSenderOptionsNetworkCredential? NetworkCredential { get; set; }

        public class SmtpClientEmailSenderOptionsNetworkCredential
        {
            public string? Username { get; set; }
            public string? Password { get; set; }
            public string? Domain { get; set; }
        }
    }
}
