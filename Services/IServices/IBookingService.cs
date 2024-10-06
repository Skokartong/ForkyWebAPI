using ForkyWebAPI.Models;
using ForkyWebAPI.Models.BookingDTOs;
using ForkyWebAPI.Models.RestaurantDTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ForkyWebAPI.Services.IServices
{
    public interface IBookingService
    {
        Task AddBookingAsync(NewBookingDTO newBookingDTO, int accountId);
        Task DeleteBookingAsync(int bookingId);
        Task UpdateBookingAsync(int bookingId, NewBookingDTO updateBookingDTO);
        Task<ViewBookingDTO> GetBookingByIdAsync(int bookingId);
        Task<IEnumerable<ViewBookingDTO>> ViewAllBookingsAsync();
        Task<IEnumerable<ViewBookingDTO>> GetBookingsByAccountAsync(int accountId);
        Task<IEnumerable<TableDTO>> CheckAvailabilityAsync(AvailabilityCheckDTO availabilityCheckDTO);
    }
}
