using AutoMapper;
using LibraryMS.API.Dtos;
using LibraryMS.API.Services.Interfaces;
using LibraryMS.API.UOW.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoansController : ControllerBase
    {
        private readonly ILoanService _loanService;

        public LoansController(ILoanService loanService)
        {
            _loanService = loanService;
        }

        [HttpPost("borrow")]
        public async Task<IActionResult> Borrow([FromBody] BorrowRequestDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var check = await _loanService.BorrowBookAsync(request);
            if(check == "")
                return Ok(new { message = "Book borrowed successfully", loanDate = DateTime.UtcNow });
            else
                return BadRequest(check);



        }

        [HttpPost("return/{loanId}")]
        public async Task<IActionResult> Return(int loanId)
        {
            var success = await _loanService.ReturnBookAsync(loanId);

            if (!success)
                return NotFound("Loan not found or already returned.");

            return Ok(new { message = "Book returned successfully" });
        }

        [HttpGet("active")]
        public async Task<ActionResult<IEnumerable<LoanResponseDto>>> GetActiveLoans()
        {
            var loans = await _loanService.GetActiveLoansAsync();
            return Ok(loans);
        }
        [HttpGet("overdue")]
        public async Task<ActionResult<IEnumerable<LoanResponseDto>>> GetOverdueLoans()
        {
            var loans = await _loanService.GetOverdueLoansAsync();
            return Ok(loans);
        }
    }
}
