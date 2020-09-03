using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lab.AppConfiguration.Application;
using Lab.AppConfiguration.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Lab.AppConfiguration.Pages
{
    public class ContactModel : PageModel
    {

        private readonly IContactService contactService;

        public ContactModel(IContactService contactService)
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
