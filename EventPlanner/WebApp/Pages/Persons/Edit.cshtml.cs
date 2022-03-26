using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApp.DAL;
using WebApp.Domain;

namespace WebApp.Pages.Persons
{
    public class EditModel : PageModel
    {
        private readonly AppDbContext _context;

        public SelectList? PaymentOptionsSelectList { get; set; }

        public EditModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty] public Person? Person { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            PaymentOptionsSelectList = new SelectList(_context.PaymentOptions, nameof(PaymentOption.Id),
                nameof(PaymentOption.Name));

            Person = await _context.GetPersonById(id);

            if (Person == null)
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

            try
            {
                await _context.EditPersonAsync(Person!);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonExists(Person!.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Details", new {id = Person!.Id});
        }

        private bool PersonExists(int id)
        {
            return _context.Persons.Any(e => e.Id == id);
        }
    }
}