using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace MovieShop.Core.ServiceInterfaces
{
    public interface ICurrentUserService
    {
        int? UserId { get; }
        bool IsAuthenticated { get; }
        string UserName { get; }
        string FullName { get; }
        string Email { get; }
        string RemoteIpAddress { get; }
        IEnumerable<Claim> GetClaimsIdentity();
        IEnumerable<string> Roles { get; }
    }
}
