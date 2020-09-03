using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lab.AppConfiguration.Application;
using Lab.AppConfiguration.Infrastructure;
using Lab.AppConfiguration.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.FeatureManagement;
using Microsoft.FeatureManagement.Mvc;

namespace Lab.AppConfiguration.Pages
{
    // Eigenes Feature Gate-Attribut.
    [FeatureGateOnPage("NewContactForm")]
    public class ContactNewModel : PageModel
    {
        private readonly IContactService contactService;
        public ContactNewModel(IContactService contactService)
        {
            this.contactService = contactService;
        }

        [BindProperty]
        public ContactRequest ContactRequest { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await contactService.SendContactRequest(ContactRequest.Email, ContactRequest.Text);

            return RedirectToPage("./Index");
        }

        
    }
}
