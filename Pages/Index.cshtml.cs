using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using razorweb.models;
using System.Linq;

namespace razorweb.Pages;

public class IndexModel : PageModel
{
    #pragma warning disable IDE0052
    private readonly ILogger<IndexModel> _logger;
    #pragma warning restore IDE0052
    
    private readonly MyBlogContext myBlogContext;
    public IndexModel(ILogger<IndexModel> logger, MyBlogContext _myBlogContext)
    {
        _logger = logger;
        myBlogContext = _myBlogContext;
    }

    public void OnGet()
    {
        var posts = (from a in myBlogContext.articles
                    orderby a.Created descending
                    select a).ToList();

        ViewData["posts"] = posts;
    }
}
