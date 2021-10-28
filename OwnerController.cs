using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NotesAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class OwnerController : ControllerBase
    {
        static List<Owner> _owners = new List<Owner> {
        new Owner {
            Id = new System.Guid(),
            Name = "First Owner",
        },
         new Owner {
            Id = new System.Guid(),
            Name = "Second Owner",
        },
         new Owner {
            Id = new System.Guid(),
            Name = "Third Owner",
        },
          new Owner {
            Id = new System.Guid(),
            Name = "Forth Owner",
        },
          new Owner {
            Id = new System.Guid(),
            Name = "Fifth Owner",
        }
        };
        public OwnerController()
        {

        }

        [HttpGet]
        public IActionResult GetOwners()
        {
            return Ok(_owners);
        }

        [HttpPost]
        public IActionResult CreateOwners([FromBody] Owner owner)
        {
            if (owner == null)
            {
                return BadRequest("Note cannot be null");
            }
            _owners.Add(owner);
            return Ok();
            //return StatusCode(StatusCodes.Status500InternalServerError, "Error in processing the owner");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <response code="400">Bad Request</response>
        /// <response code="404">Not Found</response>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult UpdateOwner(Guid id, [FromBody] Owner ownerToUpdate)
        {
            if (ownerToUpdate == null)
            {
                return BadRequest("Owner cannot be null");
            }
            var index = _owners.FindIndex(owner => owner.Id == id);
            if (index == -1)
            {
                return NotFound();
            }
            ownerToUpdate.Id = _owners[index].Id;
            _owners[index] = ownerToUpdate;
            return NoContent();
            //return Ok(_notes);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <response code="400">Bad Request</response>
        /// <response code="404">Not Found</response>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult DeleteOwner(Guid id)
        {
            var index = _owners.FindIndex(owner => owner.Id == id);
            if (index == -1)
            {
                return NotFound();
            }
            _owners.RemoveAt(index);
            return NoContent();
        }
    }
}
