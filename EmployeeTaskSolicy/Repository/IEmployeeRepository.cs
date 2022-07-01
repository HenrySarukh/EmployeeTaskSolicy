using EmployeeTaskSolicy.Model;

namespace EmployeeTaskSolicy.Repository
{
    public interface IEmployeeRepository
    {
        public Task<IEnumerable<Employee>> GetEmployes();
        public Task<IEnumerable<Employee>> GetEmployee(int Id);
        public Task<string> CreateDataEmployes();
        public Task<string> AddEmployee(string FirstName, string LastName, int Age, string Country);
        public Task<string> UpdateEmployee(int Id, string FirstName, string LastName, int Age, string Country);
        public Task<string> DeleteEmployee(int Id);

    }
}
