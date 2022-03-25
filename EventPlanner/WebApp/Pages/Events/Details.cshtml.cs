
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
        public IList<Business>? Businesses { get;set; }
        public IList<Person>? Persons { get;set; }


        public async Task<IActionResult> OnGetAsync(int? id)
        {
            
            Businesses = await _context.Businesses.Where(business => business.EventId == id).ToListAsync();
            Persons = await _context.Persons.Where(person => person.EventId == id).ToListAsync();

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

        public bool IsInFuture(DateTime dateTime)
        {
            return dateTime.CompareTo(DateTime.Now) > 0;
        }
    }
}
