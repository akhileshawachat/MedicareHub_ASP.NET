
using System.ComponentModel.DataAnnotations;

namespace WebAppDemoStudent.Models
{
    internal class FutureDateAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return false;
            }

            DateTime dateTime;
            if (DateTime.TryParse(value.ToString(), out dateTime))
            {
                return dateTime.Date > DateTime.Now.Date;
            }

            return false;
        }

        public override string FormatErrorMessage(string name)
        {
            return $"{name} must be a future date.";
        }
    }
}