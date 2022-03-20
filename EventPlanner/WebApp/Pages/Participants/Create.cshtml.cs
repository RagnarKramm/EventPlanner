
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

namespace WebApp.Pages.Participants
{
    public class CreateModel : PageModel
    {
        private readonly WebApp.DAL.AppDbContext _context;
        [BindProperty] public Participant Participant { get; set; } = default!;

        public Event Event { get; set; } = default!;
        public ParticipantType ParticipantType { get; set; } = default!;

        public SelectList? PaymentOptionsSelectList { get; set; }
        public CreateModel(AppDbContext context)
        {
            _context = context;
        }
        
        public string? ErrorMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(int eventId, int participantTypeId)
        {
            ParticipantType = await _context.ParticipantTypes.FirstAsync(type => type.Id == participantTypeId);
            Event = await _context.Events.FirstAsync(item => item.Id == eventId);

            PaymentOptionsSelectList = new SelectList(_context.PaymentOptions, nameof(PaymentOption.Id), nameof(PaymentOption.Name));
            
            return Page();
        }

        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            ParticipantType = await _context.ParticipantTypes.FirstAsync(type => type.Id == Participant.ParticipantTypeId);
            Event = await _context.Events.FirstAsync(item => item.Id == Participant.EventId);

            PaymentOptionsSelectList = new SelectList(_context.PaymentOptions, nameof(PaymentOption.Id), nameof(PaymentOption.Name));
            
            if (!ModelState.IsValid)
            {
                return Page();
            }
            
            if (Participant.ParticipantTypeId == 1 && Participant.AdditionalInformation.Length > 1500)
            {
                ErrorMessage = "Kirjelduse limiit on 1500 märki, palun sisestage vähem kui 1500 märki!";
                return Page();
            }
            

            _context.Participants.Add(Participant);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Events/Index");
        }
    }
}
