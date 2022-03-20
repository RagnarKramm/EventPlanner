#nullable disable

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApp.DAL;
using WebApp.Domain;

namespace WebApp.Pages.Participants
{
    public class CreateModel : PageModel
    {
        private readonly AppDbContext _context;

        [BindProperty]
        public ParticipantType ParticipantType { get; set; }
        public CreateModel(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(int? participantTypeId)
        {
            ParticipantType = await _context.ParticipantTypes.FirstOrDefaultAsync(type => type.Id == participantTypeId);
            
        ViewData["EventId"] = new SelectList(_context.Events, "Id", "Id");
        ViewData["ParticipantTypeId"] = new SelectList(_context.ParticipantTypes, "Id", "Id");
        ViewData["PaymentOptionId"] = new SelectList(_context.PaymentOptions, "Id", "Id");
            return Page();
        }

        [BindProperty]
        public Participant Participant { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Participants.Add(Participant);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
