﻿using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using YourTutor.Application.Abstractions;

namespace YourTutor.Infrastructure.Services
{
    public sealed class HttpContextService : IHttpContextService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HttpContextService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Guid GetUserIdFromClaims()
        {
            try
            {
                var httpContext = _httpContextAccessor.HttpContext;
                if (httpContext.User is null || httpContext.User.Claims.Count() == 0)
                    return Guid.Empty;

                var claim = httpContext.User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.NameIdentifier);


                Guid result;

                if (claim is null
                    || string.IsNullOrWhiteSpace(claim.Value)
                    || !Guid.TryParse(claim.Value, out result))
                {
                    return Guid.Empty;
                }
                    
                return result;
            }
            catch
            {
                return Guid.Empty;
            }
        }
    }
}

