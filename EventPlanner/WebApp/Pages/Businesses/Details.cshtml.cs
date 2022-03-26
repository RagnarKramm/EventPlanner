using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.DAL;
using WebApp.Domain;

namespace WebApp.Pages.Businesses
{
    public class DetailsModel : PageModel
    {
        private readonly AppDbContext _context;

        public DetailsModel(AppDbContext context)
        {
            _context = context;
        }

        public Business? Business { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Business = await _context.GetBusinessById(id);

            if (Business == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}