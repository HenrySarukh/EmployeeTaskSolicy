using System;
using EmployeeTaskSolicy.Dto;
using EmployeeTaskSolicy.Model;

namespace EmployeeTaskSolicy.Services
{
    public interface IEmployeeService
    {
        public Task<IEnumerable<EmployeeIdDto>> GetEmployees();
        public Task<EmployeeIdDto> GetEmployee(int id);
        public Task<EmployeeIdDto> AddEmployee(EmployeeDto employee);
        public Task<bool> DeleteEmployee(int id);
        public Task<EmployeeIdDto> UpdateEmployee(EmployeeIdDto employee);
    }
}

