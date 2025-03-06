using MySql.Data;
using MySql.Data.MySqlClient;

namespace Hospital
{
    class DBManager
    {
        MySqlConnection connection;

        public DBManager(MySqlConnection connection)
        {
            this.connection = connection;
        }

        public void CreateTables()
        {
            string query = "CREATE TABLE IF NOT EXISTS Doctors ( " +
                "ID INT AUTO_INCREMENT PRIMARY KEY," +
                "Name VARCHAR(100)," +
                "Premium DECIMAL(15, 2)," +
                "Salary DECIMAL(15, 2)" +
                ");";
            using (MySqlCommand cmd = new MySqlCommand(query, connection))
            {
                cmd.ExecuteNonQuery();
            }
        }

        public void AddDoctor(Doctor doctor)
        {
            string query = "INSERT INTO Doctors (Name, Premium, Salary) " +
                "VALUES (@name, @premium, @salary);";
            using (MySqlCommand cmd = new MySqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@name", doctor.Name);
                cmd.Parameters.AddWithValue("@premium", doctor.Premium);
                cmd.Parameters.AddWithValue("@salary", doctor.Salary);
                cmd.ExecuteNonQuery();
            }
        }

        public int RemoveDoctor(int id)
        {
            string query = "DELETE FROM Doctors WHERE ID=@id;";
            using (MySqlCommand cmd = new MySqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@id", id);
                return cmd.ExecuteNonQuery();
            }
        }

        public int EditDoctor(int id, Doctor update)
        {
            string query = "UPDATE Doctors " +
                "SET ID=@id, Name=@name, Premium=@premium, Salary=@salary " +
                "WHERE ID=@target_id;";
            using (MySqlCommand cmd = new MySqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@target_id", id);
                cmd.Parameters.AddWithValue("@id", update.ID);
                cmd.Parameters.AddWithValue("@name", update.Name);
                cmd.Parameters.AddWithValue("@premium", update.Premium);
                cmd.Parameters.AddWithValue("@salary", update.Salary);
                return cmd.ExecuteNonQuery();
            }
        }

        public List<Doctor> GetDoctors()
        {
            string query = "SELECT * FROM Doctors;";
            List<Doctor> doctors = new List<Doctor>();
            using (MySqlCommand cmd = new MySqlCommand(query, connection))
            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
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
    }
    
    class Doctor
    {
        public int ID { get; set; }
        public string? Name { get; set; }
        public decimal Premium { get; set; }
        public decimal Salary { get; set; }

        public Doctor(int id, string? name, decimal premium, decimal salary)
        {
            ID = id;
            Name = name;
            Premium = premium;
            Salary = salary;
        }

        public Doctor(string? name, decimal premium, decimal salary)
            : this(0, name, premium, salary) { }

        public Doctor() : this(0, "", 0, 0) { }
    }

    /*
        1. Створити клас Doctor, в якому зберігається
            інформація про лікаря

        2. Змінити метод GetDoctors таким чином, 
            щоб він не виводив таблицю з лікарями, а
            повертав список об`єктів класу Doctor.
            Список лікарів — List<Doctor>

        3. Створити функцію ShowDoctors, яка приймає
            список лікарів та виводить його на екран
            у вигляді таблиці. Реалізувати також
            шапку таблиці.
    */
}
