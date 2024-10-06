using ForkyWebAPI.Models;
using ForkyWebAPI.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
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
        }

        public async Task DeleteBookingAsync(int bookingId)
        {
            var booking = await _context.Bookings.FindAsync(bookingId);
            if (booking != null)
            {
                _context.Bookings.Remove(booking);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateBookingAsync(Booking updateBooking)
        {
            var booking = await _context.Bookings.FindAsync(updateBooking.Id);
            if (booking != null)
            {
                booking.NumberOfGuests = updateBooking.NumberOfGuests;
                booking.BookingStart = updateBooking.BookingStart;
                booking.BookingEnd = updateBooking.BookingEnd;
                booking.Message = updateBooking.Message;
                booking.FK_AccountId = updateBooking.FK_AccountId;
                booking.FK_TableId = updateBooking.FK_TableId;
                booking.FK_RestaurantId = updateBooking.FK_RestaurantId;

                await _context.SaveChangesAsync();
            }
        }

        public async Task<Booking?> GetBookingByIdAsync(int bookingId)
        {
            return await _context.Bookings
                .Include(b => b.Account) 
                .Include(b => b.Table)   
                .Include(b => b.Restaurant) 
                .FirstOrDefaultAsync(b => b.Id == bookingId);
        }

        public async Task<IEnumerable<Booking?>> ViewAllBookingsAsync()
        {
            return await _context.Bookings
                .Include(b => b.Account) 
                .Include(b => b.Table)   
                .Include(b => b.Restaurant) 
                .ToListAsync();
        }

        public async Task<IEnumerable<Booking?>> GetBookingsByAccountAsync(int accountId)
        {
            return await _context.Bookings
                .Where(b => b.FK_AccountId == accountId)
                .Include(b => b.Table)   
                .Include(b => b.Restaurant) 
                .ToListAsync();
        }
    }
}

