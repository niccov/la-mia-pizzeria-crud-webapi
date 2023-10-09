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

        [HttpPost]
        public IActionResult Create([FromBody] Pizza newPizza)
        {
            using (PizzeriaContext db = new PizzeriaContext())
            {
                try
                {
                    db.Pizze.Add(newPizza);
                    db.SaveChanges();

                    return Ok();

                }catch(Exception ex)
                {
                    return BadRequest(new { Message = ex.Message });
                }
            }
        }

        [HttpPut("{id}")]
        public IActionResult Modify(int id, [FromBody] Pizza updatedPizza)
        {
            using (PizzeriaContext db = new PizzeriaContext())
            {
                Pizza? pizzaToUpdate = db.Pizze.Where(Pizza => Pizza.Id == id).FirstOrDefault();

                if(pizzaToUpdate == null)
                {
                    return NotFound();
                }

                pizzaToUpdate.Nome = updatedPizza.Nome;
                pizzaToUpdate.Descrizione = updatedPizza.Descrizione;
                pizzaToUpdate.Prezzo = updatedPizza.Prezzo;
                pizzaToUpdate.Foto = updatedPizza.Foto;
                pizzaToUpdate.categoriaId = updatedPizza.categoriaId;
                pizzaToUpdate.Ingredienti = updatedPizza.Ingredienti;

                db.SaveChanges();

                return Ok();
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            using (PizzeriaContext db = new PizzeriaContext())
            {
                Pizza? pizzaDaCancellare = db.Pizze.Where(pizza => pizza.Id == id).FirstOrDefault();

                if(pizzaDaCancellare == null)
                {
                    return NotFound();
                }

                db.Pizze.Remove(pizzaDaCancellare);
                db.SaveChanges();

                return Ok();
            }
        }
    }
}
