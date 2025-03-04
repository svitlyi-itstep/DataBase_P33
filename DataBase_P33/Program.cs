using MySql.Data;
using MySql.Data.MySqlClient;
using System.Text;


class Program
{
    public static void CreateTables(MySqlConnection connection)
    {
        string query = "CREATE TABLE IF NOT EXISTS Doctors ( " +
            "ID INT AUTO_INCREMENT PRIMARY KEY," +
            "Name VARCHAR(100)," +
            "Premium DECIMAL(15, 2)," +
            "Salary DECIMAL(15, 2)" +
            ");";
        using(MySqlCommand cmd = new MySqlCommand(query, connection))
        {
            cmd.ExecuteNonQuery();
        }
    }

    public static void AddDoctor(MySqlConnection connection, string name, 
        decimal premium, decimal salary)
    {
        string query = "INSERT INTO Doctors (Name, Premium, Salary) " +
            "VALUES (@name, @premium, @salary);";
        using(MySqlCommand cmd = new MySqlCommand(query, connection)) 
        {
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@premium", premium);
            cmd.Parameters.AddWithValue("@salary", salary);
            cmd.ExecuteNonQuery();
        }
    }

    public static void GetDoctors(MySqlConnection connection)
    {
        string query = "SELECT * FROM Doctors;";
        using(MySqlCommand cmd = new MySqlCommand(query, connection))
        using(MySqlDataReader reader = cmd.ExecuteReader()) 
        {
            while(reader.Read())
            {
                Console.WriteLine($"{reader["ID"], 3}, {reader["Name"], 20}, " +
                    $"{reader["Premium"], 8} грн., {reader["Salary"], 9} грн.");
            }
            
        }
    }

    public static void Main(string[] args)
    {
        Console.OutputEncoding = UTF8Encoding.UTF8;
        Console.InputEncoding = UTF8Encoding.UTF8;

        string db_host = "localhost";
        string db_database = "hospital_p33";
        string db_user = "root";
        string db_password = "";

        string connectionString = $"Server={db_host};Database={db_database};" +
            $"User ID={db_user};Password={db_password};";

        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                Console.WriteLine($"Successfully connected to data base {db_database}");
                CreateTables(connection);
                // AddDoctor(connection, "Іванов Іван", 200, 1000);
                GetDoctors(connection);
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error: {ex.Message}");
                Console.ResetColor();
            }
        }

    }
}
