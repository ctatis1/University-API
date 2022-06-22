using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniversityApiBackend.DataAccess;
using UniversityApiBackend.Models.DataModels;

namespace UniversityApiBackend.Controllers
{
    [Route("api/[controller]")] // Controller for Requests to https://localhost:7269/api/users
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UniversityDBContext _context;

        //crea un contexto de la BD para acceder a todos los sets (tablas)
        public UsersController(UniversityDBContext context)
        {
            _context = context;
        }

        // GET: api/Users https://localhost:7269/api/users
        [HttpGet]
        //Una Task asícrona que devuelve un resultado único de tipo lista de User
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            //accede al contexto, a la tabla de Users y deveulve una lista asíncrona
            return await _context.Users.ToListAsync();
        }

        // GET: api/Users/5 https://localhost:7269/api/users/1
        [HttpGet("{id}")]
        //una task asíncrona que devuelve un result único de tipo User dado un id
        public async Task<ActionResult<User>> GetUser(int id)
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            //se busca dentro del contexto, en el set de User el ID de manera asíncrona
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Users/5 https://localhost:7269/api/users/1
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")] 
        //en este caso se usa un IActionResult porque no devuelve un objeto 
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }
            /*
                accede a la tabla de Users mediante el contexto con el user dado para rastrear su estado.
                EntityState está diciendo que ese estado ha sido modificado 
             */
            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            //en caso de que haya error de tipo concurrencia 
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            //se devuelve un 204: todo ha ido bien 
            return NoContent();
        }

        // POST: api/Users https://localhost:7269/api/users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        //una task asíncrona que devuelve un resultado de tipo User donde, por parámetros, recibe un User 
        public async Task<ActionResult<User>> PostUser(User user)
        {
          if (_context.Users == null)
          {
              return Problem("Entity set 'UniversityDBContext.Users'  is null.");
          }
            //accediendo a la tabla User mediante el contexto, se añade el nuevo user
            _context.Users.Add(user);
            //se espera a que el contexto guarde los cambios para continuar con la ejecución
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        // DELETE: api/Users/5 https://localhost:7269/api/users/5
        [HttpDelete("{id}")]
        //una task asíncrona donde no devuelve un resultado, pero si le llega un id como parámetro
        public async Task<IActionResult> DeleteUser(int id)
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            /*
             se identifica al user a borrar accediendo a la tabla users mediante el contexto 
             y buscando entre todos por el id dado
             */
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            /*se accede a la tabla Users mediante el contexto y se ejecuta la acción de Remove del
             user previamente identificado*/
            _context.Users.Remove(user);
            //el contexto guarda los cambios de manera asíncrona
            await _context.SaveChangesAsync();

            return NoContent();
        }
        //función que verifica si el User existe por medio de un id dado
        private bool UserExists(int id)
        {
            /*se accede a la tabla Users mediante el contexto para buscar el user 
             el cual su ID coincida con el id dado y retornar el user*/
            return (_context.Users?.Any(user => user.Id == id)).GetValueOrDefault();
        }
    }
}
