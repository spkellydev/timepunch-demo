using timepunch.Models;

namespace timepunch.Services
{
    public class UserService : IUserService
    {
        private readonly TimepunchContext _ctx;
        public UserService(TimepunchContext ctx) { _ctx = ctx; }
        public UserProfileModel CreateProfile()
        {
            return new UserProfileModel{
                Id = new System.Guid(),
                User = new UserModel(),
                Employee = new EmployeeModel{
                    EmployeeCompany = new CompanyModel{
                        CompanyName = "Name",
                        CompanyDescription = "Description",
                        CompanyLocation = "Here",
                        CompanyContact = "Me"
                    },
                    EmployeePosition = "Developer",
                    EmployeeSalary = 49999.98,
                    Shift = new ShiftModel{
                        AccruedHours = 0.00,
                        AllowedHours = 32.00,
                        OvertimeAllowed = false,
                        PayPeriodDuration = 14
                    }
                }
            };
        }
    }
}