namespace Avtos
{
    class Avto
    {
        private string car_number; // номер автомобиля
        private float fuel; // количество бензина в баке автомобиля
        private float consumption; // расход топлива на 100 км
        private float distance; // пройденное расстояние
        private float speed; // скорость автомобиля
        private float mileage; // пробег автомобиля
        private float totalmileage; // общий пробег автомобиля

        public void info() // заполнение информации об автомобиле
        {
            Console.Write("Введите номер автомобиля: ");
            car_number = Console.ReadLine();

            Console.Write("Введите объем топлива в баке: ");
            fuel = (Convert.ToSingle(Console.ReadLine()));

            Console.Write("Введите расход топлива на 100 км: ");
            consumption = (Convert.ToSingle(Console.ReadLine()));

            Console.Write("Введите пробег автомобиля: ");
            mileage = (Convert.ToSingle(Console.ReadLine()));

            Random rnd = new Random();
            distance = rnd.Next(1, 3000);
            speed = rnd.Next(10, 300);
        }

        public void output() // вывод информации об автомобиле на экран
        {
            Console.WriteLine("ИНФОРМАЦИЯ ОБ АВТОМОБИЛЕ");
            Console.WriteLine($"Номер автомобиля: {car_number}");
            Console.WriteLine($"Объем топлива в баке: {fuel} л");
            Console.WriteLine($"Расход топлива на 100 км: {consumption} л");
            Console.WriteLine($"Пробег автомобиля: {mileage} км");
            Console.WriteLine($"Пройденное расстояние: {distance} км");
            Console.WriteLine($"Скорость автомобиля: {speed} км/ч");
            total_mileage();
            Console.ReadKey();
        }

        public void move() // езда
        {
            float km_current_fuel = ((fuel / consumption)) * 100; // расстояние, которое автомобиль проедет за текущий объем топлива
            float missing_fuel = (distance * consumption) / 100 - fuel; // недостающее количество топлива для поездки
            float remainingfuel = (distance * consumption) / 100; // топливо, отставшееся после поездки

            if (km_current_fuel >= distance) // проверка, хватит ли топлива на всю дорогу
            {
                Console.WriteLine($"\nСейчас в баке {fuel} л топлива, вы сможете проехать {km_current_fuel} км");
                Console.WriteLine($"По окончании поездки в баке останется {fuel - remainingfuel} л топлива");

                Console.WriteLine("\nВы можете разогнаться или притормозить. Выберите одно из действий:");
                Console.WriteLine("1. Разогнаться \n2. Притормозить");
                Console.Write("Ваш ответ: ");

                int user_choise = Convert.ToInt32(Console.ReadLine());
                if (user_choise == 1)
                {
                    acceleration(); // разгон автомобиля
                }
                else
                {
                    braking(); // торможение автомобиля
                }
                Console.ReadKey();
            }
            else
            {
                remaining_fuel(km_current_fuel, missing_fuel); // заправка автомобиля
            }
        }

        public void acceleration() // разгон автомобиля
        {
            Random rnd = new Random();
            speed += rnd.Next(5, 20); // рандомное прибавление к скорости
            Console.WriteLine($"\nВаша скорость увеличилась до {speed} км/ч");
        }

        private void braking() // торможение автомобиля
        {
            Random rnd = new Random();
            speed -= rnd.Next(5, 20); // рандомное убавление от скорости
            Console.WriteLine($"\nВаша скорость уменьшилась до {speed} км/ч");
        }

        public void remaining_fuel(float can_drive, float filling_fuel) // остаток топлива в баке
        {
            Console.WriteLine($"\nПри объеме топлива в баке в {fuel} л вы сможете проехать {can_drive} км");
            Console.WriteLine($"Чтобы проехать {distance} км, вам нужно дополнительно залить в бак {filling_fuel} л топлива");
            refilling();
            Console.WriteLine($"В вашем баке теперь {fuel} л топлива");
            move();
        }

        public void refilling() // заправка автомобиля
        {
            Console.Write("Введите количество топлива, которое нужно залить: ");
            float fuel_to_be_filled = Convert.ToInt32(Console.ReadLine()); // количество бензина, которое нужно залить
            fuel += fuel_to_be_filled;
        }

        public void total_mileage() // общий пробег с учетом пройденного расстояния
        {
            totalmileage = mileage + distance; // расчёт общего пробега с учетом пройденного расстояния
            Console.WriteLine($"Общий пробег после поездки: {totalmileage} км");
        }

        public void accidents() // аварии
        {
            Random rnd = new Random();
            float i = rnd.Next(1, 5);
            float j = rnd.Next(1, 5);
            if (i != j)
            {
                Console.WriteLine($"Машины {i} и {j} попали в аварию!");
            }
            else
            {
                Console.WriteLine("Сегодня на дорогах никаких аварий не было!");
            }
        }
    }
}