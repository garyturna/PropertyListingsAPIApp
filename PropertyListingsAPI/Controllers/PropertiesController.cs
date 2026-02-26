using Microsoft.AspNetCore.Mvc;
using PropertyListingsAPI.Models;

namespace PropertyListingsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PropertiesController : ControllerBase
    {
        // Temporary in-memory list - we'll replace with database later
        private static List<Property> _properties = new List<Property>
        {
            new Property { Id = 1, Title = "Modern Ponsonby Villa", Address = "12 Brown St, Ponsonby", Price = 1200000, Bedrooms = 3, Bathrooms = 2, ListedDate = DateTime.Now },
            new Property { Id = 2, Title = "Grey Lynn Bungalow", Address = "45 Great North Rd, Grey Lynn", Price = 950000, Bedrooms = 2, Bathrooms = 1, ListedDate = DateTime.Now }
        };


        // GET all properties
        [HttpGet]
        public ActionResult<List<Property>> GetAll()
        {
            return Ok(_properties);
        }

        // GET single property by id
        [HttpGet("{id}")]
        public ActionResult<Property> GetById(int id)
        {
            var property = _properties.FirstOrDefault(p => p.Id == id);
            if (property == null) return NotFound();
            return Ok(property);
        }

        // POST create new property
        [HttpPost]
        public ActionResult<Property> Create(Property property)
        {
            property.Id = _properties.Max(p => p.Id) + 1;
            _properties.Add(property);
            return CreatedAtAction(nameof(GetById), new { id = property.Id }, property);
        }

        // PUT update existing property
        [HttpPut("{id}")]
        public ActionResult Update(int id, Property updated)
        {
            var property = _properties.FirstOrDefault(p => p.Id == id);
            if (property == null) return NotFound();

            property.Title = updated.Title;
            property.Address = updated.Address;
            property.Price = updated.Price;
            property.Bedrooms = updated.Bedrooms;
            property.Bathrooms = updated.Bathrooms;

            return NoContent();
        }

        // DELETE property
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var property = _properties.FirstOrDefault(p => p.Id == id);
            if (property == null) return NotFound();

            _properties.Remove(property);
            return NoContent();
        }
    }
}
