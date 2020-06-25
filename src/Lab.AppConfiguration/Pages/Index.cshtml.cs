using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lab.AppConfiguration.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Lab.AppConfiguration.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        public AppSettings AppSettings { get; set; }

        public IndexModel(ILogger<IndexModel> logger, IOptionsSnapshot<AppSettings> appSettings)
        {
            AppSettings = appSettings.Value;
            _logger = logger;
        }

        public void OnGet()
        {

        }
    }
}
