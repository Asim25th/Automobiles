namespace Avtos
{
    class Program
    {
        static void Main(string[] args)
        {
            Avto[] cars = new Avto[3]; // объявление и создание объекта "автомобиль"
            for (int i = 0; i < cars.Length; i++)
            {
                cars[i] = new Avto();
                cars[i].Info();
                cars[i].Move();
                Console.Clear();
            }
            for (int i = 0; i < cars.Length; i++) // вывод информации о каждом автомобиле в конце программы
            {
                Console.WriteLine($"Автомобиль {i + 1}");
                cars[i].Output();
                Console.WriteLine();
            }
            cars[0].Accidents();
            Console.ReadKey();
        }
    }
}