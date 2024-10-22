using ForkyWebAPI.Data;
using ForkyWebAPI.Data.Repos.IRepos;
using ForkyWebAPI.Models;
using ForkyWebAPI.Models.BookingDTOs;
using ForkyWebAPI.Models.TableDTOs;
using ForkyWebAPI.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace ForkyWebAPI.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepo _bookingRepo;
        private readonly IRestaurantRepo _restaurantRepo;
        private readonly ForkyContext _context;

        public BookingService(IBookingRepo bookingRepo, IRestaurantRepo restaurantRepo, ForkyContext context)
        {
            _bookingRepo = bookingRepo;
            _restaurantRepo = restaurantRepo;
            _context = context;
        }

        public async Task<ViewBookingDTO> AddBookingAsync(NewBookingDTO newBookingDTO)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var availabilityCheck = new AvailabilityCheckDTO
                    {
                        FK_RestaurantId = newBookingDTO.FK_RestaurantId,
                        StartTime = newBookingDTO.BookingStart,
                        EndTime = newBookingDTO.BookingEnd,
                        NumberOfGuests = newBookingDTO.NumberOfGuests
                    };

                    var availableTables = await CheckAvailabilityAsync(availabilityCheck);
                    var selectedTable = availableTables.First() ??
                        throw new Exception(
                            "No available tables found for the selected time and number of guests."
                        );

                    var booking = new Booking
                    {
                        NumberOfGuests = newBookingDTO.NumberOfGuests,
                        BookingStart = newBookingDTO.BookingStart,
                        BookingEnd = newBookingDTO.BookingEnd,
                        Message = newBookingDTO.Message,
                        FK_AccountId = newBookingDTO.FK_AccountId,
                        FK_RestaurantId = newBookingDTO.FK_RestaurantId,
                        FK_TableId = selectedTable.Id
                    };

                    await _bookingRepo.AddBookingAsync(booking);

                    await transaction.CommitAsync();

                    return new ViewBookingDTO
                    {
                        Id = booking.Id,
                        FK_AccountId = booking.FK_AccountId,
                        CustomerName = $"{booking.Account?.FirstName ?? ""} {booking.Account?.LastName ?? ""}".Trim(),
                        FK_RestaurantId = booking.FK_RestaurantId,
                        RestaurantName = booking.Restaurant?.RestaurantName,
                        FK_TableId = booking.FK_TableId,
                        NumberOfGuests = booking.NumberOfGuests,
                        BookingStart = booking.BookingStart,
                        BookingEnd = booking.BookingEnd,
                        Message = booking.Message ?? "",
                        OperationResult = "Created"
                    };
                }

                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new Exception("Failed to add booking: " + ex.Message, ex);
                }
            }
        }

        public async Task DeleteBookingAsync(int bookingId)
        {
            var booking = await _bookingRepo.GetBookingByIdAsync(bookingId)
                ?? throw new KeyNotFoundException($"Booking with ID {bookingId} not found.");

            await _bookingRepo.DeleteBookingAsync(booking);
        }

        public async Task UpdateBookingAsync(int bookingId, UpdateBookingDTO updateBookingDTO)
        {
            if (updateBookingDTO == null)
            {
                throw new ArgumentNullException(nameof(updateBookingDTO), "Updated booking information cannot be null.");
            }

            var booking = await _bookingRepo.GetBookingByIdAsync(bookingId)
                ?? throw new KeyNotFoundException($"Booking with ID {bookingId} not found.");

            var availabilityCheckDTO = new AvailabilityCheckDTO
            {
                FK_RestaurantId = updateBookingDTO.FK_RestaurantId,
                StartTime = updateBookingDTO.BookingStart,
                EndTime = updateBookingDTO.BookingEnd,
                NumberOfGuests = updateBookingDTO.NumberOfGuests,
                FK_BookingId = bookingId
            };

            var availableTables = await CheckAvailabilityAsync(availabilityCheckDTO);

            if (!availableTables.Any())
            {
                throw new Exception("No available tables found for the selected time and number of guests.");
            }

            var selectedTable = availableTables.First();

            booking.Id = bookingId;
            booking.NumberOfGuests = updateBookingDTO.NumberOfGuests;
            booking.BookingStart = updateBookingDTO.BookingStart;
            booking.BookingEnd = updateBookingDTO.BookingEnd;
            booking.Message = updateBookingDTO.Message;
            booking.FK_AccountId = updateBookingDTO.FK_AccountId;
            booking.FK_RestaurantId = updateBookingDTO.FK_RestaurantId;
            booking.FK_TableId = selectedTable.Id;  

            await _bookingRepo.UpdateBookingAsync(booking);
        }

        public async Task<ViewBookingDTO> GetBookingByIdAsync(int bookingId)
        {
            var booking = await _bookingRepo.GetBookingByIdAsync(bookingId) ??
                throw new KeyNotFoundException($"Booking with ID {bookingId} not found.");

            return new ViewBookingDTO
            {
                Id = booking.Id,
                FK_AccountId = booking.FK_AccountId,
                CustomerName = $"{booking.Account?.FirstName ?? ""} {booking.Account?.LastName ?? ""}".Trim(),
                FK_RestaurantId = booking.FK_RestaurantId,
                RestaurantName = booking.Restaurant?.RestaurantName,
                FK_TableId = booking.FK_TableId,
                NumberOfGuests = booking.NumberOfGuests,
                BookingStart = booking.BookingStart,
                BookingEnd = booking.BookingEnd,
                Message = booking.Message ?? "",
                OperationResult = "Unchanged"
            };
        }

        public async Task<IEnumerable<ViewBookingDTO>> GetAllBookingsAsync()
        {
            var bookings = await _bookingRepo.GetAllBookingsAsync() ??
                throw new KeyNotFoundException($"No bookings found.");

            if (!bookings.Any())
            {
                throw new KeyNotFoundException("No bookings found.");
            }

            return bookings.Select(b =>
            {
                if (b == null) throw new InvalidOperationException("Unexpected null booking");
                return new ViewBookingDTO
                {
                    Id = b.Id,
                    FK_AccountId = b.FK_AccountId,
                    CustomerName = $"{b.Account?.FirstName ?? ""} {b.Account?.LastName ?? ""}".Trim(),
                    FK_RestaurantId = b.FK_RestaurantId,
                    RestaurantName = b.Restaurant?.RestaurantName ?? "",
                    FK_TableId = b.FK_TableId,
                    NumberOfGuests = b.NumberOfGuests,
                    BookingStart = b.BookingStart,
                    BookingEnd = b.BookingEnd,
                    Message = b.Message ?? "",
                    OperationResult = "Unchanged"
                };
            });
        }

        public async Task<IEnumerable<ViewBookingDTO>> GetBookingsByAccountIdAsync(int accountId)
        {
            var bookings = await _bookingRepo.GetBookingsByAccountIdAsync(accountId) ??
                throw new KeyNotFoundException($"No bookings found for account with ID {accountId}.");

            return bookings.Select(b =>
            {
                if (b == null) throw new InvalidOperationException("Unexpected null booking");
                return new ViewBookingDTO
                {
                    Id = b.Id,
                    FK_AccountId = b.FK_AccountId,
                    CustomerName = $"{b.Account?.FirstName ?? ""} {b.Account?.LastName ?? ""}".Trim(),
                    FK_RestaurantId = b.FK_RestaurantId,
                    RestaurantName = b.Restaurant?.RestaurantName ?? "",
                    FK_TableId = b.FK_TableId,
                    NumberOfGuests = b.NumberOfGuests,
                    BookingStart = b.BookingStart,
                    BookingEnd = b.BookingEnd,
                    Message = b.Message ?? "",
                    OperationResult = "Unchanged"
                };
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
                availabilityCheckDTO.NumberOfGuests,
                availabilityCheckDTO.FK_BookingId) ?? 
                throw new KeyNotFoundException("No available tables found for the selected time and number of guests.");

            var availableTableDTOs = availableTables.Select(t => new TableDTO
            {
                Id = t.Id,
                TableNumber = t.TableNumber,
                AmountOfSeats = t.AmountOfSeats,
                FK_RestaurantId = t.FK_RestaurantId
            });

            return availableTableDTOs;
        }

    }
}
