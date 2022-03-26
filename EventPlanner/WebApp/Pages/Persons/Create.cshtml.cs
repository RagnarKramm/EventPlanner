
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApp.DAL;
using WebApp.Domain;

namespace WebApp.Pages.Persons
{
    public class CreateModel : PageModel
    {
        private readonly AppDbContext _context;

        public Event Event { get; set; } = default!;
        
        public SelectList? PaymentOptionsSelectList { get; set; }

        public CreateModel(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGet(int eventId)
        {
            Event = await _context.Events.FirstAsync(item => item.Id == eventId);
            
            PaymentOptionsSelectList = new SelectList(_context.PaymentOptions, nameof(PaymentOption.Id), nameof(PaymentOption.Name));

            return Page();
        }

        [BindProperty]
        public Person? Person { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _context.AddPersonAsync(Person!);

            return RedirectToPage("/Index");
        }
    }
}
