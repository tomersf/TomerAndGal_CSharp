using System;

namespace B20_Ex01_1
{
    public class Program
    {
        public static void Main()
        {
            Console.WriteLine("Please enter 3 binary numbers with 9 digits each");
            int firstNumber = GetNumbersFromTheUser();
            int secondNumber = GetNumbersFromTheUser();
            int thirdNumber = GetNumbersFromTheUser();
            StatisticsOfTheNumbers(firstNumber, secondNumber, thirdNumber);
        }

        public static void PrintNumbersInDecimal(int i_FirstNumber, int i_SecondNumber, int i_ThirdNumber)
        {
            string msg = string.Format(
@"Numbers in decimal:
First number: {0}
Second number: {1}
Third number: {2}
",
i_FirstNumber,
i_SecondNumber,
i_ThirdNumber);
            Console.Write(msg);
        }

        public static int GetNumbersFromTheUser()
        {
            string numberAsString = Console.ReadLine();
            ushort numberOfDigitsInTheNumber = (ushort)numberAsString.Length;
            while ((numberOfDigitsInTheNumber != 9) || (IsBinaryNumber(numberAsString, 9) == false))
            {
                Console.WriteLine("This number is invalid, please enter again");
                numberAsString = Console.ReadLine();
                numberOfDigitsInTheNumber = (ushort)numberAsString.Length;
            }

            int numberAsInt = int.Parse(numberAsString);
            return numberAsInt;
        }

        public static bool IsBinaryNumber(string i_Number, ushort i_LengthOfTheNumber)
        {
            bool thisIsBinaryNumber = true;
            int numberInInt;
            bool goodInput = int.TryParse(i_Number, out numberInInt);
            if (goodInput == false)
            {
                thisIsBinaryNumber = false;
            }

            eDecimalDigit digitToCheck = (eDecimalDigit)(numberInInt % 10);
            for (int i = 0; i < i_LengthOfTheNumber && thisIsBinaryNumber; i++)
            {
                if (digitToCheck != eDecimalDigit.Zero && digitToCheck != eDecimalDigit.One)
                {
                    thisIsBinaryNumber = false;
                }

                digitToCheck = (eDecimalDigit)(numberInInt % 10);
                numberInInt /= 10;
            }

            return thisIsBinaryNumber;
        }

        public static ushort FromBinaryToDecimal(int i_Number)
        {
            double numberInDecimal = 0;
           short currentDigit = (short)(i_Number % 10);
            for (int i = 0; i < 9; i++)
            {
                numberInDecimal = (double)numberInDecimal + (currentDigit * Math.Pow(2, i));
                i_Number /= 10;
                currentDigit = (short)(i_Number % 10);
            }

            ushort numberInInt = (ushort)numberInDecimal;
            return numberInInt;
        }

        public static void StatisticsOfTheNumbers(int i_FirstNumber, int i_SecondNumber, int i_ThirdNumber)
        {
            ushort firstNumberInDecimal = FromBinaryToDecimal(i_FirstNumber);
            ushort secondNumberInDecimal = FromBinaryToDecimal(i_SecondNumber);
            ushort thirdNumberInDecimal = FromBinaryToDecimal(i_ThirdNumber);
            PrintNumbersInDecimal(firstNumberInDecimal, secondNumberInDecimal, thirdNumberInDecimal);
            PrintAverageNumberOfBinaryDigits(i_FirstNumber, i_SecondNumber, i_ThirdNumber);
            PrintHowManyArePowOfTwo(firstNumberInDecimal, secondNumberInDecimal, thirdNumberInDecimal);
            PrintHowManyAreRisingSeries(firstNumberInDecimal, secondNumberInDecimal, thirdNumberInDecimal);
            PrintMaxAndMin(firstNumberInDecimal, secondNumberInDecimal, thirdNumberInDecimal);
        }

