// File: Models/ReservationModel.cs
using System;
using System.ComponentModel.DataAnnotations;

namespace ParkIn.Models
{
    public class ReservationModel
    {
        [Required]
        public DateTime Date { get; set; }

        [Required]
        public string Time { get; set; }

        [Required]
        public string EndTime { get; set; }
    }
}
