using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text.Encodings.Web;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;


namespace BasicAuthen.Services
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly ILogger<BasicAuthenticationHandler> _logger;
        public BasicAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock)
            : base(options, logger, encoder, clock)
        {
            _logger = logger.CreateLogger<BasicAuthenticationHandler>();
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey("Authorization"))
            {
                return AuthenticateResult.Fail("Missing Authorization Header");
            }

            try
            {
                var authHeader = Request.Headers["Authorization"].ToString();
                var authHeaderValue = authHeader.Substring("Basic ".Length).Trim();
                var authHeaderValueBytes = Convert.FromBase64String(authHeaderValue);
                var authHeaderValueString = Encoding.UTF8.GetString(authHeaderValueBytes);

                var credentials = authHeaderValueString.Split(':');
                var username = credentials[0];
                var password = credentials[1];

                if (username == "ThangNT" && password == "Aa@123456")
                {
                    var claims = new[] { new Claim(ClaimTypes.Name, username) };
                    var identity = new ClaimsIdentity(claims, Scheme.Name);
                    var principal = new ClaimsPrincipal(identity);
                    var ticket = new AuthenticationTicket(principal, Scheme.Name);

                    return AuthenticateResult.Success(ticket);
                }
                else
                {
                    return AuthenticateResult.Fail("Invalid Username or Password");
                }
            }
            catch
            {
                return AuthenticateResult.Fail("Invalid Authorization Header");
            }
        }
    }
}
