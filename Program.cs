namespace Automobiles
{
    class Program
    {
        static void Main(string[] args)
        {
            Auto auto = new Auto("", 0, 0, 0, 0, 0);
            Auto[] cars = new Auto[3]; // объявление и создание объекта "автомобиль"
            Random rnd = new Random();

            for (int i = 0; i < cars.Length; i++)
            {
                Console.WriteLine($"Автомобиль {i + 1}");
                Console.Write("Введите номер автомобиля: ");
                string carNumber = Console.ReadLine();

                Console.Write("Введите объем топлива в баке (до 60 л): ");
                float fuel = Convert.ToSingle(Console.ReadLine());
                if (fuel <= 0 || fuel > 60) { fuel = Convert.ToSingle(Auto.CheckingFuel(fuel)); } // вызов проверки введенного значения

                Console.Write("Введите расход топлива на 100 км: ");
                float consumption = Convert.ToSingle(Console.ReadLine());
                if (consumption <= 0) { consumption = Convert.ToSingle(Auto.Checking(consumption)); }

                Console.Write("Введите текущий пробег автомобиля: ");
                float mileage = Convert.ToSingle(Console.ReadLine());
                if (mileage <= 0) { mileage = Convert.ToSingle(Auto.Checking(mileage)); }

                float distance = rnd.Next(1, 100);
                float speed = rnd.Next(60, 120);
                Console.WriteLine();

                cars[i] = new Auto(carNumber, fuel, consumption, mileage, distance, speed);
            }

            auto.Menu(cars);
        }
    }
}