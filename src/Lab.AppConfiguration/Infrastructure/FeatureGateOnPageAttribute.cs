using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.FeatureManagement;

namespace Lab.AppConfiguration.Infrastructure
{
    public class FeatureGateOnPageAttribute : Attribute, IAsyncPageFilter
    {
        private readonly string feature;

        public FeatureGateOnPageAttribute(string feature)
        {
            this.feature = feature;
        }

        public async Task OnPageHandlerExecutionAsync(PageHandlerExecutingContext context, PageHandlerExecutionDelegate next)
        {
            var featureManager = (IFeatureManagerSnapshot)context.HttpContext.RequestServices.GetService(typeof(IFeatureManagerSnapshot));

            var isFeatureActive = await featureManager.IsEnabledAsync(this.feature);

            if (!isFeatureActive)
            {
                context.Result = new NotFoundResult();
                return;
            }

            await next();
        }

        public Task OnPageHandlerSelectionAsync(PageHandlerSelectedContext context)
        {
            return Task.CompletedTask;
        }
    }
}