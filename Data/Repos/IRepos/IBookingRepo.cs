using ForkyWebAPI.Models;

namespace ForkyWebAPI.Data.Repos.IRepos
{
    public interface IBookingRepo
    {
        // ADD, DELETE, UPDATE BOOKINGS
        Task AddBookingAsync(Booking booking);
        Task DeleteBookingAsync(Booking booking);
        Task UpdateBookingAsync(Booking booking);

        // GET BOOKINGS
        Task<Booking?> GetBookingByIdAsync(int bookingId);
        Task<IEnumerable<Booking?>> GetAllBookingsAsync();
        Task<IEnumerable<Booking?>> GetBookingsByAccountIdAsync(int accountId);
    }
}
