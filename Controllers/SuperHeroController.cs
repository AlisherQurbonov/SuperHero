using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using superhero.Data;

namespace superhero.Controllers;

[ApiController]
[Route("api/[controller]")]

public class SuperHeroController : ControllerBase
{
    private readonly DataContext _context;
    private readonly ILogger<SuperHeroController> _logger;

    public SuperHeroController(ILogger<SuperHeroController> logger , DataContext context)
    {
        _context = context;
        _logger = logger;
    }
    
   
    [HttpGet]
    public async Task<ActionResult> Get()
    {

        return Ok(await _context.SuperHeros.Where(x=> x.Name == "Database").ToListAsync());

    }

     [HttpGet("{id}")]
    public async  Task<ActionResult> Get(int id)
    {
     
        var hero = await _context.SuperHeros.FirstOrDefaultAsync(i=>i.Id == id);
      
        if(hero==null)
        {
            return BadRequest("Hero not found");
        }


        return Ok(hero);
    }


    [HttpPost]
    public async Task<ActionResult> AddHero([FromForm]NewSuperHero hero)
    {
        var heroues = new SuperHero(
            id : hero.Id,
            name: hero.Name,
            firstName: hero.FirstName,
            lastName: hero.LastName,
            place: hero.Place
        );

           try
            {
                await _context.SuperHeros.AddAsync(heroues);
                await _context.SaveChangesAsync();

                _logger.LogInformation($"New hero added with ID: {heroues.Id}");

                return Ok(new
                {
                     id = hero.Id,
                    name = hero.Name,
                    firstName = hero.FirstName,
                    lastName = hero.LastName,
                    place = hero.Place
                });
            }
            catch(Exception e)
            {
                _logger.LogWarning($"Error occured while saving user to DB:\n{e.Message}");
                return StatusCode(500, new { errorMessage = e.Message });
            }
    }


    [HttpPut]
    public async Task<ActionResult> UpdateHero([FromForm]SuperHero request)
    {
       
        var hero = await _context.SuperHeros.FirstOrDefaultAsync(i=>i.Id == request.Id);
       
        if(hero==null)
        {
            return BadRequest("Hero not found");
        }

        hero.Name = request.Name;
        hero.FirstName = request.FirstName;
        hero.LastName = request.LastName;
        hero.Place = request.Place;

       var herous = _context.SuperHeros.Update(hero);
        await _context.SaveChangesAsync();

        return Ok(herous);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteHero(int id)
    {
     
       var hero = await _context.SuperHeros.FirstOrDefaultAsync(i=>i.Id == id);

        if(hero==null)
        {
            return BadRequest("Hero not found");
        }

        _context.SuperHeros.Remove(hero);
      
        return Ok(hero);
    }


}
