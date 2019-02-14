using System;
using timepunch.Models;

namespace timepunch.Services
{
    public class ClockService : IClockService
    {
        private readonly TimepunchContext ctx;
        public ClockService(TimepunchContext _ctx) { ctx = _ctx; }
        public ShiftModel CreateShift(ShiftModel shift)
        {            
            var createdShift = ctx.Add(shift);
            var saved = ctx.SaveChanges();
            if (saved < 1) throw new Exception("could not be created");
            return createdShift.Entity;
        }
    }
}