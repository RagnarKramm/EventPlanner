using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp.DAL;
using WebApp.Domain;

namespace WebApp.Pages.Businesses
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
            Event = await _context.GetEventById(eventId) ?? Event;

            PaymentOptionsSelectList = new SelectList(_context.PaymentOptions, nameof(PaymentOption.Id),
                nameof(PaymentOption.Name));

            return Page();
        }

        [BindProperty] public Business? Business { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _context.AddBusinessAsync(Business!);

            return RedirectToPage("/Index");
        }
    }
}