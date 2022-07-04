using System;
using EmployeeTaskSolicy.Dto;
using EmployeeTaskSolicy.Model;

namespace EmployeeTaskSolicy.Extensions
{
    public static class EmployeeIdDtoExtensions
    {
        public static EmployeeIdDto ToEmployeeIdDto(this Employee employee)
        {
            return new EmployeeIdDto
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Age = employee.Age,
                Country = employee.Country
            };
        }

        public static IEnumerable<EmployeeIdDto> ToEmployeeIdDto(this IEnumerable<Employee> employees)
        {
            if(employees == null || !employees.Any())
            {
                return null;
            }

            return employees.Select(employee => employee.ToEmployeeIdDto());
        }
    }

}

