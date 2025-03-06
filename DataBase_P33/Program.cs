using Hospital;
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

    public static void AddDoctor(MySqlConnection connection, Doctor doctor)
    {
        string query = "INSERT INTO Doctors (Name, Premium, Salary) " +
            "VALUES (@name, @premium, @salary);";
        using(MySqlCommand cmd = new MySqlCommand(query, connection)) 
        {
            cmd.Parameters.AddWithValue("@name", doctor.Name);
            cmd.Parameters.AddWithValue("@premium", doctor.Premium);
            cmd.Parameters.AddWithValue("@salary", doctor.Salary);
            cmd.ExecuteNonQuery();
        }
    }

    public static List<Doctor> GetDoctors(MySqlConnection connection)
    {
        string query = "SELECT * FROM Doctors;";
        List<Doctor> doctors = new List<Doctor>();
        using(MySqlCommand cmd = new MySqlCommand(query, connection))
        using(MySqlDataReader reader = cmd.ExecuteReader()) 
        {
            while(reader.Read())
            {
                doctors.Add(new Doctor(
                    (int)reader["ID"],
                    (string)reader["Name"],
                    (decimal)reader["Premium"],
                    (decimal)reader["Salary"]
                ));
            }
            
        }
        return doctors;
    }

    public static void ShowDoctors(IEnumerable<Doctor> doctors)
    {
        Console.WriteLine($"| {"ID", 3} | {"Назва", 20} | {"Премія",13} | " +
            $"{"Зарплатня", 14} |");
        foreach(Doctor doctor in doctors)
            Console.WriteLine($"| {doctor.ID,3} | {doctor.Name,20} | " +
                    $"{doctor.Premium,8} грн. | {doctor.Salary,9} грн. |");
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
                AddDoctor(connection, new Doctor("Іванов Іван", 200, 1000));
                List<Doctor> doctors = GetDoctors(connection);
                ShowDoctors(doctors);
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
