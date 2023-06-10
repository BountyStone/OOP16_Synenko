using System;
using System.Collections.Generic;
using System.Numerics;

namespace LongNumberArray
{
   
    class LongNumberArray
    {
        private List<BigInteger> numbers;

        public LongNumberArray()
        {
            numbers = new List<BigInteger>();
        }

        public LongNumberArray(List<BigInteger> initialNumbers)
        {
            numbers = initialNumbers;
        }

        public void AddNumber(BigInteger number)
        {
            numbers.Add(number);
        }

        public void SortDescending()
        {
            numbers.Sort();
            numbers.Reverse();
        }

        public void PrintNumbers()
        {
            foreach (BigInteger number in numbers)
            {
                Console.WriteLine(number);
            }
        }

        public void Add(LongNumberArray otherArray)
        {
            int maxLength = Math.Max(numbers.Count, otherArray.GetCount());

            for (int i = 0; i < maxLength; i++)
            {
                BigInteger sum = GetNumberAt(i) + otherArray.GetNumberAt(i);
                SetNumberAt(i, sum);
            }
        }

        public void Subtract(LongNumberArray otherArray)
        {
            int maxLength = Math.Max(numbers.Count, otherArray.GetCount());

            for (int i = 0; i < maxLength; i++)
            {
                BigInteger difference = GetNumberAt(i) - otherArray.GetNumberAt(i);
                SetNumberAt(i, difference);
            }
        }

        public void Multiply(LongNumberArray otherArray)
        {
            int newLength = numbers.Count + otherArray.GetCount();
            List<BigInteger> result = new List<BigInteger>(new BigInteger[newLength]);

            for (int i = 0; i < numbers.Count; i++)
            {
                for (int j = 0; j < otherArray.GetCount(); j++)
                {
                    int newIndex = i + j;
                    result[newIndex] += GetNumberAt(i) * otherArray.GetNumberAt(j);
                }
            }

            numbers = result;
        }

        public void Divide(BigInteger divisor, out LongNumberArray quotient, out LongNumberArray remainder)
        {
            quotient = new LongNumberArray();
            remainder = new LongNumberArray();

            for (int i = numbers.Count - 1; i >= 0; i--)
            {
                BigInteger partialQuotient = GetNumberAt(i) / divisor;
                BigInteger partialRemainder = GetNumberAt(i) % divisor;

                quotient.AddNumber(partialQuotient);

                if (i > 0)
                {
                    BigInteger carryOver = partialRemainder * BigInteger.Pow(10, GetNumberAt(i - 1).ToString().Length);
                    SetNumberAt(i - 1, GetNumberAt(i - 1) + carryOver);
                }

                remainder.AddNumber(partialRemainder);
            }

            quotient.SortDescending();
        }

        public bool IsEqual(LongNumberArray otherArray)
        {
            if (numbers.Count != otherArray.GetCount())
                return false;

            for (int i = 0; i < numbers.Count; i++)
            {
                if (GetNumberAt(i) != otherArray.GetNumberAt(i))
                    return false;
            }

            return true;
        }

        public bool IsNotEqual(LongNumberArray otherArray)
        {
            return !IsEqual(otherArray);
        }

        public bool IsGreaterThanOrEqual(LongNumberArray otherArray)
        {
            if (numbers.Count > otherArray.GetCount())
                return true;
            if (numbers.Count < otherArray.GetCount())
                return false;

            for (int i = numbers.Count - 1; i >= 0; i--)
            {
                if (GetNumberAt(i) > otherArray.GetNumberAt(i))
                    return true;
                if (GetNumberAt(i) < otherArray.GetNumberAt(i))
                    return false;
            }

            return true;
        }

        public bool IsLessThanOrEqual(LongNumberArray otherArray)
        {
            return !IsGreaterThanOrEqual(otherArray);
        }

        private int GetCount()
        {
            return numbers.Count;
        }

        private BigInteger GetNumberAt(int index)
        {
            if (index < numbers.Count)
                return numbers[index];

            return 0;
        }

        private void SetNumberAt(int index, BigInteger value)
        {
            if (index < numbers.Count)
                numbers[index] = value;
            else
                numbers.Add(value);
        }
    }

    class Program
    {

        static void Main(string[] args)
        {
         
            LongNumberArray longNumbers = new LongNumberArray();
            longNumbers.AddNumber(BigInteger.Parse("12345678901234567890"));
            longNumbers.AddNumber(BigInteger.Parse("98765432109876543210"));
            longNumbers.AddNumber(BigInteger.Parse("54321098765432109876"));

            Console.WriteLine("Вихiднi числа:");
            longNumbers.PrintNumbers();

            longNumbers.SortDescending();

            Console.WriteLine("\nЧисла в порядку спадання:");
            longNumbers.PrintNumbers();

            LongNumberArray otherNumbers = new LongNumberArray();
            otherNumbers.AddNumber(BigInteger.Parse("11111111111111111111"));
            otherNumbers.AddNumber(BigInteger.Parse("22222222222222222222"));
            otherNumbers.AddNumber(BigInteger.Parse("33333333333333333333"));

            longNumbers.Add(otherNumbers);

            Console.WriteLine("\nПiсля додавання iнших чисел:");
            longNumbers.PrintNumbers();

            longNumbers.Subtract(otherNumbers);

            Console.WriteLine("\nПiсля вiднiмання iнших чисел:");
            longNumbers.PrintNumbers();

            longNumbers.Multiply(otherNumbers);

            Console.WriteLine("\nПiсля множення на iнших Чисел:");
            longNumbers.PrintNumbers();

            LongNumberArray quotient, remainder;
            longNumbers.Divide(BigInteger.Parse("12345"), out quotient, out remainder);

            Console.WriteLine("\nКоефiцiєнт:");
            quotient.PrintNumbers();

            Console.WriteLine("\nЗалишок:");
            remainder.PrintNumbers();

            LongNumberArray equalNumbers = new LongNumberArray();
            equalNumbers.AddNumber(BigInteger.Parse("123456789"));

            LongNumberArray notEqualNumbers = new LongNumberArray();
            notEqualNumbers.AddNumber(BigInteger.Parse("987654321"));

            Console.WriteLine("\nЧи equalNumbers дорiвнює longNumbers? " + longNumbers.IsEqual(equalNumbers));
            Console.WriteLine("Чи notEqualNumbers дорiвнює longNumbers? " + longNumbers.IsEqual(notEqualNumbers));

            Console.WriteLine("\nЧи longNumbers бiльше чи дорiвнює equalNumbers? " + longNumbers.IsGreaterThanOrEqual(equalNumbers));
            Console.WriteLine("Чи longNumbers менше або дорiвнює to equalNumbers? " + longNumbers.IsLessThanOrEqual(equalNumbers));

            Console.ReadLine();
        }
    }
}
