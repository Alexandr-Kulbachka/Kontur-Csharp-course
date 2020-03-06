using System;

namespace Percent
{
    class Program
    {
        //using System.Globalization Такое подключение библиотеки вызывает ошибку:
        //Не нужно писать весь исходный файл целиком — пишите только метод / класс, который необходим в //задаче.
        public static double Calculate(string inputData)
        {
            var inputParams = inputData.Split(' ');
            var amountOfMoney = double.Parse(inputParams[0], System.Globalization.CultureInfo.InvariantCulture);
            var interestRate = double.Parse(inputParams[1], System.Globalization.CultureInfo.InvariantCulture);
            var amountOfMonth = int.Parse(inputParams[2]);
            var persentOfMonth = interestRate / 1200;
            return amountOfMoney * Math.Pow((1 + persentOfMonth), amountOfMonth);
        }

        static void Main(string[] args)
        {
            Console.WriteLine(Calculate("100.00 12 1"));
        }
    }
}
