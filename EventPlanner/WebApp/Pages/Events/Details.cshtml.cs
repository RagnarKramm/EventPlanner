
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApp.DAL;
using WebApp.Domain;

namespace WebApp.Pages.Events
{
    public class DetailsModel : PageModel
    {
        private readonly AppDbContext _context;

        public DetailsModel(AppDbContext context)
        {
            _context = context;
        }

        public Event? Event { get; set; }
        public IList<Participant>? Participant { get;set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            
            // Participant = await _context.Participants
            //     .Include(p => p.Event)
            //     .Include(p => p.ParticipantType)
            //     .Include(p => p.PaymentOption).Where(participant => participant.EventId == id).ToListAsync();
            //
            if (id == null)
            {
                return NotFound();
            }

            Event = await _context.Events.FirstOrDefaultAsync(m => m.Id == id);

            if (Event == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
