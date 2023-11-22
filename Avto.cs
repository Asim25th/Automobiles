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
        private float x = 0; // координата X
        private float time; // время в пути
        private float distance_traveled = 0; // пройденное расстояние
        private Random rnd = new Random();

        public void Info() // заполнение информации об автомобиле
        {
            Console.Write("Введите номер автомобиля: ");
            car_number = Console.ReadLine();

            Console.Write("Введите объем топлива в баке (до 60 л): ");
            fuel = Convert.ToSingle(Console.ReadLine());
            if (fuel <= 0 || fuel > 60) { fuel = Convert.ToSingle(CheckingFuel(fuel)); } // вызов проверки введенного значения

            Console.Write("Введите расход топлива на 100 км: ");
            consumption = Convert.ToSingle(Console.ReadLine());
            if (consumption <= 0) { consumption = Convert.ToSingle(Checking(consumption)); }

            Console.Write("Введите текущий пробег автомобиля: ");
            mileage = Convert.ToSingle(Console.ReadLine());
            if (mileage <= 0) { mileage = Convert.ToSingle(Checking(mileage)); }

            distance = rnd.Next(1, 100);
            speed = rnd.Next(60, 120);

            Console.Clear();
            Output();
        }

        private double Checking(float a) // проверка на то, чтобы введенные данные были больше нуля
        {
            while (a <= 0)
            {
                Console.Write("ОШИБКА: Введенное значение должно быть больше нуля.\nПопробуйте ввести значение заново: ");
                a = float.Parse(Console.ReadLine());
            }
            return a; // возвращение измененного значения
        }

        private double CheckingFuel(float fuel) // проверка на то, что топлива больше 0 и меньше 60 л
        {
            while (fuel <= 0 || fuel > 60)
            {
                Console.Write("ОШИБКА: Топлива в баке должно быть больше 0 и меньше 60 л.\nПопробуйте ввести значение заново: ");
                fuel = float.Parse(Console.ReadLine());
            }
            return fuel; // возвращение измененного значения
        }

        public void Output() // вывод информации об автомобиле на экран
        {
            Console.WriteLine($"Номер автомобиля: {car_number}");
            Console.WriteLine($"Объем топлива в баке: {Math.Round(fuel, 2)} л");
            Console.WriteLine($"Расход топлива на 100 км: {consumption} л");
            Console.WriteLine($"Расстояние дороги: {Math.Round(distance, 2)} км");
            Console.WriteLine($"Скорость автомобиля: {Math.Round(speed, 2)} км/ч");
            Console.WriteLine($"Время в дороге: {Math.Round(time, 2)} ч");
            Console.WriteLine($"Текущий пробег автомобиля: {Math.Round(mileage, 2)} км");
            Console.WriteLine($"Текущая координата по оси X: {Math.Round(x, 2)}");
            Console.WriteLine("Нажмите Enter, чтобы перейти дальше");
            Console.ReadKey();
        }

        public void Move() // езда
        {
            Console.Clear();
            while (distance_traveled < distance) // цикл, пока пройденный путь не привысит введенное расстояние
            {
                float km_current_fuel = ((fuel / consumption)) * 100; // расстояние, которое автомобиль проедет за текущий объем топлива
                float missing_fuel = ((distance - distance_traveled) * consumption) / 100 - fuel; // недостающее количество топлива для поездки
                float remaining_fuel = (distance * consumption) / 100; // топливо, отставшееся после поездки               
                if (km_current_fuel >= distance - distance_traveled) // если топлива на всю дорогу
                {
                    if (distance_traveled == 0) // если автомобиль доехал до пункта назначения без дозаправки
                    {
                        Time(distance); // обновление времени
                    }
                    else // если автомобиль хотя бы раз бывал на заправке на этом пути
                    {
                        Time(distance - distance_traveled);
                    }
                    distance_traveled += distance - distance_traveled; // обновление пройденного расстояния
                    Mileage(distance_traveled); // обновление пробега
                    Coordinates(distance_traveled); // обновление координаты
                    fuel = remaining_fuel; // обновление количества топлива в баке после поездки
                    if (fuel > 60) { fuel  = 60; } // если остаток топлива в баке окажется больше объема бака, то по умолчанию устанавливается полный бак - 60 л

                    Console.WriteLine($"Вы доехали до пункта назначения, проехав {Math.Round(distance, 2)} км со скоростью {Math.Round(speed, 2)} км/ч.\nТекущий пробег - {Math.Round(mileage, 2)} км, координата по оси X - {Math.Round(x, 2)}.");
                    Console.WriteLine("Нажмите Enter, чтобы перейти дальше");
                    Console.ReadKey();
                    Console.Clear();
                }
                else // если топлива НЕ хватило на всю дорогу
                {
                    Time(km_current_fuel);
                    distance_traveled += km_current_fuel; // к пройденному расстоянию прибавляем возможное за это количество топлива
                    fuel = 0; // обнуляем объем топлива
                    Mileage(distance_traveled); // обновление пробега
                    Coordinates(distance_traveled); // обновление координаты X

                    Console.WriteLine($"Вы проехали {Math.Round(distance_traveled, 2)} км со скоростью {Math.Round(speed, 2)} км/ч.\nТекущий пробег - {Math.Round(mileage, 2)} км, координата по оси X - {Math.Round(x, 2)}.");
                    Console.WriteLine("\nОднако, топлива не хватило, чтобы проехать весь путь. Желаете ли вы заправиться и продолжить путь?");
                    Console.WriteLine("1. Заправиться и продолжить путь\n2. Не заправляться и бросить автомобиль на пол пути");
                    Console.Write("Введите номер желаемого варианта действий: ");
                    int userChoice = Convert.ToInt32(Console.ReadLine());
                    Console.Clear();
                    if (userChoice == 1)
                    {
                        RemainingFuel(missing_fuel); // заправка автомобиля
                        Console.WriteLine("После заправки вы можете изменить свою скорость\n1. Ускориться\n2. Замедлиться\n3. Не менять скорость");
                        Console.Write("Введите номер желаемого варианта действий: ");
                        int userChoice1 = Convert.ToInt32(Console.ReadLine());
                        if (userChoice1 == 1)
                        {
                            Acceleration(); // ускорение автомобиля
                            Console.WriteLine($"\nВаша скорость увеличилась до {Math.Round(speed, 2)} км/ч.");
                            Console.WriteLine("Нажмите Enter, чтобы перейти дальше");
                            Console.ReadKey();
                            Console.Clear();
                        }
                        else if (userChoice1 == 2)
                        {
                            Braking(); // замедление автомобиля
                            Console.WriteLine($"\nВаша скорость снизилась до {Math.Round(speed, 2)} км/ч.");
                            Console.WriteLine("Нажмите Enter, чтобы перейти дальше");
                            Console.ReadKey();
                            Console.Clear();
                        }
                        else
                        {
                            Console.WriteLine($"\nВаша скорость не изменилась - {Math.Round(speed, 2)} км/ч.");
                            Console.WriteLine("Нажмите Enter, чтобы перейти дальше");
                            Console.ReadKey();
                            Console.Clear();
                        }                       
                    }
                    else
                    {
                        distance = distance_traveled;
                        Console.WriteLine("Вы приняли непростое решение оставить свой путь. Домой возвращаться придется пешком...");
                        Console.WriteLine("Нажмите Enter, чтобы перейти дальше");
                        Console.ReadKey();
                        break;
                    }
                }
            }
        }

        private void RemainingFuel(float missing_fuel) // остаток топлива в баке
        {
            Console.WriteLine($"В вашем баке сейчас {Math.Round(fuel, 2)} л топлива. Чтобы проехать оставшиеся {Math.Round(distance - distance_traveled, 2)} км, вам нужно залить {Math.Round(missing_fuel, 2)} л топлива в бак.");
            Refilling();
        }

        private void Refilling() // заправка автомобиля
        {
            Console.Write("Введите количество топлива, которое вы хотите залить: ");
            float fuel_to_be_filled = Convert.ToInt32(Console.ReadLine()); // количество топлива, которое нужно залить    
            if (fuel + fuel_to_be_filled <= 0 || fuel + fuel_to_be_filled > 60) { fuel = Convert.ToSingle(CheckingFuel(fuel)); }
            fuel += fuel_to_be_filled;
            Console.WriteLine($"Теперь в вашем баке {Math.Round(fuel, 2)} л топлива.\n");
        }

        private void Mileage(float distance) // общий пробег с учетом пройденного расстояния
        {
            mileage += distance; // расчёт общего пробега с учетом пройденного расстояния
        }

        private void Coordinates(float distance) // расчет координаты X
        {
            x += distance;
        }

        private void Time(float distance_traveled) // рассчет времени, проведенного в дороге
        {
            time += distance_traveled / speed;
        }

        private void Acceleration() // ускорение автомобиля
        {
            speed += rnd.Next(5, 20); // рандомное прибавление к скорости
        }

        private void Braking() // замедление автомобиля
        {
            speed -= rnd.Next(5, 20); // рандомное убавление от скорости
        }

        public void Accidents() // аварии
        {
            float i = rnd.Next(1, 3);
            float j = rnd.Next(1, 3);
            if (i != j) 
            {
                Console.WriteLine($"Автомобили {i} и {j} попали в аварию!");
            }
            else 
            {
                Console.WriteLine("Сегодня на дорогах никаких аварий не было!");
            }
        }
    }
}