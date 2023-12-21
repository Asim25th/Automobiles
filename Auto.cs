namespace Automobiles
{
    class Auto
    {
        private string carNumber; // номер автомобиля
        private float fuel; // количество бензина в баке автомобиля
        private float consumption; // расход топлива на 100 км
        private float distance; // пройденное расстояние
        private float speed; // скорость автомобиля
        private float mileage; // пробег автомобиля
        private float x = 0; // координата X
        private float time; // время в пути
        private float distanceTraveled = 0; // пройденное расстояние
        private float kmCurrentFuel; // расстояние, которое автомобиль проедет за текущий объем топлива
        private float missingFuel; // недостающее количество топлива для поездки
        private float remainingFuel; // топливо, отставшееся после поездки          
        private Random rnd = new Random();

        public Auto(string carNumber, float fuel, float consumption, float mileage, float distance, float speed)
        {
            this.carNumber = carNumber;
            this.fuel = fuel;
            this.consumption = consumption;
            this.mileage = mileage;
            this.distance = distance;
            this.speed = speed;
        }

        private void CarChioice(Auto[] cars) // вывод списка автомобилей для выбора
        {
            Console.WriteLine("ДОСТУПНЫЕ АВТОМОБИЛИ\n");
            for (int i = 0; i < cars.Length; i++)
            {
                Console.WriteLine($"{i + 1}. Автомобиль {cars[i].carNumber}");
            }
            Console.WriteLine("\nЧтобы выйти из программы введите 0\n");
        }

        public void Output(Auto[] cars, int i) // вывод информации об автомобиле на экран
        {
            Console.WriteLine($"Номер автомобиля: {cars[i].carNumber}");
            Console.WriteLine($"Объем топлива в баке: {Math.Round(cars[i].fuel, 2)} л");
            Console.WriteLine($"Расход топлива на 100 км: {cars[i].consumption} л");
            Console.WriteLine($"Расстояние дороги: {Math.Round(cars[i].distance, 2)} км");
            Console.WriteLine($"Скорость автомобиля: {Math.Round(cars[i].speed, 2)} км/ч");
            Console.WriteLine($"Время в дороге: {Math.Round(cars[i].time, 2)} ч");
            Console.WriteLine($"Текущий пробег автомобиля: {Math.Round(cars[i].mileage, 2)} км");
            Console.WriteLine($"Текущая координата по оси X: {Math.Round(cars[i].x, 2)}");
            Console.WriteLine("Нажмите Enter, чтобы перейти дальше");
            Console.ReadKey();
        }

        public void Menu(Auto[] cars)
        {
            while (true)
            {
                Console.Clear();
                CarChioice(cars);
                int choice;
                do {
                    Console.Write("Введите порядковый номер выбранного автомобиля: ");
                    choice = Convert.ToInt32(Console.ReadLine()); // выбор автомобиля для езды
                } while (choice < 0 || choice > 3);

                if (choice == 0) // выход из программы
                {
                    System.Environment.Exit(0);
                }
                else
                {
                    int i = choice - 1;
                    Console.Clear();
                    Output(cars, i); // вывод информации о выбранном автомобиле

                    if (cars[i].x != 0 && cars[i].distanceTraveled < cars[i].distance) // если был выбран автомобиль, который не проехал весь свой путь до конца
                    {
                        Console.WriteLine($"\nВы проехали {cars[i].distanceTraveled} из {cars[i].distance} км вашего маршрута. Желаете ли вы продолжить путь?");
                        Console.WriteLine("1. Да, продолжить путь\n2. Нет, вернуться в меню выбора автомобилей");
                        Console.Write("Введите номер вашего выбора: ");
                        int user_choice = Convert.ToInt32(Console.ReadLine());
                        switch (user_choice)
                        {
                            case 1:
                                //Console.Clear();
                                RemainingFuel(cars, i, cars[i].missingFuel);
                                Console.WriteLine();
                                Move(cars, i);
                                break;
                            case 2:
                                Menu(cars);
                                break;
                        }
                    }

                    else if (cars[i].distanceTraveled == cars[i].distance) // если автомобиль уже проехал все расстояние
                    {
                        Console.WriteLine("\nВы проехали все расстояние вашего маршрута. Желаете ли вы ехать дальше?");
                        Console.WriteLine("1. Да, продолжить путь\n2. Нет, вернуться в меню выбора автомобилей");
                        Console.Write("Введите номер вашего выбора: ");
                        int userChoice = Convert.ToInt32(Console.ReadLine());
                        switch (userChoice)
                        {
                            case 1:
                                cars[i].distance = rnd.Next(1, 100); // генерация нового расстояния
                                cars[i].distanceTraveled = 0;
                                Console.Clear();
                                Output(cars, i);
                                Console.WriteLine();
                                Move(cars, i);
                                break;
                            case 2:
                                Menu(cars);
                                break;
                        }
                    }

                    else
                    {
                        Console.WriteLine();
                        Move(cars, i); // если машина едет впервые
                    }
                }
            }
        }

        public void Move(Auto[] cars, int i) // езда
        {
            //Console.Clear();
            while (cars[i].distanceTraveled < cars[i].distance) // цикл, пока пройденный путь не привысит введенное расстояние
            {
                cars[i].kmCurrentFuel = cars[i].fuel / cars[i].consumption * 100; // расстояние, которое автомобиль проедет за текущий объем топлива
                cars[i].missingFuel = (cars[i].distance - cars[i].distanceTraveled) * cars[i].consumption / 100 - cars[i].fuel; // недостающее количество топлива для поездки
                cars[i].remainingFuel = cars[i].distance * cars[i].consumption / 100; // топливо, отставшееся после поездки               
                if (cars[i].kmCurrentFuel >= cars[i].distance - cars[i].distanceTraveled) // если расстояние, которое автомобиль проедет за текущий объем топлива, больше или равно оставшемуся пути
                {
                    if (cars[i].distanceTraveled == 0) // если автомобиль доехал до пункта назначения без дозаправки
                    {
                        Time(cars, i, cars[i].distance); // обновление времени
                    }
                    else // если автомобиль хотя бы раз бывал на заправке на этом пути
                    {
                        Time(cars, i, cars[i].distance - cars[i].distanceTraveled);
                    }                   
                    Mileage(cars, i, cars[i].distance - cars[i].distanceTraveled); // обновление пробега
                    Coordinates(cars, i, cars[i].distance - cars[i].distanceTraveled); // обновление координаты
                    cars[i].distanceTraveled += cars[i].distance - cars[i].distanceTraveled; // обновление пройденного расстояния
                    cars[i].fuel = cars[i].remainingFuel; // обновление количества топлива в баке после поездки
                    if (cars[i].fuel > 60) { cars[i].fuel  = 60; } // если остаток топлива в баке окажется больше объема бака, то по умолчанию устанавливается полный бак - 60 л

                    Console.WriteLine($"Вы доехали до пункта назначения, проехав {Math.Round(cars[i].distance, 2)} км со скоростью {Math.Round(cars[i].speed, 2)} км/ч.\nТекущий пробег - {Math.Round(cars[i].mileage, 2)} км, координата по оси X - {Math.Round(cars[i].x, 2)}.");
                    Console.WriteLine("Нажмите Enter, чтобы перейти дальше");
                    Console.ReadKey();
                    //Console.Clear();
                }
                else // если топлива НЕ хватило на всю дорогу
                {
                    Time(cars, i, cars[i].kmCurrentFuel);
                    Mileage(cars, i, cars[i].kmCurrentFuel); // обновление пробега
                    Coordinates(cars, i, cars[i].kmCurrentFuel); // обновление координаты X
                    cars[i].distanceTraveled += cars[i].kmCurrentFuel; // к пройденному расстоянию прибавляем возможное за это количество топлива
                    cars[i].fuel = 0; // обнуляем объем топлива

                    Console.WriteLine($"Вы проехали {Math.Round(cars[i].distanceTraveled, 2)} км со скоростью {Math.Round(cars[i].speed, 2)} км/ч.\nТекущий пробег - {Math.Round(cars[i].mileage, 2)} км, координата по оси X - {Math.Round(cars[i].x, 2)}.");
                    Console.WriteLine("\nОднако, топлива не хватило, чтобы проехать весь путь. Желаете ли вы заправиться и продолжить путь?");
                    Console.WriteLine("1. Заправиться и продолжить путь\n2. Не заправляться и бросить автомобиль на пол пути");
                    Console.Write("Введите номер желаемого варианта действий: ");
                    int userChoice = Convert.ToInt32(Console.ReadLine());
                    //Console.Clear();
                    if (userChoice == 1)
                    {
                        RemainingFuel(cars, i, cars[i].missingFuel); // заправка автомобиля
                        Console.WriteLine("\nПосле заправки вы можете изменить свою скорость\n1. Ускориться\n2. Замедлиться\n3. Не менять скорость");
                        Console.Write("Введите номер желаемого варианта действий: ");
                        int userChoice1 = Convert.ToInt32(Console.ReadLine());
                        if (userChoice1 == 1)
                        {
                            Acceleration(cars, i); // ускорение автомобиля
                            Console.WriteLine($"\nВаша скорость увеличилась до {Math.Round(cars[i].speed, 2)} км/ч.");
                            Console.WriteLine("Нажмите Enter, чтобы перейти дальше\n");
                            Console.ReadKey();
                            //Console.Clear();
                        }
                        else if (userChoice1 == 2)
                        {
                            Braking(cars, i); // замедление автомобиля
                            Console.WriteLine($"\nВаша скорость снизилась до {Math.Round(cars[i].speed, 2)} км/ч.");
                            Console.WriteLine("Нажмите Enter, чтобы перейти дальше\n");
                            Console.ReadKey();
                            //Console.Clear();
                        }
                        else
                        {
                            Console.WriteLine($"\nВаша скорость не изменилась - {Math.Round(cars[i].speed, 2)} км/ч.");
                            Console.WriteLine("Нажмите Enter, чтобы перейти дальше\n");
                            Console.ReadKey();
                            //Console.Clear();
                        }                       
                    }
                    else
                    {
                        Console.WriteLine("\nВы приняли непростое решение оставить свой путь. Домой возвращаться придется пешком...");
                        Console.WriteLine("Нажмите Enter, чтобы перейти дальше\n");
                        Console.ReadKey();
                        break;
                    }
                }
            }
        }

        public static double Checking(float a) // проверка на то, чтобы введенные данные были больше нуля
        {
            while (a <= 0)
            {
                Console.Write("ОШИБКА: Введенное значение должно быть больше нуля.\nПопробуйте ввести значение заново: ");
                a = float.Parse(Console.ReadLine());
            }
            return a; // возвращение измененного значения
        }

        public static double CheckingFuel(float fuel) // проверка на то, что топлива больше 0 и меньше 60 л
        {
            while (fuel <= 0 || fuel > 60)
            {
                Console.Write("ОШИБКА: Топлива в баке должно быть больше 0 и меньше 60 л.\nПопробуйте ввести значение заново: ");
                fuel = float.Parse(Console.ReadLine());
            }
            return fuel; // возвращение измененного значения
        }

        private void RemainingFuel(Auto[] cars, int i, float missingFuel) // остаток топлива в баке
        {
            Console.WriteLine($"\nВ вашем баке сейчас {Math.Round(cars[i].fuel, 2)} л топлива. Чтобы проехать оставшиеся {Math.Round(cars[i].distance - cars[i].distanceTraveled, 2)} км, вам нужно залить {Math.Round(missingFuel, 2)} л топлива в бак.");
            Refilling(cars, i);
        }

        private void Refilling(Auto[] cars, int i) // заправка автомобиля
        {
            Console.Write("Введите количество топлива, которое вы хотите залить: ");
            float fuelToBeFilled = Convert.ToInt32(Console.ReadLine()); // количество топлива, которое нужно залить    
            if (cars[i].fuel + fuelToBeFilled <= 0 || cars[i].fuel + fuelToBeFilled > 60) { cars[i].fuel = Convert.ToSingle(CheckingFuel(cars[i].fuel)); }
            cars[i].fuel += fuelToBeFilled;
            Console.WriteLine($"Теперь в вашем баке {Math.Round(cars[i].fuel, 2)} л топлива.");
        }

        private void Mileage(Auto[] cars, int i, float distance) // общий пробег с учетом пройденного расстояния
        {
            cars[i].mileage += distance; // расчёт общего пробега с учетом пройденного расстояния
        }

        private void Coordinates(Auto[] cars, int i, float distance) // расчет координаты X
        {
            cars[i].x += distance;
        }

        private void Time(Auto[] cars, int i, float distanceTraveled) // рассчет времени, проведенного в дороге
        {
            cars[i].time += distanceTraveled / cars[i].speed;
        }

        private void Acceleration(Auto[] cars, int i) // ускорение автомобиля
        {
            cars[i].speed += rnd.Next(5, 20); // рандомное прибавление к скорости
        }

        private void Braking(Auto[] cars, int i) // замедление автомобиля
        {
            cars[i].speed -= rnd.Next(5, 20); // рандомное убавление от скорости
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