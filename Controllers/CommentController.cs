using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestApi.Models;

namespace TestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly AplicationDbContext _context;
        public CommentController(AplicationDbContext context) {
            _context = context;
        }

        //

        [HttpGet] // Se pudo haber escrito asi [HttpGet("Index")] 
        public async Task<IActionResult> Index(){
            var listComments = await _context.Comments.ToListAsync();

            if (listComments == null || listComments.Count == 0)
            {
                return NoContent();
            }
            return Ok(listComments);
        }

        // Store

        [HttpPost("Store")]
        public async Task<HttpStatusCode> Store([FromBody] Comment comment)
        {
            if (comment == null)
            {
                return HttpStatusCode.BadRequest;
            }
            _context.Add(comment);
            await _context.SaveChangesAsync();
            return HttpStatusCode.Created;
        }

        // Show
        [HttpGet("Show")]
        public async Task<IActionResult> Show(int id)
        {
            var comment = await _context.Comments.FindAsync(id);
            if (comment == null)
            {
                return NotFound();
            }
            return Ok(comment);
        }

        // Distroy

        [HttpPost("Destroy")]
        public async Task<IActionResult> Destroy(int id)
        {
            var comment = await _context.Comments.FindAsync(id);
            if (comment == null)
            {
                return NotFound();
            }
            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("Update")]
        public async Task<IActionResult> Update([FromBody] Comment comment)
        {
            if(comment == null ){
                return BadRequest();//400
            }
            var entity = await _context.Comments.FindAsync(comment.Id);
            if(entity == null){
                return BadRequest();//404
            }
            entity.Title = comment.Title;
            entity.Description = comment.Description;
            entity.Author = comment.Author;
            entity.CreatedAt = comment.CreatedAt;
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}