using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.DAL;
using WebApp.Domain;

namespace WebApp.Pages.Events
{
    public class CreateModel : PageModel
    {
        private readonly AppDbContext _context;

        public string ErrorMessage { get; private set; } = default!;

        public CreateModel(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty] public Event? Event { get; set; }

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