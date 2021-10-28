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

    public class NotesController : ControllerBase
    {
        private static List<Notes> _notes = new List<Notes>
        { new Notes {
            Id = new Guid("00000000-0000-0000-0000-000000000001"),
            CategoryId = "1",
            OwnerId = new Guid("00000000-0000-0000-0000-000000000001"),
            Title = "First Note",
            Description = "First Note Description" },
        new Notes {
            Id = new Guid("00000000-0000-0000-0000-000000000002"),
            CategoryId = "1",
            OwnerId = new Guid("00000000-0000-0000-0000-000000000001"),
            Title = "Second Note",
            Description = "Second Note Description" },
        new Notes {
            Id = new Guid("00000000-0000-0000-0000-000000000003"),
            CategoryId = "1",
            OwnerId = new Guid("00000000-0000-0000-0000-000000000001"),
            Title = "Third Note",
            Description = "Third Note Description" },
        new Notes {
            Id = new Guid("00000000-0000-0000-0000-000000000004"),
            CategoryId = "1",
            OwnerId = new Guid("00000000-0000-0000-0000-000000000001"),
            Title = "Fourth Note",
            Description = "Fourth Note Description" },
        new Notes {
            Id = new Guid("00000000-0000-0000-0000-000000000005"),
            CategoryId = "1",
            OwnerId = new Guid("00000000-0000-0000-0000-000000000001"),
            Title = "Fifth Note",
            Description = "Fifth Note Description" }
        };

        public NotesController()
        {

        }

        [HttpGet]
        public IActionResult GetNotes()
        {
            return Ok(_notes);
        }
       /// <summary>
        /// 
        /// </summary>
        /// <response code="400">Bad Request</response>
        /// <response code="404">Not Found</response>
        /// <returns></returns>
        [HttpPut("Owner/{id}/{ownerid}")]
        public IActionResult UpdateNotewithIdAndOwner(Guid id, Guid ownerid, [FromBody] Notes noteToUpdate)
        {
            if (noteToUpdate == null)
            {
                return BadRequest("Note cannot be null");
            }
            var indexid = _notes.FindIndex(note => note.Id == id);
            var indexowner = _notes.FindIndex(note => note.OwnerId == ownerid);
            if (indexid == -1)
            {
                return NotFound();
            }
            if (indexowner == -1)
            {
                return NotFound();
            }
            noteToUpdate.Id = _notes[indexid].Id;
            _notes[indexid] = noteToUpdate;
            noteToUpdate.OwnerId = _notes[indexowner].Id;
            _notes[indexowner] = noteToUpdate;
            return NoContent();
            //return Ok(_notes);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <response code="400">Bad Request</response>
        /// <response code="404">Not Found</response>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult UpdateNote(Guid id, [FromBody] Notes noteToUpdate)
        {
            if (noteToUpdate == null)
            {
                return BadRequest("Note cannot be null");
            }
            var index = _notes.FindIndex(note => note.Id == id);
            if (index == -1)
            {
                return NotFound();
            }
            noteToUpdate.Id = _notes[index].Id;
            _notes[index] = noteToUpdate;
            return NoContent();
            //return Ok(_notes);
        }
         /// <summary>
        /// 
        /// </summary>
        /// <response code="400">Bad Request</response>
        /// <response code="404">Not Found</response>
        /// <returns></returns>
       [HttpDelete("Owner/{id}/{ownerid}")]
        public IActionResult DeleteNoteWithIdAndOrder(Guid id,Guid ownerid)
        {
            var indexid = _notes.FindIndex(note => note.Id == id);
            if (indexid == -1)
            {
                return NotFound();
            }
            var indexowner = _notes.FindIndex(note => note.OwnerId == ownerid);
            if (indexowner == -1)
            {
                return NotFound();
            }
            _notes.RemoveAt(indexid);
            _notes.RemoveAt(indexowner);
            return NoContent();
        }
         
        /// <summary>
        /// 
        /// </summary>
        /// <response code="400">Bad Request</response>
        /// <response code="404">Not Found</response>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult DeleteNote(Guid id)
        {
            var index = _notes.FindIndex(note => note.Id == id);
            if (index == -1)
            {
                return NotFound();
            }
            _notes.RemoveAt(index);
            return NoContent();
        }
     
       [HttpDelete("OwnerId/{id}")]
        public IActionResult DeleteAllNotes(Guid id)
        {
            foreach (Notes note in _notes) 
            {
                _notes.Remove(_notes.Find(note => string.Equals(id, note.OwnerId)));
            }
            return NoContent();
        } 
        /// <summary>
        /// 
        /// </summary>
        /// <response code="400">Bad Request</response>
        /// <response code="404">Not Found</response>
        /// <returns></returns>
        [HttpPatch("{id}/title")]
        public IActionResult UpdateTitleNote (Guid id, [FromBody] string title)
        {
            if(string.IsNullOrEmpty(title))
            {
                return BadRequest("The string cannot ne null");
            }
            var index = _notes.FindIndex(note => note.Id == id);
            if (index == -1)
            {
                return NotFound();
            }
            _notes[index].Title = title;
            return Ok(_notes[index]);
        }

        [HttpPost]
        public IActionResult CreateNotes([FromBody] Notes note)
        {
            if (note == null)
            {
                return BadRequest("Note cannot be null");
            }
            _notes.Add(note);
            return Ok();
            //return StatusCode(StatusCodes.Status500InternalServerError, "Error in processing the note");
        }
        [HttpGet("OwnerId/{id}")]
        public IActionResult GetByOwnerId(Guid id)
        {
            List<Notes> note=_notes.FindAll(note => note.OwnerId==id);
            return Ok(note); 
        }

        [HttpGet(" Id/{id}")]
        public IActionResult GetByNoteId(Guid id)
        {
            List<Notes> note = _notes.FindAll(note => note.Id == id);
            return Ok(note);
        }


        /*///List<string> myList = new List<string> { "test1", "test2", "test3" };

        /// <summary>
        /// Returns the notes collection
        /// </summary>
        /// <return></return>
        ///[HttpGet("{id}")]
        ///public IActionResult GetWithParams(string id, string id2, string id3)
        /// {
        ///   return Ok($"id:{ id}, id2: { id2},id3: { id3}");

        ///}
        /// <summary>
        /// Returns a list of notes
        /// </summary>
        /// <returns></returns>
        ///[HttpGet("")]
       /// public IActionResult Get()
        ///{
          ///  return Ok("FROM GET");

        ///}

    /// <summary>
    /// Returns the notes collection
    /// </summary>
    /// <param name="bodyContent"></param>
    /// <returns></returns>
        [HttpPost("")]
        public IActionResult Post([FromBody] string bodyContent)
        {
            myList.Add(bodyContent);
        }
        /// <summary>
        /// REturns the notes collection
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
         [HttpDelete("{id}")]
       public IActionResult Delete( string id)
        {
            myList.Remove(id);
        }*/


    }
}
