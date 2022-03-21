using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApp.DAL;
using WebApp.Domain;

namespace WebApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _context;

        public IndexModel(AppDbContext context)
        {
            _context = context;
        }

        public IList<Event>? Event { get;set; }
        public IList<ParticipantType>? ParticipantTypes { get; set; }

        public async Task OnGetAsync()
        {
            Event = await _context.Events.Include(item => item.Participants)!.ThenInclude(participant => participant.ParticipantType).ToListAsync();
            ParticipantTypes = await _context.ParticipantTypes.ToListAsync();
        }
    }
}