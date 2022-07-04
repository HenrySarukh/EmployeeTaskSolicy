using EmployeeTaskSolicy.Dto;
using EmployeeTaskSolicy.Repository;
using EmployeeTaskSolicy.Services;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeTaskSolicy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [Route("/error")]
        public IActionResult HandleError() =>
            Problem();

        [HttpGet]
        [Route("getEmployees")]
        public async Task<IActionResult> GetEmployees()
        {
            var employes = await _employeeService.GetEmployees();
            if (!employes.Any())
                return Ok("Data is empty!\nAdd Data!");
            return Ok(employes);
        }

        [HttpGet]
        [Route("getEmployee/{id}")]
        public async Task<IActionResult> GetEmployee([FromBody]int id)
        {
            if (id < 1)
                return BadRequest("Id must be positive number!");
            var employee = await _employeeService.GetEmployee(id);
            if (employee == null)
                return Ok($"There is no employee with Id = {id}");
            return Ok(employee);
        }

        [HttpPost]
        [Route("addEmployee")]
        public async Task<IActionResult> AddEmployee([FromBody]EmployeeDto employee)
        {
            var result = await _employeeService.AddEmployee(employee);
            return Ok(result);
        }

        [HttpDelete]
        [Route("deleteEmployee")]
        public async Task<IActionResult> DeleteEmployee([FromBody]int id)
        {
            var result = await _employeeService.DeleteEmployee(id);
            if(result)
                return Ok(result);
            return BadRequest($"Employee with Id = {id} does not exist");
        }

        [HttpPut]
        [Route("updateEmployee")]
        public async Task<IActionResult> UpdateEmployee([FromBody]EmployeeIdDto employee)
        {
            var result = await _employeeService.UpdateEmployee(employee);
            if (result == null)
                return BadRequest($"Employee with Id = {employee.Id} does not exist");
            return Ok(result);
        }
    }
}