        public static void PrintAverageNumberOfBinaryDigits(int i_FirstNumber, int i_SecondNumber, int i_ThirdNumber)
        {
            const ushort k_NumberOfNumbers = 3;
            const ushort k_NumberOfDigitsInOneNumbers = 9;
            ushort countOfZeros = 0;
            ushort countOfOnes = 0;
            countOfZeros += CountZerosInBinaryNumber(i_FirstNumber);
            countOfZeros += CountZerosInBinaryNumber(i_SecondNumber);
            countOfZeros += CountZerosInBinaryNumber(i_ThirdNumber);
            countOfOnes = (ushort)((k_NumberOfNumbers * k_NumberOfDigitsInOneNumbers) - countOfZeros);
            double averageNumberOfZero = (double)countOfZeros / 3;
            double averageNumberOfOne = (double)countOfOnes / 3;
            string msg = string.Format(
@"The average number of zeros in number: {0}
The average number of ones in number: {1} ",
averageNumberOfZero,
averageNumberOfOne);
            Console.WriteLine(msg);
        }

        public static void PrintHowManyArePowOfTwo(ushort i_FirstNumber, ushort i_SecondNumber, ushort i_ThirdNumber)
        {
            short countOfNumbersArePowOfTwo = 0;

            if (IsPowOfTwo(i_FirstNumber) == true)
            {
                countOfNumbersArePowOfTwo++;
            }

            if (IsPowOfTwo(i_SecondNumber) == true)
            {
                countOfNumbersArePowOfTwo++;
            }

            if (IsPowOfTwo(i_ThirdNumber) == true)
            {
                countOfNumbersArePowOfTwo++;
            }

            string msg = string.Format(
            @"Number of numbers there are power of two: {0}", countOfNumbersArePowOfTwo);
            Console.WriteLine(msg);
        }

        public static ushort CountZerosInBinaryNumber(int i_BinaryNumber)
        {
            ushort counterOfZeros = 0;
            for (int i = 0; i < 9; i++)
            {
                if (i_BinaryNumber % 10 == 0)
                {
                    counterOfZeros++;
                }

                i_BinaryNumber /= 10;
            }

            return counterOfZeros;
        }

        public static bool IsPowOfTwo(double i_Number)
        {
            return Math.Log(i_Number, 2) % 1 == 0;
        }

        public static void PrintHowManyAreRisingSeries(ushort i_FirstNumber, ushort i_SecondNumber, ushort i_ThirdNumber)
        {
            short countOfNumbersAreRisingSeries = 0;
            if (IsRisingSeries(i_FirstNumber) == true)
            {
                countOfNumbersAreRisingSeries++;
            }

            if (IsRisingSeries(i_SecondNumber) == true)
            {
                countOfNumbersAreRisingSeries++;
            }

            if (IsRisingSeries(i_ThirdNumber) == true)
            {
                countOfNumbersAreRisingSeries++;
            }

            string msg = string.Format(
                @"Number of numbers whose digits are a rising series: {0}",
                countOfNumbersAreRisingSeries);
            Console.WriteLine(msg);
        }

        public static bool IsRisingSeries(ushort i_Number)
        {
            bool thisSeriesIsRising = true;
            eDecimalDigit currentDigit = (eDecimalDigit)(i_Number % 10);
            i_Number /= 10;
            eDecimalDigit nextDigitToCheck = (eDecimalDigit)(i_Number % 10);
            while (i_Number > 0 && thisSeriesIsRising)
            {
                if (nextDigitToCheck >= currentDigit)
                {
                    thisSeriesIsRising = false;
                }
                else
                {
                    currentDigit = nextDigitToCheck;
                }

                i_Number /= 10;
                 nextDigitToCheck = (eDecimalDigit)(i_Number % 10);
            }

            return thisSeriesIsRising;
        }

        public enum eDecimalDigit
        {
            Zero, One, Two, Three, Four, Five, Six, Seven, Eight, Nine
        }

        public static void PrintMaxAndMin(ushort i_FirstNumber, ushort i_SecondNumber, ushort i_ThirdNumber)
        {
            ushort maxNumber = Math.Max(Math.Max(i_FirstNumber, i_SecondNumber), i_ThirdNumber);
            ushort minNumber = Math.Min(Math.Min(i_FirstNumber, i_SecondNumber), i_ThirdNumber);
            string msg = string.Format(
@"Biggest number is: {0}
Smallest number: {1}",
maxNumber,
minNumber);
            Console.WriteLine(msg);
        }
    }
}
