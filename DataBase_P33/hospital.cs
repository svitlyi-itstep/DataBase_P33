
namespace Hospital
{
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
