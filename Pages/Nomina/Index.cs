using System.Runtime.CompilerServices;
using CosultaNomina.Data;
using CosultaNomina.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace ConsultaNomina.Pages.Nomina
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public List<ConceptoNomina> ConceptosNominas { get; set; } = new();

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public void OnGet(string documento)
        {
            var parameters = new List<object>();
            parameters.Add(new SqlParameter("identidad", documento));
            parameters.Add(new SqlParameter("evento", 1));
            parameters.Add(new SqlParameter("usuario", "ADMIN"));

            var query = FormattableStringFactory
                .Create(
                    "USP_CONSULTA_NOMINA_POR_DOCUMENTO @identidad, @evento, @usuario",
                    parameters.ToArray()
                );

            ConceptosNominas = _context.ConceptoNomina.FromSql<ConceptoNomina>(query).ToList();
        }
    }
}
