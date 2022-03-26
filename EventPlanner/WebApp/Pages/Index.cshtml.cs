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

        public async Task OnGetAsync()
        {
            Event = await _context.GetEventsAndParticipantsAsync();
        }

        public static int GetParticipantCount(Event theEvent)
        {
            var participantCount = 0;

            if (theEvent.Businesses != null)
            {
                foreach (var business in theEvent.Businesses)
                {
                    participantCount += Int32.Parse(business.ParticipantCount.ToString());
                }
            }
           
            if (theEvent.Persons != null) participantCount += theEvent.Persons.Count;

            return participantCount;
        }
        
        public static bool IsInFuture(DateTime dateTime)
        {
            return dateTime.CompareTo(DateTime.Now) > 0;
        }
    }
}