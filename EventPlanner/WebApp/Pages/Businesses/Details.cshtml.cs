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

namespace WebApp.Pages.Businesses
{
    public class DetailsModel : PageModel
    {
        private readonly WebApp.DAL.AppDbContext _context;

        public DetailsModel(WebApp.DAL.AppDbContext context)
        {
            _context = context;
        }

        public Business Business { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Business = await _context.Businesses
                .Include(b => b.Event)
                .Include(b => b.PaymentOption).FirstOrDefaultAsync(m => m.Id == id);

            if (Business == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
