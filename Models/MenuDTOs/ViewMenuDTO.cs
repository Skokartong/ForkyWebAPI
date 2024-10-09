﻿using System.ComponentModel.DataAnnotations;

namespace ForkyWebAPI.Models.MenuDTOs
{
    public class ViewMenuDTO
    {
        public int Id { get; set; }
        public string NameOfDish { get; set; }

        public string Drink { get; set; }

        public bool IsAvailable { get; set; }

        public string? Ingredients { get; set; }

        public double Price { get; set; }
        public int FK_RestaurantId { get; set; }
    }
}
