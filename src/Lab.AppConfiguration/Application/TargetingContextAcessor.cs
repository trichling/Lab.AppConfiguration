using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.FeatureManagement;
using Microsoft.FeatureManagement.FeatureFilters;

namespace Lab.AppConfiguration.Application
{
    public class TargetingContextAcessor : ITargetingContextAccessor
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public TargetingContextAcessor(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public ValueTask<TargetingContext> GetContextAsync()
        {
            var httpContext = httpContextAccessor.HttpContext;
            var userIdFromQuery = httpContext.Request.Query["userId"].FirstOrDefault() ?? "MrX";

            var targetingContext = new TargetingContext()
            {
                UserId = userIdFromQuery,
                Groups = new List<string>()
            };

            return new ValueTask<TargetingContext>(targetingContext);
        }
    }
}