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
        public async Task<IActionResult> GetEmployees()
        {
            var employes = await _employeeService.GetEmployees();
            if (!employes.Any())
                return NotFound("Data is empty!\nAdd Data!");
            return Ok(employes);
        }

        [HttpGet]
        [Route("{id:int:min(1)}")]
        public async Task<IActionResult> GetEmployee([FromBody]int id)
        {
            if (id < 1)
                return BadRequest("Id must be positive number!");
            var employee = await _employeeService.GetEmployee(id);
            if (employee == null)
                return NotFound($"There is no employee with Id = {id}");
            return Ok(employee);
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee([FromBody]EmployeeDto employee)
        {
            var result = await _employeeService.AddEmployee(employee);
            return Ok(result);
        }

        [HttpDelete]
        [Route("{id:int:min(1)}")]
        public async Task<IActionResult> DeleteEmployee([FromBody]int id)
        {
            var result = await _employeeService.DeleteEmployee(id);
            if(result)
                return Ok(result);
            return NotFound($"Employee with Id = {id} does not exist");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateEmployee([FromBody]EmployeeIdDto employee)
        {
            var result = await _employeeService.UpdateEmployee(employee);
            if (result == null)
                return NotFound($"Employee with Id = {employee.Id} does not exist");
            return Ok(result);
        }
    }
}
