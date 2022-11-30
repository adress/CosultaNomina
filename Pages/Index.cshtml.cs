using CosultaNomina.Data;
using CosultaNomina.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CosultaNomina.Pages;

public class IndexModel : PageModel
{
    private readonly ApplicationDbContext _context;
    public List<RegSolicitudesIngresos> Empleados { get; set; } = new();

    public IndexModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public void OnGet()
    {
        Empleados = _context.RegSolicitudesIngresos.FromSql<RegSolicitudesIngresos>($"SPR_CONSULTA_EMPLEADOS").ToList();
    }
}
