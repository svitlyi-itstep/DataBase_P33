using Hospital;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Text;


class Program
{
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
                DBManager db = new DBManager(connection);

                Console.WriteLine($"Successfully connected to data base {db_database}");
                db.CreateTables();
                List<Doctor> doctors = db.GetDoctors();
                ShowDoctors(doctors);

                Console.ReadLine();
                db.AddDoctor(new Doctor("Биков Андрій", 340, 7400));
                doctors = db.GetDoctors();
                Doctor last_record = doctors.Last();
                ShowDoctors(doctors);

                Console.ReadLine();
                last_record.Name = "Биков Ілля";
                db.EditDoctor(last_record.ID, last_record);
                doctors = db.GetDoctors();
                last_record = doctors.Last();
                ShowDoctors(doctors);

                Console.ReadLine();
                db.RemoveDoctor(last_record.ID);
                doctors = db.GetDoctors();
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
