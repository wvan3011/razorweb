using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using razorweb.models;

namespace razorweb.Pages_Blog
{
    public class IndexModel : PageModel
    {
        private readonly MyBlogContext _context;

        public IndexModel(MyBlogContext context)
        {
            _context = context;
        }

        public IList<Article> Article { get;set; } = default!;

        public const int ITEMS_PER_PAGE = 10;

        [BindProperty(SupportsGet = true, Name = "p")]
        public int CurrentPage { get; set; }
        public int CountPages { get; set; }

        public async Task OnGetAsync(string SearchString)
        {
            if (_context.articles != null)
            {
                int totalArticle = await _context.articles.CountAsync();

                CountPages = (int)Math.Ceiling((double)totalArticle / ITEMS_PER_PAGE);

                if (CurrentPage < 1)
                    CurrentPage = 1;
                if (CurrentPage > CountPages)
                    CurrentPage = CountPages;
                

                var qr = (from a in _context.articles
                        orderby a.Created descending
                        select a)
                        .Skip((CurrentPage - 1) * ITEMS_PER_PAGE)
                        .Take(ITEMS_PER_PAGE);
                if (!string.IsNullOrEmpty(SearchString))
                {
                    qr = (IOrderedQueryable<Article>)qr.Where(a => a.Title.Contains(SearchString));
                }
                Article = await qr.ToListAsync();
            }
        }
    }
}
