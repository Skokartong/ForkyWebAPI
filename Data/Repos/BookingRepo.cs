using ForkyWebAPI.Models;
using Microsoft.EntityFrameworkCore;
using ForkyWebAPI.Data.Repos.IRepos;

namespace ForkyWebAPI.Data.Repos
{
    public class BookingRepo : IBookingRepo
    {
        private readonly ForkyContext _context;

        public BookingRepo(ForkyContext context)
        {
            _context = context;
        }

        public async Task AddBookingAsync(Booking booking)
        {
            await _context.Bookings.AddAsync(booking);
            await _context.SaveChangesAsync();

            await _context.Entry(booking)
                .Reference(b => b.Account)
                .LoadAsync();

            await _context.Entry(booking)
                .Reference(b => b.Restaurant)
                .LoadAsync();
        }

        public async Task DeleteBookingAsync(Booking booking)
        {
            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();
        }


        public async Task UpdateBookingAsync(Booking booking)
        {
            _context.Bookings.Update(booking);
            await _context.SaveChangesAsync();
        }

        public async Task<Booking?> GetBookingByIdAsync(int bookingId)
        {
            return await _context.Bookings
                .Include(b => b.Account)
                .Include(b => b.Table)
                .Include(b => b.Restaurant)
                .FirstOrDefaultAsync(b => b.Id == bookingId);
        }

        public async Task<IEnumerable<Booking?>> GetAllBookingsAsync()
        {
            return await _context.Bookings
                .Include(b => b.Account)
                .Include(b => b.Table)
                .Include(b => b.Restaurant)
                .ToListAsync();
        }

        public async Task<IEnumerable<Booking?>> GetBookingsByAccountIdAsync(int accountId)
        {
            return await _context.Bookings
                .Where(b => b.FK_AccountId == accountId)
                .Include(b => b.Account)
                .Include(b => b.Table)
                .Include(b => b.Restaurant)
                .ToListAsync();
        }
    }
}

