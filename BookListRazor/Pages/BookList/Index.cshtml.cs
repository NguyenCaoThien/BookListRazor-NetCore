using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookListRazor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BookListRazor.Pages.BookList
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public IndexModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public IEnumerable<Book> Book { get; set; }
        public async Task OnGet()
        {
            Book = await _db.book.ToListAsync();
        }

        public async Task<IActionResult> OnPostDelete(int id)
        {
            Book book = await _db.book.FindAsync(id);

            if(book == null)
            {
                return Page();
            }

            _db.book.Remove(book);
            await _db.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}