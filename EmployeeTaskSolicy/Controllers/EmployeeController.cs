using EmployeeTaskSolicy.Dto;
using EmployeeTaskSolicy.Repository;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeTaskSolicy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepo;

        public EmployeeController(IEmployeeRepository employeeRepo)
        {
            _employeeRepo = employeeRepo;
        }

        [Route("/Error")]
        public IActionResult HandleError() =>
            Problem();

        [HttpGet]
        [Route("GetEmployes")]
        public async Task<IActionResult> GetEmployes()
        {
            try
            {
                var employes = await _employeeRepo.GetEmployes();
                if (employes == null || (employes.ToList()).Count == 0)
                    return Ok("Data is empty!\nAdd Data!");
                return Ok(employes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Route("GetEmployee/{id}")]
        public async Task<IActionResult> GetEmployee(int id)
        {
            try
            {
                if (id < 1)
                    return BadRequest("Id must be positive number!");
                var employes = await _employeeRepo.GetEmployee(id);
                if (employes == null || (employes.ToList()).Count == 0)
                    return Ok($"There is no employee with Id = {id}");
                return Ok(employes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        [Route("AddEmployee")]
        public async Task<IActionResult> AddEmployee([FromBody] EmployeeDto employee)
        {
            try
            {
                int age;
                if(employee.Id != null)
                    return BadRequest("Data is incorrect\nDo not input Id,We will make it ourselves for your data security!");
                if (employee.Age == null || employee.FirstName == null || employee.Country == null || employee.LastName == null)
                    return BadRequest("Data is incorrect");
                age = (int)employee.Age;
                var result = await _employeeRepo.AddEmployee(employee.FirstName, employee.LastName, age, employee.Country);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete]
        [Route("DeleteEmployee")]
        public async Task<IActionResult> DeleteEmployee([FromBody] EmployeeDto employee)
        {
            try
            {
                int id;
                if(employee.Id == null)
                    return BadRequest("Input Employee's Id,which you want to delete from DB");
                if (employee.Age != null || employee.FirstName != null || employee.Country != null || employee.LastName != null)
                    return BadRequest("Please input only Employee's Id,which you want to delete from DB                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                ");
                id = (int)employee.Id;
                var result = await _employeeRepo.DeleteEmployee(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut]
        [Route("UpdateEmployee")]
        public async Task<IActionResult> UpdateEmployee([FromBody] EmployeeDto employee)
        {
            try
            {
                int id, age;
                if (employee.Id == null || employee.Age == null || employee.FirstName == null || employee.Country == null || employee.LastName == null)
                    return BadRequest("Data is incorrect");
                age = (int)employee.Age;
                id = (int)employee.Id;
                string result = await _employeeRepo.UpdateEmployee(id, employee.FirstName, employee.LastName, age, employee.Country);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
