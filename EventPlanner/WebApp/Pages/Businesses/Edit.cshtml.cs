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

namespace WebApp.Pages.Businesses
{
    public class EditModel : PageModel
    {
        private readonly AppDbContext _context;

        public SelectList? PaymentOptionsSelectList { get; set; }

        public EditModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty] public Business? Business { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            PaymentOptionsSelectList = new SelectList(_context.PaymentOptions, nameof(PaymentOption.Id), nameof(PaymentOption.Name));


            Business = await _context.Businesses
                .Include(b => b.Event)
                .Include(b => b.PaymentOption).FirstOrDefaultAsync(m => m.Id == id);

            if (Business == null)
            {
                return NotFound();
            }
            
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }


            try
            {
                await _context.EditBusinessAsync(Business!);

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BusinessExists(Business!.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Details", new{ id = Business!.Id});
        }

        private bool BusinessExists(int id)
        {
            return _context.Businesses.Any(e => e.Id == id);
        }
    }
}
