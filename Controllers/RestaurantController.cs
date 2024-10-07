using ForkyWebAPI.Models.MenuDTOs;
using ForkyWebAPI.Models.RestaurantDTOs;
using ForkyWebAPI.Models.TableDTOs;
using ForkyWebAPI.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ForkyWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantService _restaurantService;
        private readonly IBookingService _bookingService;

        public RestaurantController(IRestaurantService restaurantService, IBookingService bookingService)
        {
            _restaurantService = restaurantService;
            _bookingService = bookingService;
        }

        [HttpPost]
        [Route("/addrestaurant")]
        public async Task<IActionResult> CreateRestaurant([FromBody] AddRestaurantDTO addRestaurantDTO)
        {
            try
            {
                await _restaurantService.AddRestaurantAsync(addRestaurantDTO);
                return Created();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("/deleterestaurant/{restaurantId}")]
        public async Task<IActionResult> DeleteRestaurant(int restaurantId)
        {
            try
            {
                await _restaurantService.DeleteRestaurantAsync(restaurantId);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut]
        [Route("/updaterestaurant/{restaurantId}")]
        public async Task<IActionResult> UpdateRestaurant(int restaurantId, [FromBody] UpdateRestaurantDTO updateRestaurantDTO)
        {
            try
            {
                await _restaurantService.UpdateRestaurantAsync(restaurantId, updateRestaurantDTO);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet]
        [Route("/viewrestaurant/{restaurantId}")]
        public async Task<ActionResult<ViewRestaurantDTO>> GetRestaurantById(int restaurantId)
        {
            var restaurant = await _restaurantService.GetRestaurantByIdAsync(restaurantId);
            if (restaurant == null)
            {
                return NotFound("Restaurant not found");
            }
            return Ok(restaurant);
        }

        [HttpGet]
        [Route("/viewrestaurants")]
        public async Task<ActionResult<IEnumerable<ViewRestaurantDTO>>> GetAllRestaurants()
        {
            var restaurants = await _restaurantService.GetAllRestaurantsAsync();
            return Ok(restaurants);
        }

        [HttpGet]
        [Route("/viewmenu/{restaurantId}")]
        public async Task<ActionResult<IEnumerable<ViewMenuDTO>>> SeeMenu(int restaurantId)
        {
            var menu = await _restaurantService.SeeMenuAsync(restaurantId);
            return Ok(menu);
        }

        [HttpPost]
        [Route("/addmenuitem/{restaurantId}")]
        public async Task<IActionResult> AddDishOrDrink(int restaurantId, [FromBody] AddDishDTO dishDTO)
        {
            try
            {
                await _restaurantService.AddDishOrDrinkAsync(dishDTO);
                return CreatedAtAction(nameof(SeeMenu), new { restaurantId }, dishDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("/deletedish/{menuId}")]
        public async Task<IActionResult> DeleteDishOrDrink(int menuId)
        {
            try
            {
                await _restaurantService.DeleteDishOrDrinkAsync(menuId);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut]
        [Route("/updatemenuitem/{menuId}")]
        public async Task<IActionResult> UpdateDishOrDrink(int menuId, [FromBody] UpdateMenuDTO updateMenuDTO)
        {
            try
            {
                await _restaurantService.UpdateDishOrDrinkAsync(menuId, updateMenuDTO);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet]
        [Route("/viewtables")]
        public async Task<ActionResult<IEnumerable<ViewTableDTO>>> ViewAllTables()
        {
            var tables = await _restaurantService.GetAllTablesAsync();
            return Ok(tables);
        }

        [HttpGet]
        [Route("/viewmenus")]
        public async Task<ActionResult<IEnumerable<ViewMenuDTO>>> ViewAllMenus()
        {
            var menus = await _restaurantService.GetAllMenusAsync();
            return Ok(menus);
        }
      
        [HttpGet]
        [Route("/viewtables/{restaurantId}")]
        public async Task<ActionResult<IEnumerable<ViewTableDTO>>> ViewTables(int restaurantId)
        {
            var tables = await _restaurantService.GetTablesByRestaurantIdAsync(restaurantId);

            if (tables == null || !tables.Any())
            {
                return NoContent(); 
            }

            return Ok(tables); 
        }

        [HttpPost]
        [Route("/addtable")]
        public async Task<IActionResult> AddTable([FromBody] NewTableDTO newTableDTO)
        {
            await _restaurantService.AddTableAsync(newTableDTO);
            return Created("", new {message = "Table created successfully"}); 
        }

        [HttpPut]
        [Route("/updatetable/{tableId}")]
        public async Task<IActionResult> UpdateTable(int tableId,[FromBody] UpdateTableDTO updateTableDTO)
        {
            await _restaurantService.UpdateTableAsync(tableId, updateTableDTO);
            return NoContent(); 
        }

        [HttpDelete]
        [Route("/deletetable/{tableId}")]
        public async Task<IActionResult> DeleteTable(int tableId)
        {
            await _restaurantService.DeleteTableAsync(tableId);
            return NoContent(); 
        }
    }
}

