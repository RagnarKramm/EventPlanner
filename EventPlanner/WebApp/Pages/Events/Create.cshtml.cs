
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp.DAL;
using WebApp.Domain;

namespace WebApp.Pages.Events
{
    public class CreateModel : PageModel
    {
        private readonly AppDbContext _context;

        public string ErrorMessage { get; set; } = default!;
        public CreateModel(AppDbContext context)
        {
            _context = context;
        }
        
        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Event? Event { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            
            if (!ModelState.IsValid)
            {
                return Page();
            }
            
            if (Event != null && Event!.HappeningAt.CompareTo(DateTime.Now) < 0)
            {
                ErrorMessage = "Valitud aeg on minevikus, palun valige aeg mis on veel tulemas!";
                return Page();
            }

            await _context.AddEventAsync(Event!);

            return RedirectToPage("/Index");
        }
    }
}
