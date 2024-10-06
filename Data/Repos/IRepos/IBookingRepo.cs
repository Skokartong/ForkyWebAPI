using ForkyWebAPI.Models;

namespace ForkyWebAPI.Data.Repos.IRepos
{
    public interface IBookingRepo
    {
        Task AddBookingAsync(Booking booking);
        Task DeleteBookingAsync(int bookingId);
        Task UpdateBookingAsync(Booking updateBooking);
        Task<Booking?> GetBookingByIdAsync(int bookingId);
        Task<IEnumerable<Booking?>> ViewAllBookingsAsync();
        Task<IEnumerable<Booking?>> GetBookingsByAccountAsync(int accountId);
    }
}
