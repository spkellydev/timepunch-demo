using System.ComponentModel.DataAnnotations;

namespace timepunch.Models
{
    public class ShiftModel
    {
        [Key]
        public int ShiftId { get; set; }
        public double AccruedHours { get; set; }
        public double AllowedHours { get; set; }
        public bool OvertimeAllowed { get; set; }
        public int PayPeriodDuration { get; set; }
    }
}