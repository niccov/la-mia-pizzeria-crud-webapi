using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pizzeria_Statica.Database;
using Pizzeria_Statica.Models;

namespace Pizzeria_Statica.Controllers.API
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PizzaApiController : ControllerBase
    {

        [HttpGet]
        public IActionResult GetPizzas()
        {
            using(PizzeriaContext db = new PizzeriaContext())
            {
                List<Pizza> pizze = db.Pizze.Include(pizza => pizza.Categoria).Include(pizza => pizza.Ingredienti).ToList();


                if (pizze == null)
                {
                    return NotFound(new { message = "Nessuna pizza trovata." });
                }
                return Ok(pizze); 
            }
        }

        [HttpGet]
        public IActionResult GetPizzasByName(string? search)
        {
            if (search == null)
            {
                return BadRequest(new { message = "La ricerca non è valida" });
            }

            using (PizzeriaContext db = new PizzeriaContext())
            {
                List<Pizza>? pizze = db.Pizze.Include(pizza => pizza.Categoria).Include(pizza => pizza.Ingredienti).Where(pizza => pizza.Nome.ToLower().Contains(search.ToLower())).ToList();

                if (pizze == null)
                {
                    return NotFound(new { message = "Nessuna pizza trovata." });
                }
                return Ok(pizze);
            }
        }

        [HttpGet]
        public IActionResult GetPizzasById(int? id)
        {
            if (id == null)
            {
                return BadRequest(new { message = "La ricerca non è valida" });
            }

            using (PizzeriaContext db = new PizzeriaContext())
            {
                Pizza? pizzaTrovata = db.Pizze.Include(pizza => pizza.Categoria).Include(pizza => pizza.Ingredienti).Where(pizza => pizza.Id==id).FirstOrDefault();

                if(pizzaTrovata == null)
                {
                    return NotFound(new { message = "Non ho trovato pizze con questo id" });
                }

                return Ok(pizzaTrovata);
            }
        }
    }
}
