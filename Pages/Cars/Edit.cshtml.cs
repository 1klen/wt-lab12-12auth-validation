using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using wt_lab5_5mvc_leshkevich.Data;
using wt_lab5_5mvc_leshkevich.Models;

namespace wt_lab5_5mvc_leshkevich.Pages.Cars;

public class EditModel : PageModel
{
    private readonly AppDbContext _context;

    public EditModel(AppDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Car Car { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var car = await _context.Cars.FirstOrDefaultAsync(x => x.Id == id);
        if (car == null)
        {
            return NotFound();
        }

        Car = car;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var existingCar = await _context.Cars.FirstOrDefaultAsync(x => x.Id == Car.Id);
        if (existingCar == null)
        {
            return NotFound();
        }

        existingCar.BrandModel = Car.BrandModel;
        existingCar.Category = Car.Category;
        existingCar.PricePerDay = Car.PricePerDay;
        existingCar.IsAvailable = Car.IsAvailable;

        await _context.SaveChangesAsync();

        return RedirectToPage("Index");
    }
}