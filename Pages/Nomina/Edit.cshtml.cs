using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using CosultaNomina.Data;
using CosultaNomina.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace ConsultaNomina.Pages.Nomina
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public string Registro { get; set; } = "";
        public string Documento { get; set; } = "";

        [Required]
        [BindProperty]
        [Display(Name = "Valor Nomina")]
        public int ValNomina { get; set; }

        public EditModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public void OnGet(string registro, string documento)
        {

            var parameters = new List<object>();
            parameters.Add(new SqlParameter("registro", registro));

            var query = FormattableStringFactory
                .Create(
                    "SELECT VAL_NOMINA FROM NOM_NOMINA_DEFINITIVA WHERE REGISTRO = @registro",
                    parameters.ToArray()
                );

            ValNomina = _context.Database
            .SqlQuery<int>(query)
            .ToList().FirstOrDefault();

            Registro = registro;
            Documento = documento;
        }


        public IActionResult OnPost(int registro, string documento)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var parameters = new List<object>();
            parameters.Add(new SqlParameter("identidad", documento));
            parameters.Add(new SqlParameter("evento", 2));
            parameters.Add(new SqlParameter("usuario", "ADMIN"));
            parameters.Add(new SqlParameter("registro", registro));
            parameters.Add(new SqlParameter("cantidad", ValNomina));

            var query = FormattableStringFactory
                .Create(
                    "USP_CONSULTA_NOMINA_POR_DOCUMENTO @identidad, @evento, @usuario, @registro, @cantidad",
                    parameters.ToArray()
                );

            _context.Database.ExecuteSql(query);

            return RedirectToPage("Index", new { documento });
        }
    }
}
