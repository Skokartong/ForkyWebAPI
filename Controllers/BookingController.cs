using ForkyWebAPI.Models;
using ForkyWebAPI.Models.BookingDTOs;
using ForkyWebAPI.Models.TableDTOs;
using ForkyWebAPI.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ForkyWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpPost]
        [Route("/newbooking")]
        public async Task<IActionResult> BookTable([FromBody] NewBookingDTO newBookingDTO)
        {
            try
            {
                var result = await _bookingService.AddBookingAsync(newBookingDTO);

                return Created("", new { message = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete]
        [Route("/deletebooking/{bookingId}")]
        public async Task<IActionResult> DeleteBooking(int bookingId)
        {
            await _bookingService.DeleteBookingAsync(bookingId);
            return NoContent();
        }

        [HttpPut]
        [Route("/updatebooking/{bookingId}")]
        public async Task<IActionResult> UpdateBooking(int bookingId, [FromBody] UpdateBookingDTO updateBookingDTO)
        {
            try
            {
                await _bookingService.UpdateBookingAsync(bookingId, updateBookingDTO);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet]
        [Route("/viewbooking/{bookingId}")]
        public async Task<ActionResult<ViewBookingDTO?>> GetBookingById(int bookingId)
        {
            var booking = await _bookingService.GetBookingByIdAsync(bookingId);
            if (booking == null)
            {
                return NotFound("Booking not found");
            }
            return Ok(booking);
        }

        [HttpGet]
        [Route("/viewbookings")]
        public async Task<ActionResult<IEnumerable<ViewBookingDTO>>> ViewAllBookings()
        {
            var bookings = await _bookingService.GetAllBookingsAsync();
            return Ok(bookings);
        }

        [HttpGet]
        [Route("/viewbookings/{accountId}")]
        public async Task<ActionResult<IEnumerable<ViewBookingDTO>>> GetBookingsByAccount(int accountId)
        {
            var bookings = await _bookingService.GetBookingsByAccountIdAsync(accountId);
            return Ok(bookings);
        }

        [HttpPost]
        [Route("/checkavailability")]
        public async Task<ActionResult<IEnumerable<TableDTO>>> CheckAvailability([FromBody] AvailabilityCheckDTO availabilityCheckDTO)
        {
            var availableTables = await _bookingService.CheckAvailabilityAsync(availabilityCheckDTO);
            return Ok(availableTables);
        }
    }
}

