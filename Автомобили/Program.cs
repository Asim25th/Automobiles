namespace Avtos
{
    class Program
    {
        static void Main(string[] args)
        {
            Avto[] cars = new Avto[5]; // объявление и создание объекта "автомобиль"
            for (int i = 0; i < cars.Length; i++)
            {
                cars[i] = new Avto();
                cars[i].info();
                Console.Clear();
                cars[i].output();
                cars[i].move();
                Console.Clear();
            }

            for (int i = 0; i < cars.Length; i++) // вывод информации о каждом автомобиле в конце программы
            {
                Console.WriteLine($"Автомобиль {i + 1}");
                cars[i].output();
                Console.WriteLine();
            }

            cars[0].accidents();
            Console.ReadKey();
        }
    }
}