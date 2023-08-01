using Microsoft.AspNetCore.Mvc;
using Utility;
using Utility.Models;

namespace TrainingAPI.Controllers
{
    [ApiController]
    [Route("api/person")]
    public class PersonController : Controller
    {
        private readonly DataContext _dbContext;

        public PersonController([FromServices] DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        public async Task<ActionResult<Person>> AddNewPerson(Person person)
        {
            try
            {
                _dbContext.Person.Add(person);

                await _dbContext.SaveChangesAsync();

                return Ok(person);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        
        [HttpPut]
        public async Task<ActionResult<Person>> DeletePerson(int personId)
        {
            try
            {
                Person person = (Person)_dbContext.Person.Where(m => m.Id.Equals(personId));
                person.DeletionDate = DateTime.Now;

                _dbContext.Person.Update(person);
                await _dbContext.SaveChangesAsync();

                return Ok(person);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
