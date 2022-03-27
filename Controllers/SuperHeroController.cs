using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using superhero.Data;

namespace superhero.Controllers;

[ApiController]
[Route("api/[controller]")]

public class SuperHeroController : ControllerBase
{
    private readonly DataContext _context;
    public SuperHeroController(DataContext context)
    {
        _context = context;
    }

    private static List<SuperHero> herous = new List<SuperHero>
        {
            new SuperHero {Id = 1,
             Name = "Spider Man",
             FirstName = "Peter ",
             LastName = "Parker",
             Place = "New York City"},

             new SuperHero {Id = 1,
             Name = "Spider ",
             FirstName = "Jonny ", 
             LastName = "Hard",
             Place = "Oclaxoma City"}
        };

    [HttpGet]
    public async Task<ActionResult<List<SuperHero>>> Get()
    {
        return Ok(await _context.SuperHeros.ToListAsync());
    }

    [HttpGet("{id}")]
    public ActionResult<SuperHero> Get(int id)
    {
        var hero = herous.Find(h=> h.Id == id);

        if(hero==null)
        {
            return BadRequest("Hero not found");
        }


        return Ok(hero);
    }


    [HttpPost]
    public ActionResult<List<SuperHero>> AddHero([FromForm]SuperHero hero)
    {
        herous.Add(hero);
        return Ok(herous);
    }


    [HttpPut]
    public ActionResult<List<SuperHero>> UpdateHero([FromForm]SuperHero request)
    {
        var hero = herous.Find(h=> h.Id == request.Id);

        if(hero==null)
        {
            return BadRequest("Hero not found");
        }

        hero.Name = request.Name;
        hero.FirstName = request.FirstName;
        hero.LastName = request.LastName;
        hero.Place = request.Place;

        return Ok(herous);
    }

    [HttpDelete("{id}")]
    public ActionResult<List<SuperHero>> DeleteHero(int id)
    {
        var hero = herous.Find(h=> h.Id == id);

        if(hero==null)
        {
            return BadRequest("Hero not found");
        }

        herous.Remove(hero);
        return Ok(hero);
    }


}
