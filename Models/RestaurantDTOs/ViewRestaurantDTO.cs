﻿namespace ForkyWebAPI.Models.RestaurantDTOs
{
    public class ViewRestaurantDTO
    {
        public int Id { get; set; }
        public string RestaurantName { get; set; }
        public string TypeOfRestaurant { get; set; }
        public string Location { get; set; }
        public string? AdditionalInformation { get; set; }
    }
}