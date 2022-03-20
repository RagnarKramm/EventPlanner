
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApp.DAL;
using WebApp.Domain;

namespace WebApp.Pages.Participants
{
    public class IndexModel : PageModel
    {
        private readonly WebApp.DAL.AppDbContext _context;

        public IndexModel(WebApp.DAL.AppDbContext context)
        {
            _context = context;
        }

        public IList<Participant>? Participant { get;set; }

        public async Task OnGetAsync()
        {
            Participant = await _context.Participants
                .Include(p => p.Event)
                .Include(p => p.ParticipantType)
                .Include(p => p.PaymentOption).ToListAsync();
        }
    }
}
