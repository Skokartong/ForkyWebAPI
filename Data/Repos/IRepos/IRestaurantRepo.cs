using ForkyWebAPI.Models;

namespace ForkyWebAPI.Data.Repos.IRepos
{
    public interface IRestaurantRepo
    {
        Task AddRestaurantAsync(Restaurant restaurant);
        Task DeleteRestaurantAsync(int restaurantId);
        Task UpdateRestaurantAsync(int restaurantId, Restaurant updatedRestaurant);
        Task<Models.Restaurant> SearchRestaurantAsync(int restaurantId);
        Task<IEnumerable<Restaurant>> GetAllRestaurantsAsync();
        Task<IEnumerable<Table>> GetTablesByRestaurantIdAsync(int restaurantId);
        Task AddTableAsync(Table table);
        Task DeleteTableAsync(int tableId);
        Task UpdateTableAsync(int tableId, Table updatedTable);
        Task<IEnumerable<Table>> GetAvailableTablesAsync(int restaurantId, DateTime startTime, DateTime endTime, int numberOfGuests);
        Task AddDishOrDrinkAsync(Menu menu);
        Task DeleteDishOrDrinkAsync(int menuId);
        Task UpdateDishOrDrinkAsync(int menuId, Menu updateMenu);
        Task<IEnumerable<Menu?>> SeeMenuAsync(int FK_RestaurantId);
        Task<bool> GetAvailableMenuItemAsync(int menuId);
    }
}
