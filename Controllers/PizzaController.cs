using ContosoPizza.Models;
using Microsoft.AspNetCore.Mvc;
using ContosoPizza.Services;


namespace ContosoPizza.Controllers;

[ApiController]
[Route("[controller]")]
public class PizzaController : ControllerBase
{
    [HttpGet(Name = "GetPizzas")]
    public IEnumerable<Pizza> Get() => PizzaService.GetAll();

    [HttpGet("{id}")]
    public ActionResult<Pizza> Get(int id)
    {
        var pizza = PizzaService.Get(id);
        if (pizza == null)
            return NotFound();
        return pizza;
    }

    [HttpPost]
    public IActionResult Post(Pizza pizza)
    {
        PizzaService.Add(pizza);
        return CreatedAtAction(nameof(Post), new { id = pizza.Id }, pizza);
    }

    [HttpPut]
    public IActionResult Update(int id, Pizza pizza)
    {
        if( id != pizza.Id)
            return BadRequest();
        
        if (PizzaService.Get(pizza.Id) is null)
            return NotFound();

        PizzaService.Update(pizza);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        if (PizzaService.Get(id) is null)
            return NotFound();

        PizzaService.Delete(id);
        return NoContent();
    }
}
