using EmployeeTaskSolicy.Context;
using EmployeeTaskSolicy.Repository;

namespace EmployeeTaskSolicy.CreateDB
{
    //Create DB if it is not exist
    public static class CreateDB
    {
        public static async void Create(EmployeeContext context)
        {
            EmployeeRepository employeeRepo = new EmployeeRepository(context);
            await employeeRepo.CreateDataEmployees();
        }
    }

}