using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShelterApi.Models;
using ShelterAPI.Data;
using ShelterAPI.Models;

namespace ShelterAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimalController : ControllerBase
    {

        private readonly ShelterContext _context;

        public AnimalController(ShelterContext context)
        {
            _context = context;
        }


        // GET: api/Animal
        [HttpGet]
        public IEnumerable<Animal> Get([FromQuery] AnimalFilter filter) //returns list of all animals (filtering and sorting is avaliable)
        {
            IEnumerable<Animal> animals = _context.Animal;
            if (filter!=null && filter.ParametersExist()) animals = filter.FilterAnimals(animals);
            if (filter != null && filter.OrderParameterExist()) animals = filter.SortAnimals(animals);
            return animals;
        }

        // GET: api/Animal/name
        [Route("[action]/{name}")]
        [HttpGet]
        public IEnumerable<Animal> GetByName(string name, [FromQuery] AnimalFilter filter) //returns animal with specific name (filtering and sorting is avaliable)
        {
            IEnumerable<Animal> animals = Enumerable.Empty<Animal>(); ;
            if (!String.IsNullOrEmpty(name))
            {
                animals = _context.Animal.Where(a => a.Name.Equals(name));
            }
            if (filter != null && filter.ParametersExist()) animals = filter.FilterAnimals(animals);
            if (filter != null && filter.OrderParameterExist()) animals = filter.SortAnimals(animals);
            return animals;
        }

        [Route("[action]/{strain}")]
        [HttpGet]
        public IEnumerable<Animal> GetByStrain(string strain, [FromQuery] AnimalFilter filter) //returns animal with specific strain (filtering and sorting is avaliable)
        {
            IEnumerable<Animal> animals = Enumerable.Empty<Animal>(); ;
            if (!String.IsNullOrEmpty(strain))
            {
                animals = _context.Animal.Where(a => a.Strain.Equals(strain));
            }
            if (filter != null && filter.ParametersExist()) animals = filter.FilterAnimals(animals);
            if (filter != null && filter.OrderParameterExist()) animals = filter.SortAnimals(animals);
            return animals;
        }

        // GET: api/Animal/5
        [HttpGet("{id}", Name = "Get")]
        public Animal Get(int id) //returns animal with specific id
        {
            return _context.Animal.Find(id);
        }
        // POST: api/Animal
        [HttpPost]
        public Animal Post([FromBody] Animal animal) //creates new animal and returns created animal
        {
            _context.Animal.Add(animal);
            _context.SaveChanges();

            return animal;
        }

        // PUT: api/Animal/5
        [HttpPut("{id}")]
        public Animal Put(int id, [FromBody] Animal animal) //updates animal and returns updated animal
        {
            _context.Animal.Update(animal);
            _context.SaveChanges();
            return animal;
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public string Delete(int id) //deletes animal and returns message if animal is deleted or does not exist
        {
            string returnMessage = "";
            Animal animal = _context.Animal.Find(id);
            if (animal != null)
            {
                _context.Animal.Remove(animal);
                _context.SaveChanges();
                returnMessage = $"{animal.Name} delected";
            }
            else returnMessage = "Animal does not exist";
            return returnMessage;
        }
    }
}
