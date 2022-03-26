using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.DAL;
using WebApp.Domain;

namespace WebApp.Pages.Persons
{
    public class DeleteModel : PageModel
    {
        private readonly AppDbContext _context;

        public DeleteModel(AppDbContext context)
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

            Person = await _context.GetPersonById(id);

            if (Person == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (Person != null)
            {
                await _context.DeletePersonAsync(id);
            }

            return RedirectToPage("/Events/Details", new {id = Person?.EventId});
        }
    }
}