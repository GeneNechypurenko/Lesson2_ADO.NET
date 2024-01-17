using System.Data.SqlClient;

namespace Lesson2
{
    internal class Program
    {
        static string connectionString = "Server=localhost;Database=Office;Trusted_Connection=True;TrustServerCertificate=True;";
        static void Main(string[] args)
        {
            DisplayWorkersByDepartment(1);
            DisplayWorkersByCriteria(4, 45000);
            GetEmployeeWithMinSalary();
            IncreaseSalary(5000, 1);
            DisplayWorkersByDepartment(1);
        }

        static void DisplayWorkersByDepartment(int departmentId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new ($"SELECT * FROM Worker WHERE DepartmentId = {departmentId}", connection);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"ID: {reader["id"]}, Name: {reader["Name"]}, Position: {reader["Position"]}, Salary: {reader["Salary"]}");
                    }
                }
            }
        }
        static void DisplayWorkersByCriteria(int nameLength, decimal minSalary)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand($"SELECT * FROM Worker WHERE LEN(Name) = {nameLength} AND Salary >= {minSalary}", connection);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"ID: {reader["id"]}, Name: {reader["Name"]}, Position: {reader["Position"]}, Salary: {reader["Salary"]}");
                    }
                }
            }
        }

        static void GetEmployeeWithMinSalary()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new ("SELECT TOP 1 * FROM Worker ORDER BY Salary", connection);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"ID: {reader["id"]}, Name: {reader["Name"]}, Position: {reader["Position"]}, Salary: {reader["Salary"]}");
                    }
                }
            }
        }
        static void IncreaseSalary(decimal increaseAmount, int? departmentId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                if (departmentId != null)
                {
                    SqlCommand command = new
                        ($"UPDATE Worker" +
                        $" SET Salary = Salary + {increaseAmount}" +
                        $" WHERE DepartmentId = {departmentId}",
                        connection);

                    command.ExecuteNonQuery();
                }

            }
        }
    }
}
