using System.Runtime.CompilerServices;
using CosultaNomina.Data;
using CosultaNomina.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace ConsultaNomina.Pages.Nomina
{
    public class DeleteModel : PageModel
    {

        private readonly ApplicationDbContext _context;
        public string Registro { get; set; } = "";
        public string Documento { get; set; } = "";

        public DeleteModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public void OnGet(string registro, string documento)
        {
            Registro = registro;
            Documento = documento;
        }

        public IActionResult OnPost(int registro, int documento)
        {
            var parameters = new List<object>();
            parameters.Add(new SqlParameter("identidad", "0"));
            parameters.Add(new SqlParameter("evento", 3));
            parameters.Add(new SqlParameter("usuario", "ADMIN"));
            parameters.Add(new SqlParameter("registro", registro));

            var query = FormattableStringFactory
                .Create(
                    "USP_CONSULTA_NOMINA_POR_DOCUMENTO @identidad, @evento, @usuario, @registro",
                    parameters.ToArray()
                );

            _context.Database.ExecuteSql(query);
            return RedirectToPage("Index", new { documento });
        }
    }
}
