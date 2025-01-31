using System.Security.Claims;
using Amazon.CognitoIdentityProvider.Model;
using Ecommerce.Interfaces;

namespace Ecommerce.API.Middleware
{
    public class AuthMiddleware(RequestDelegate next)
    {
        readonly RequestDelegate _next = next;
        public async Task Invoke(HttpContext context, IRolService rolService)
        {
            if (context.User.Identity?.IsAuthenticated == true)
            {
                var firstclaim = context.User.Identities.First();
                var cognitoId = firstclaim.FindFirst("cognito:username")?.Value;

                var rol = await rolService.GetRolByCognitoIdAsync(cognitoId);

                if (!string.IsNullOrEmpty(rol.Descripcion))
                {
                    var claimsIdentity = (ClaimsIdentity)context.User.Identity;
                    claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, rol.Descripcion));
                }

            }

            await _next(context);
        }
    }
}
