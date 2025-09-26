using AutoMapper;
using LibraryMS.API.Data.Entities;
using LibraryMS.API.Dtos;
using LibraryMS.API.Services.Interfaces;
using LibraryMS.API.UOW.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibrariesController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly ILibraryService libraryService;

        public LibrariesController(IUnitOfWork unitOfWork, IMapper mapper, ILibraryService libraryService)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.libraryService = libraryService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var libraries = await unitOfWork.Library.GetAsync();
            return Ok(libraries);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var library=await unitOfWork.Library.GetFirstOrDefaultAsync(x=>x.LibraryId==id,null, "Books,Members,Loans");
            if (library == null)
                return NotFound();

            return Ok(library);
        }

        [HttpGet("{id}/stats")]
        public async Task<ActionResult<LibraryStatsDto>> GetStats(int id)
        {
            var library = await unitOfWork.Library.GetByIDAsync(id);
            if (library == null)
                return NotFound();
            var stats = await libraryService.GetStats(id);
            return Ok(stats);
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateLibraryDto dto)
        {

            var entity = mapper.Map<Library>(dto);
            entity.createdBy = 0;
            entity.createdOn = DateTime.UtcNow;
            unitOfWork.Library.Insert(entity);
            await unitOfWork.SaveAsync();

            return CreatedAtAction(nameof(Get), new { id = entity.LibraryId }, entity);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateLibraryDto dto)
        {
            if (id != dto.LibraryId)
                return BadRequest("Library ID mismatch.");

            var existing = await unitOfWork.Library.GetByIDAsync(id);
            if (existing == null)
                return NotFound();

            var entity = mapper.Map(dto, existing);
            entity.updatedBy = 0;
            entity.UpdatedOn = DateTime.UtcNow;
            unitOfWork.Library.Update(entity);
            await unitOfWork.SaveAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var library = await unitOfWork.Library.GetByIDAsync(id);
            if (library == null)
                return NotFound();

            var hasBooks = await unitOfWork.Book.GetFirstOrDefaultAsync(b => b.LibraryId == id);
            var hasMembers = await unitOfWork.Member.GetFirstOrDefaultAsync(m => m.LibraryId == id);
            var hasLoans = await unitOfWork.BookLoan.GetFirstOrDefaultAsync(l => l.LibraryId == id);

            if (hasBooks!=null || hasMembers!=null || hasLoans != null)
                return BadRequest("Cannot delete library with associated books, members, or loans.");

            unitOfWork.Library.DeleteWhere(x=>x.LibraryId==id);
            await unitOfWork.SaveAsync();

            return NoContent();
        }

    }
}
