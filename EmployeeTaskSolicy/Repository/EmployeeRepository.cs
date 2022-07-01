using Dapper;
using EmployeeTaskSolicy.Context;
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

        public async Task<string> AddEmployee(string FirstName, string LastName, int Age, string Country)
        {
            var query = "INSERT INTO Employes (Id, FirstName, LastName, Age, Country) VALUES (@Id, @FirstName, @LastName, @Age, @Country)";
            var queryId = "SELECT MAX(Id) FROM Employes";
            using (var connection = _context.CreateConnection())
            {
                connection.Open();
                int Id = connection.ExecuteScalar<int>(queryId);
                Id++;
                await connection.ExecuteAsync(query, new { Id, FirstName, LastName, Age, Country });
                return "Employee added with parametrs:\n" +
                   $"Id:{Id}; FirstName:{FirstName}; LastName:{LastName}; Age:{Age}; Country:{Country}";
            }
        }

        public async Task<string> CreateDataEmployes()
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

        public async Task<string> DeleteEmployee(int Id)
        {
            var query = "DELETE FROM Employes WHERE Id = @Id";
            var parameters = new DynamicParameters();
            parameters.Add("Id", Id);
            using (var connection = _context.CreateConnection())
            {
                var employes = await connection.ExecuteAsync(query, parameters);
                if (employes == 0)
                    return $"There is no Employee with Id = {Id}";
                return $"Employee with Id - {Id} Deleted!";
            }
        }

        public async Task<IEnumerable<Employee>> GetEmployes()
        {
            var query = "SELECT * FROM Employes";
            using (var connection = _context.CreateConnection())
            {
                var employes = await connection.QueryAsync<Employee>(query);
                return employes.ToList();
            }
        }

        public async Task<IEnumerable<Employee>> GetEmployee(int Id)
        {
            var query = "SELECT * FROM Employes WHERE Id = @Id";
            var parameters = new DynamicParameters();
            parameters.Add("Id", Id);

            using (var connection = _context.CreateConnection())
            {
                var employes = await connection.QueryAsync<Employee>(query, parameters);
                return employes.ToList();
            }
        }

        public async Task<string> UpdateEmployee(int Id, string FirstName, string LastName, int Age, string Country)
        {
            var query = "UPDATE Employes\n" +
                        "SET FirstName = @FirstName, LastName = @LastName, Age = @Age, Country = @Country\n" +
                        "WHERE Id = @Id;";
            using (var connection = _context.CreateConnection())
            {
                int employes = await connection.ExecuteAsync(query, new { FirstName, LastName, Age, Country, Id });
                if (employes == 0)
                    return $"There is no Employee with Id = {Id}";
                return $"Employee with Id - {Id} Updated!\n" +
                    $"Id: {Id}; FirstName: {FirstName}; LastName: {LastName}; Age: {Age}; Country: {Country}"; ;
            }
        }

    }
}
