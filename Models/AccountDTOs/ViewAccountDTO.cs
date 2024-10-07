﻿using System.ComponentModel.DataAnnotations;

namespace ForkyWebAPI.Models.AccountDTOs
{
    public class ViewAccountDTO
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
    }
}
