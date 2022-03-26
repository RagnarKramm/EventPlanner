using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.DAL;
using WebApp.Domain;

namespace WebApp.Pages.Events
{
    public class DetailsModel : PageModel
    {
        private readonly AppDbContext _context;

        public Event? Event { get; set; }
        public IList<Business>? Businesses { get; set; }
        public IList<Person>? Persons { get; set; }

        public DetailsModel(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            Businesses = await _context.GetBusinessesForEventAsync(id);
            Persons = await _context.GetPersonsForEventAsync(id);

            Event = await _context.GetEventById(id);

            if (Event == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}