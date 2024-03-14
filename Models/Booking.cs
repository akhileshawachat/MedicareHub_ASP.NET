using System.ComponentModel.DataAnnotations;

namespace WebAppDemoStudent.Models
{
    public class Booking
    {
        public string Email { get; set; }
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter a date.")]
        [FutureDate(ErrorMessage = "Please enter a future date.")]
        public DateTime Date { get; set; }

        [RegularExpression(@"^(?:[5-9]|[1-9][0-9]|[12][0-9]{2}|300)$", ErrorMessage = "Weight must be between 5 and 300.")]
        public int Weight { get; set; }

        [RegularExpression(@"^(?:[1-2][0-9]{2}|300|350)$", ErrorMessage = "Height must be between 100 and 350.")]
        public int Height { get; set; }

        [RegularExpression("^[A-Za-z ]+$", ErrorMessage = "Symptoms should contain only alphabets.")]
        public string Symptoms { get; set; }
        public string Doctor { get; set; }


    }
}
