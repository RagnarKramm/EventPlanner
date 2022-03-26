using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApp.DAL;
using WebApp.Domain;

namespace WebApp.Pages.Events
{
    public class EditModel : PageModel
    {
        private readonly AppDbContext _context;

        public string ErrorMessage { get; private set; } = default!;

        public EditModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty] public Event? Event { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Event = await _context.GetEventById(id);

            if (Event == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (Event!.HappeningAt.CompareTo(DateTime.Now) < 0)
            {
                ErrorMessage = "Valitud aeg on minevikus, palun valige aeg mis on veel tulemas!";
                return Page();
            }

            try
            {
                await _context.EditEventAsync(Event);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventExists(Event!.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("/Index");
        }

        private bool EventExists(int id)
        {
            return _context.Events.Any(e => e.Id == id);
        }
    }
}