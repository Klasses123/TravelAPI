﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using TravelAPI.Infrastructure.Interfaces;

namespace TravelAPI.Core.Models
{
    [Table("Users")]
    public sealed class User : IDataModel
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Login { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public DateTime RegisteredOn { get; set; }
        public string RefreshToken { get; set; }
        public Company Company { get; set; }


        public override bool Equals(object obj)
        {
            if (obj is User u)
                return Equals(u);
            return false;
        }

        private bool Equals(User otherUser)
        {
            return Id == otherUser.Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }

        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }
    }
}
