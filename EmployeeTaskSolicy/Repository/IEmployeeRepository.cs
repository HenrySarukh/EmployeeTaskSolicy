using EmployeeTaskSolicy.Dto;
using EmployeeTaskSolicy.Model;

namespace EmployeeTaskSolicy.Repository
{
    public interface IEmployeeRepository
    {
        public Task<IEnumerable<Employee>> GetEmployees();
        public Task<Employee> GetEmployee(int id);
        public Task<string> CreateDataEmployees();
        public Task<Employee> AddEmployee(EmployeeDto employeeDto);
        public Task<Employee> UpdateEmployee(EmployeeIdDto employeeIdDto);
        public Task<bool> DeleteEmployee(int Id);

    }
}
