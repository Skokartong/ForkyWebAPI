using ForkyWebAPI.Models.BookingDTOs;
using ForkyWebAPI.Models.TableDTOs;

namespace ForkyWebAPI.Services.IServices
{
    public interface IBookingService
    {
        Task<ViewBookingDTO> AddBookingAsync(NewBookingDTO newBookingDTO);
        Task DeleteBookingAsync(int bookingId);
        Task UpdateBookingAsync(int bookingId, UpdateBookingDTO updateBookingDTO);
        Task<ViewBookingDTO> GetBookingByIdAsync(int bookingId);
        Task<IEnumerable<ViewBookingDTO>> GetAllBookingsAsync();
        Task<IEnumerable<ViewBookingDTO>> GetBookingsByAccountIdAsync(int accountId);
        Task<IEnumerable<TableDTO>> CheckAvailabilityAsync(AvailabilityCheckDTO availabilityCheckDTO);
    }
}
