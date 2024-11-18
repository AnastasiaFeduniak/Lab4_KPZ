using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Lab4_1.Models;
using Lab4_1.ModelsUpdate;
using Lab4_1.Injection;
using Microsoft.EntityFrameworkCore;
using Lab4_1.Models;
using Lab4_1.Injection;
using AutoMapper;
using Lab4_1.ModelsView;

namespace Lab4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorService _authorService;
        private readonly IMapper _mapper;


        public AuthorsController(IAuthorService authorService, IMapper mapper)
    {
        _authorService = authorService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Author>>> GetAuthors()
        {
            var authors = await _authorService.Authors.ToListAsync();
            var authorsViewModel = _mapper.Map<IEnumerable<AuthorViewModel>>(authors);
            return Ok(authorsViewModel);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Author>> GetAuthor(int id)
        {
            var author = await _authorService.Authors.FindAsync(id);

            if (author == null)
            {
                return NotFound();
            }

            var authorViewModel = _mapper.Map<AuthorViewModel>(author);
            return Ok(authorViewModel);
        }

        [HttpPost]
        public async Task<ActionResult<Author>> CreateAuthor(AuthorUpdateViewModel model)
        {
            var author = _mapper.Map<Author>(model);
            _authorService.Authors.Add(author);
            await _authorService.SaveChangesAsync();

            var authorViewModel = _mapper.Map<AuthorViewModel>(author);
            return CreatedAtAction(nameof(GetAuthor), new { id = author.AuthorId }, authorViewModel);

        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateAuthorPartially(int id, AuthorUpdate modelUpdate)
        {
            await _authorService.UpdateAuthorAsync(id, modelUpdate);
            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            var author = await _authorService.Authors.FindAsync(id);
            if (author == null)
            {
                return NotFound();
            }

            _authorService.Authors.Remove(author);
            await _authorService.SaveChangesAsync();

            return NoContent();
        }
    }
}
