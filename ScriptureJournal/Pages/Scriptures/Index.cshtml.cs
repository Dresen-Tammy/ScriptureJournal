using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ScriptureJournal.Models;

namespace ScriptureJournal.Pages.Scriptures
{
    public class IndexModel : PageModel
    {
        private readonly ScriptureJournal.Models.ScriptureJournalContext _context;

        public IndexModel(ScriptureJournal.Models.ScriptureJournalContext context)
        {
            _context = context;
        }

        public List<Scripture> Scripture { get; set; }
        public IEnumerable<IEnumerable<Scripture>> ScriptureList;
        public string SearchString { get; set; }
        public SelectList Books { get; set; }
        public string ScriptureBook { get; set; }
        // new for sorting
        public string NameSort { get; set; }
        public string DateSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }

        public async Task OnGetAsync(string scriptureBook, string searchString, int? sort)
        {
            IQueryable<string> bookQuery = from m in _context.Scripture
                                           orderby m.Book
                                           select m.Book;
            IOrderedQueryable<Scripture> scriptureList;
            if (sort == 2)
            {
                // using LINQ to get list of books.
                scriptureList = from m in _context.Scripture
                                orderby m.DateAdded
                                select m;
            }
            else
            {
                // using LINQ to get list of books.
                scriptureList = from m in _context.Scripture
                                orderby m.Book
                                select m;
            }
            
            
            // using System.Linq


            if (!String.IsNullOrEmpty(searchString))
            {
                scriptureList = (IOrderedQueryable<Scripture>)scriptureList.Where(s => s.Note.Contains(searchString));
            }
            if (!String.IsNullOrEmpty(scriptureBook))
            {
                scriptureList = (IOrderedQueryable<Scripture>)scriptureList.Where(x => x.Book == scriptureBook);
            } 
            

            Books = new SelectList(await bookQuery.Distinct().ToListAsync());
            Scripture = await scriptureList.ToListAsync();
            SearchString = searchString;
            ScriptureBook = scriptureBook;
            var result = Scripture.Select((x, i) => new { Group = i / 5, Value = x })
                .GroupBy(item => item.Group, g => g.Value)
                .Select(g => g.Where(x => true));
            ScriptureList = result;
        }
    }
}
