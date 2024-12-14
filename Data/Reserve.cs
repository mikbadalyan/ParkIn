using System.ComponentModel.DataAnnotations;

namespace ParkIn.Data
{
    public class Reserve
    {
        [Key]
        public int ReservationId { get; set; }
        public int UserId { get; set; }
        public DateTime ReservationDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
