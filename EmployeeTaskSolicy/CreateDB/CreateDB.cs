using EmployeeTaskSolicy.Context;
using EmployeeTaskSolicy.Repository;

namespace EmployeeTaskSolicy.CreateDB
{
    public static class CreateDB
    {
        public static async void Create(EmployeeContext context)
        {
            EmployeeRepository employeeRepo = new EmployeeRepository(context);
            await employeeRepo.CreateDataEmployees();
        }
    }

}