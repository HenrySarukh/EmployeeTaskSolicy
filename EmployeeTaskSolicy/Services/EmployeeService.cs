using System;
using EmployeeTaskSolicy.Extensions;
using EmployeeTaskSolicy.Dto;
using EmployeeTaskSolicy.Model;
using EmployeeTaskSolicy.Repository;

namespace EmployeeTaskSolicy.Services
{
    public class EmployeeService : IEmployeeService
    {
        private IEmployeeRepository _repository;

        public EmployeeService(IEmployeeRepository repository)
        {
            _repository = repository;
        }

        public async Task<EmployeeIdDto> AddEmployee(EmployeeDto employeeDto)
        {
            var employee = await _repository.AddEmployee(employeeDto);
            return employee?.ToEmployeeIdDto();
        }

        public async Task<bool> DeleteEmployee(int id)
        {
            if (id < 1)
                return false;
            return await _repository.DeleteEmployee(id);
        }

        public async Task<EmployeeIdDto> GetEmployee(int id)
        {
            if(id < 1)
                return null;
            var employee = await _repository.GetEmployee(id);
            return employee?.ToEmployeeIdDto();
        }

        public async Task<IEnumerable<EmployeeIdDto>> GetEmployees()
        {
            var employes = await _repository.GetEmployees();
            return employes?.ToEmployeeIdDto();
        }

        public async Task<EmployeeIdDto> UpdateEmployee(EmployeeIdDto employeeIdDto)
        {
            var employee = await _repository.UpdateEmployee(employeeIdDto);
            return employee?.ToEmployeeIdDto();
        }
    }
}

