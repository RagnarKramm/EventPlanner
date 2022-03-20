#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApp.DAL;
using WebApp.Domain;

namespace WebApp.Pages.PaymentOptions
{
    public class DeleteModel : PageModel
    {
        private readonly AppDbContext _context;

        public DeleteModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public PaymentOption PaymentOption { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            PaymentOption = await _context.PaymentOptions.FirstOrDefaultAsync(m => m.Id == id);

            if (PaymentOption == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            PaymentOption = await _context.PaymentOptions.FindAsync(id);

            if (PaymentOption != null)
            {
                _context.PaymentOptions.Remove(PaymentOption);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
