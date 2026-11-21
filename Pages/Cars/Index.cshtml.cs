using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using wt_lab5_5mvc_leshkevich.Data;
using wt_lab5_5mvc_leshkevich.Models;

namespace wt_lab5_5mvc_leshkevich.Pages.Cars;

public class IndexModel : PageModel
{
    private readonly AppDbContext _context;

    public IndexModel(AppDbContext context)
    {
        _context = context;
    }

    [BindProperty(SupportsGet = true)]
    public string? Search { get; set; }

    public List<Car> Cars { get; set; } = new();

    public async Task OnGetAsync()
    {
        var query = _context.Cars.AsQueryable();

        if (!string.IsNullOrWhiteSpace(Search))
        {
            query = query.Where(c => EF.Functions.ILike(c.BrandModel, $"%{Search.Trim()}%"));
        }

        Cars = await query
            .OrderBy(c => c.BrandModel)
            .ToListAsync();
    }
}