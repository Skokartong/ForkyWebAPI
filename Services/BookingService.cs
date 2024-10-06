using ForkyWebAPI.Data.Repos;
using ForkyWebAPI.Data.Repos.IRepos;
using ForkyWebAPI.Models;
using ForkyWebAPI.Models.BookingDTOs;
using ForkyWebAPI.Models.RestaurantDTOs;
using ForkyWebAPI.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForkyWebAPI.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepo _bookingRepo;
        private readonly IRestaurantRepo _restaurantRepo;

        public BookingService(IBookingRepo bookingRepo, IRestaurantRepo restaurantRepo)
        {
            _bookingRepo = bookingRepo;
            _restaurantRepo = restaurantRepo;
        }

        public async Task AddBookingAsync(NewBookingDTO newBookingDTO, int accountId)
        {
            if (newBookingDTO == null)
            {
                throw new ArgumentNullException(nameof(newBookingDTO), "Booking information cannot be null.");
            }

            var booking = new Booking
            {
                NumberOfGuests = newBookingDTO.NumberOfGuests,
                BookingStart = newBookingDTO.BookingStart,
                BookingEnd = newBookingDTO.BookingEnd,
                Message = newBookingDTO.Message,
                FK_AccountId = accountId,
                FK_RestaurantId = newBookingDTO.FK_RestaurantId
            };

            await _bookingRepo.AddBookingAsync(booking);
        }

        public async Task DeleteBookingAsync(int bookingId)
        {
            await _bookingRepo.DeleteBookingAsync(bookingId);
        }

        public async Task UpdateBookingAsync(int bookingId, NewBookingDTO updateBookingDTO)
        {
            if (updateBookingDTO == null)
            {
                throw new ArgumentNullException(nameof(updateBookingDTO), "Updated booking information cannot be null.");
            }

            var booking = new Booking
            {
                Id = bookingId,
                NumberOfGuests = updateBookingDTO.NumberOfGuests,
                BookingStart = updateBookingDTO.BookingStart,
                BookingEnd = updateBookingDTO.BookingEnd,
                Message = updateBookingDTO.Message,
                FK_RestaurantId = updateBookingDTO.FK_RestaurantId
            };

            await _bookingRepo.UpdateBookingAsync(booking);
        }

        public async Task<ViewBookingDTO> GetBookingByIdAsync(int bookingId)
        {
            var booking = await _bookingRepo.GetBookingByIdAsync(bookingId);

            if (booking == null)
            {
                return null;
            }

            return new ViewBookingDTO
            {
                CustomerName = booking.Account?.FirstName + booking.Account?.LastName,
                RestaurantName = booking.Restaurant.RestaurantName,
                NumberOfGuests = booking.NumberOfGuests,
                BookingStart = booking.BookingStart,
                BookingEnd = booking.BookingEnd,
                Message = booking.Message
            };
        }

        public async Task<IEnumerable<ViewBookingDTO>> ViewAllBookingsAsync()
        {
            var bookings = await _bookingRepo.ViewAllBookingsAsync();

            return bookings.Select(b => new ViewBookingDTO
            {
                CustomerName = b.Account?.FirstName + " " + b.Account?.LastName,
                RestaurantName = b.Restaurant?.RestaurantName ?? "",
                NumberOfGuests = b.NumberOfGuests,
                BookingStart = b.BookingStart,
                BookingEnd = b.BookingEnd,
                Message = b.Message
            });
        }

        public async Task<IEnumerable<ViewBookingDTO>> GetBookingsByAccountAsync(int accountId)
        {
            var bookings = await _bookingRepo.GetBookingsByAccountAsync(accountId);

            return bookings.Select(b => new ViewBookingDTO
            {
                CustomerName = b.Account?.FirstName + " " + b.Account?.LastName,
                RestaurantName = b.Restaurant?.RestaurantName ?? "",
                NumberOfGuests = b.NumberOfGuests,
                BookingStart = b.BookingStart,
                BookingEnd = b.BookingEnd,
                Message = b.Message
            });
        }

        public async Task<IEnumerable<TableDTO>> CheckAvailabilityAsync(AvailabilityCheckDTO availabilityCheckDTO)
        {
            if (availabilityCheckDTO == null)
            {
                throw new ArgumentNullException(nameof(availabilityCheckDTO), "Availability check information cannot be null.");
            }

            var availableTables = await _restaurantRepo.GetAvailableTablesAsync(
                availabilityCheckDTO.FK_RestaurantId,
                availabilityCheckDTO.StartTime,
                availabilityCheckDTO.EndTime,
                availabilityCheckDTO.NumberOfGuests);

            var availableTableDTOs = availableTables.Select(t => new TableDTO
            {
                TableNumber = t.TableNumber,
                AmountOfSeats = t.AmountOfSeats,
                FK_RestaurantId = t.FK_RestaurantId
            });

            return availableTableDTOs;
        }
    }
}
