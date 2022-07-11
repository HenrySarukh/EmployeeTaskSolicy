using Dapper;
using EmployeeTaskSolicy.Context;
using EmployeeTaskSolicy.Dto;
using EmployeeTaskSolicy.Model;

namespace EmployeeTaskSolicy.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly EmployeeContext _context;
        public EmployeeRepository(EmployeeContext context)
        {
            _context = context;
        }

        public async Task<Employee> AddEmployee(EmployeeDto employeeDto)
        {
            var query = "INSERT INTO Employes (Id, FirstName, LastName, Age, Country) VALUES (@Id, @FirstName, @LastName, @Age, @Country)";
            var queryId = "SELECT MAX(Id) FROM Employes";
            using (var connection = _context.CreateConnection())
            {
                connection.Open();
                int id = connection.ExecuteScalar<int>(queryId);
                id++;
                await connection.ExecuteAsync(query, new { id, employeeDto.FirstName, employeeDto.LastName, employeeDto.Age, employeeDto.Country });
                return new Employee
                {
                    Id = id,
                    Age = employeeDto.Age,
                    Country = employeeDto.Country,
                    FirstName = employeeDto.FirstName,
                    LastName = employeeDto.LastName
                };
            }
        }

        public async Task<string> CreateDataEmployees()
        {
            var query = "SELECT * FROM sys.databases WHERE name = @name";
            var parameters = new DynamicParameters();
            parameters.Add("name", "DbEmployes");

            using (var connection = _context.CreateConnection())
            {
                var records = await connection.QueryAsync(query, parameters);
                if (!records.Any())
                {
                    connection.Execute($"CREATE DATABASE DbEmployes;" +
                    "USE DbEmployes;" +
                    "GO" +
                    "CREATE TABLE Employes" +
                    "(" +
                    "Id INT IDENTITY PRIMARY KEY," +
                    "FirstName NVARCHAR(50) NOT NULL," +
                    "LastName NVARCHAR(50) NOT NULL," +
                    "Age INT NOT NULL," +
                    "Country NVARCHAR(50) NOT NULL" +
                    ")");
                    return "DATABASE is created!";
                }
            }
            return "DATABASE already exists!";
        }

        public async Task<bool> DeleteEmployee(int Id)
        {
            var query = "DELETE FROM Employes WHERE Id = @Id";
            var parameters = new DynamicParameters();
            parameters.Add("Id", Id);
            using (var connection = _context.CreateConnection())
            {
                var employes = await connection.ExecuteAsync(query, parameters);
                if (employes == 0)
                    return false;
                return true;
            }
        }

        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            var query = "SELECT * FROM Employes";
            using (var connection = _context.CreateConnection())
            {
                var employes = await connection.QueryAsync<Employee>(query);
                return employes;
            }
        }

        public async Task<Employee> GetEmployee(int id)
        {
            var query = "SELECT * FROM Employes WHERE Id = @Id";
            var parameters = new DynamicParameters();
            parameters.Add("Id", id);

            using (var connection = _context.CreateConnection())
            {
                var employee = await connection.QueryFirstAsync<Employee>(query, parameters);
                return employee;
            }
        }

        public async Task<Employee> UpdateEmployee(EmployeeIdDto employeeIdDto)
        {
            var query = "UPDATE Employes\n" +
                        "SET FirstName = @FirstName, LastName = @LastName, Age = @Age, Country = @Country\n" +
                        "WHERE Id = @Id;";
            using (var connection = _context.CreateConnection())
            {
                int employees = await connection.ExecuteAsync(query, new { employeeIdDto.FirstName, employeeIdDto.LastName,
                                                                            employeeIdDto.Age, employeeIdDto.Country});
                if (employees == 0)
                    return null;
                return new Employee
                {
                    Id = employeeIdDto.Id,
                    Age = employeeIdDto.Age,
                    Country = employeeIdDto.Country,
                    FirstName = employeeIdDto.FirstName,
                    LastName = employeeIdDto.LastName
                };
            }
        }

    }
}
