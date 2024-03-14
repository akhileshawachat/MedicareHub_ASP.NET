using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppDemoStudent.Models
{
    public class OrderEntity
    {
        public int Id { get; set; }
        public string Customername { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public double TotalAmount { get; set; }

        [NotMapped]
        public string TransactionId {  get; set; }

        [NotMapped]
        public string OrderId {  get; set; }
    }
}
