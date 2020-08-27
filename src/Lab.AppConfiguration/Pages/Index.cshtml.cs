using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lab.AppConfiguration.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.FeatureManagement;

namespace Lab.AppConfiguration.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IFeatureManagerSnapshot _featureManager;

        public AppSettings AppSettings { get; set; }

        public IndexModel(ILogger<IndexModel> logger, IOptionsSnapshot<AppSettings> appSettings,
                          IFeatureManagerSnapshot featureManager)
        {
            _featureManager = featureManager;
            _logger = logger;

            AppSettings = appSettings.Value;
        }

        public async Task OnGet()
        {
            var isActive = await _featureManager.IsEnabledAsync("NewContactForm");
        }
    }
}
