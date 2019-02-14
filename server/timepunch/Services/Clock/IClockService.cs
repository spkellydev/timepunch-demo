using timepunch.Models;

namespace timepunch.Services
{
    public interface IClockService
    {
        ShiftModel CreateShift(ShiftModel shift);
    }
}