using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Order.Domain.Users;
using Order.Services.Users;

namespace Order.Api.Helper
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly IUserService _userService;
		private readonly ILogger<BasicAuthenticationHandler> _logger;

		public BasicAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
			ILogger<BasicAuthenticationHandler> nlogger,
            IUserService userService)
            : base(options, logger, encoder, clock)
        {
            _userService = userService;
			_logger = nlogger;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey("Authorization"))
            {
				_logger.LogError(Guid.NewGuid() + " Missing Authorization Header");

				return AuthenticateResult.Fail(Guid.NewGuid() + " Missing Authorization Header");
            }

            User user = null;
            try
            {
                var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
                var credentialBytes = Convert.FromBase64String(authHeader.Parameter);
                var credentials = Encoding.UTF8.GetString(credentialBytes).Split(':');
                var username = credentials[0];
                var password = credentials[1];

                user = await _userService.Authenticate(username, password);
            }
            catch
            {
				_logger.LogError(Guid.NewGuid() + " Invalid Authorization Header");

				return AuthenticateResult.Fail(Guid.NewGuid() + " Invalid Authorization Header");
            }

            if (user == null)
            {
				_logger.LogError(Guid.NewGuid() + " Invalid Username or Password");

				return AuthenticateResult.Fail(Guid.NewGuid() + " Invalid Username or Password");
            }

            var claims = new List<Claim> {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Name, user.Email),
				new Claim(ClaimTypes.Role, user.UserRole.ToString())
            };

            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            return AuthenticateResult.Success(ticket);
        }
    }
}

