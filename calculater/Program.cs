namespace calculater
{
    internal class Program
    {
        delegate decimal EquationOperator(decimal a, decimal b);
        static void Main(string[] args)
        {
            Console.WriteLine("Вас приветствует микро-калькулятор");
            Console.WriteLine("Для выхода введите 'quit'\nПожалуйста, введите выражение");
            String? rawEquation;
            do
            {
                rawEquation = Console.ReadLine();
                if (rawEquation == "")
                {
                    Console.WriteLine("ПОЖАЛУЙСТА, ВВЕДИТЕ ВЫРАЖЕНИЕ!");
                    continue;
                }

                if (rawEquation?.ToLower() == "quit") break;

                if (rawEquation.StartsWith("sqrt"))
                {
                    Double.TryParse(rawEquation.Substring(5, rawEquation.Length-6), out double d);
                    if (d >= 0) Console.WriteLine($"Итог: {Math.Sqrt(d)}");
                    else Console.WriteLine($"Итог: {Math.Sqrt(-d)}i");
                    continue;
                }

                (bool, decimal, EquationOperator, decimal) tuple = ToTupleEquation(rawEquation);

                if(tuple.Item1 == false)
                {
                    Console.WriteLine("Введено неверное выражение!");
                    continue;
                }
                Console.WriteLine($"Итог: {tuple.Item3?.Invoke(tuple.Item2, tuple.Item4)}");
            } while (true);

        }

        private static (bool, decimal, EquationOperator, decimal) ToTupleEquation(String rawEquation)
        {
            string[] splitArray = rawEquation.Split(' ');
            (bool, decimal, EquationOperator, decimal) tuple = (false, 1.0m, (decimal a, decimal b) => a + b, 1.0m) ;
            tuple.Item1 = decimal.TryParse(splitArray[0], out tuple.Item2);
            tuple.Item1 = decimal.TryParse(splitArray[2], out tuple.Item4);
            switch (splitArray[1])
            {
                case "+":
                    tuple.Item3 = (decimal a, decimal b) => a + b;
                    break;
                case "-":
                    tuple.Item3 = (decimal a, decimal b) => a - b;
                    break;
                case "*":
                    tuple.Item3 = (decimal a, decimal b) => a * b;
                    break;
                case "/":
                    if (tuple.Item4 == 0)
                    {
                        tuple.Item1 = false;
                        Console.WriteLine("Деление на ноль запрещенно");
                        break;
                    }
                    tuple.Item3 = (decimal a, decimal b) => a / b;
                    break;
            }
            return tuple;
        }
    }
}