using Microsoft.AspNetCore.Mvc;
using timepunch.Models;
using timepunch.Services;

namespace timepunch.Controllers
{
    [Route("v1/api/clock")]
    [ApiController]
    public class ClockController
    {
        private readonly IClockService _repo;
        public ClockController(IClockService repo) { _repo = repo; }

        [HttpPost]
        [Route("shift")]
        public ShiftModel CreateShift(ShiftModel shift)
        {
            return _repo.CreateShift(shift);
        }
    }
}