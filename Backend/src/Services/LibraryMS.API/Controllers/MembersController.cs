using AutoMapper;
using LibraryMS.API.Data.Entities;
using LibraryMS.API.Dtos;
using LibraryMS.API.Helpers;
using LibraryMS.API.Services.Interfaces;
using LibraryMS.API.UOW.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembersController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IMemberService memberService;

        public MembersController(IUnitOfWork unitOfWork, IMapper mapper, IMemberService memberService)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.memberService = memberService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var members = await memberService.Get();

            return Ok(members);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var member=await memberService.GetById(id)  ;
            if (member == null)
                return NotFound();

            return Ok(member);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateMemberDto dto)
        {
           
            var library = await unitOfWork.Library.GetByIDAsync(dto.LibraryId);
            if (library == null)
                return BadRequest("Invalid Library ID.");
            var member=mapper.Map<Member>(dto);
            member.createdBy = 0;
            member.createdOn = DateTime.UtcNow;
            unitOfWork.Member.Insert(member);
            await unitOfWork.SaveAsync();

            return Ok(200);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateMemberDto dto)
        {
            if (id != dto.MemberId)
                return BadRequest("Member ID mismatch.");

            var existing = await unitOfWork.Member.GetByIDAsync(id);
            if (existing == null)
                return NotFound();

            // a member cannot change linked library if loans exist
            var hasLoans = await unitOfWork.BookLoan.GetFirstOrDefaultAsync(l => l.MemberId == id);
            if (hasLoans!=null && existing.LibraryId != dto.LibraryId)
                return BadRequest("Cannot change library for a member with loan history.");

            var entity = mapper.Map(dto, existing);
            entity.updatedBy = 0;
            entity.UpdatedOn = DateTime.UtcNow;
            unitOfWork.Member.Update(entity);
            await unitOfWork.SaveAsync();

            return NoContent();
        }
    }
}
