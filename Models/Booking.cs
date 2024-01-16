using System.ComponentModel.DataAnnotations;

namespace WebAppDemoStudent.Models
{
    public class Booking
    {
        public string Email { get; set; }
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int Weight { get; set; }
        public int Height { get; set; }
        public string Symptoms { get; set; }
        public string Doctor { get; set; }

    }
}
