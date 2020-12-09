using MovieShop.Core.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;
using System.Linq;
namespace MovieShop.Infrastructure.Services
{
    public class CurrentUserService : ICurrentUserService
    {


        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string Name => GetName();

        public int? UserId => GetUserId();
        public bool IsAuthenticated => GetAuthenticated();
        public string UserName => _httpContextAccessor.HttpContext.User.Identity.Name;

        public string FullName => _httpContextAccessor.HttpContext.User.Claims
                                                      .FirstOrDefault(c => c.Type == ClaimTypes.GivenName)
                                                      ?.Value + " " + _httpContextAccessor.HttpContext.User.Claims
                                                                                          .FirstOrDefault(c =>
                                                                                                              c.Type ==
                                                                                                              ClaimTypes
                                                                                                                  .Surname)
                                                                                          ?.Value;

        public string Email => _httpContextAccessor.HttpContext.User.Claims
                                                   .FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

        public string RemoteIpAddress => _httpContextAccessor.HttpContext.Connection?.RemoteIpAddress.ToString();

        public IEnumerable<Claim> GetClaimsIdentity()
        {
            return _httpContextAccessor.HttpContext.User.Claims;
        }

        public IEnumerable<string> Roles => GetRoles();

        private int? GetUserId()
        {
            var userId =
                Convert.ToInt32(_httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier).Value);
            return userId;
        }

        private bool GetAuthenticated()
        {
            return _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated;
        }

        private string GetName()
        {
            return _httpContextAccessor.HttpContext.User.Identity.Name ??
                   _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
        }

        private IEnumerable<string> GetRoles()
        {
            var claims = GetClaimsIdentity();
            var roles = new List<string>();
            foreach (var claim in claims)
                if (claim.Type == ClaimTypes.Role)
                    roles.Add(claim.Value);
            return roles;
        }
    }
}
