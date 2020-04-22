using System;
using System.Text;

namespace B20_Ex01_5
{
    public class Program
    {
        public static void Main()
        {
            InitiateProgram();
        }

        public static void InitiateProgram()
        {
            StringBuilder inputStringFromUser = new StringBuilder();
            inputStringFromUser.Append(GetStringFromUser());
            ValidateTheNumberFromUser(inputStringFromUser);
            printStatisticsOfTheNumber(inputStringFromUser.ToString());
        }

        public static string GetStringFromUser()
        {
            System.Console.WriteLine("Please enter an 9 digit positive number");
            return System.Console.ReadLine();
        }

        public static void ValidateTheNumberFromUser(StringBuilder i_NumberFromUser)
        {
            int theNumberFromUser;
            bool numberIsValid = int.TryParse(i_NumberFromUser.ToString(), out theNumberFromUser);
            if(theNumberFromUser <= 0)
            {
                numberIsValid = false;
            }

            while(!numberIsValid)
            {
                System.Console.WriteLine("Bad input! please repeat the process.");
                i_NumberFromUser.Remove(0, i_NumberFromUser.Length);
                i_NumberFromUser.Append(GetStringFromUser());
                numberIsValid = int.TryParse(i_NumberFromUser.ToString(), out theNumberFromUser);
                if(theNumberFromUser <= 0)
                {
                    numberIsValid = false;
                }
            }
        }

        private static void printStatisticsOfTheNumber(string i_StringFromUser)
        {
            int howManyDigitsBiggerThenTheUnitDigit = 0;
            int howManyDigitsHaveRemainderZeroDividedByThree = 0;
            int stringLength = i_StringFromUser.Length;
            int theUnitDigitOfTheNumber = ToInt(i_StringFromUser[stringLength - 1]);
            int theHighestDigitInNumber = ToInt(i_StringFromUser[0]);
            int theLowestDigitInNumber = ToInt(i_StringFromUser[0]);

            for (int i = 0; i < stringLength; i++)
            {
                int intOfIndexInString = ToInt(i_StringFromUser[i]);
                if(intOfIndexInString % 3 == 0)
                {
                    howManyDigitsHaveRemainderZeroDividedByThree++;
                }

                if (intOfIndexInString > theUnitDigitOfTheNumber)
                {
                    howManyDigitsBiggerThenTheUnitDigit++;
                }

                if (theHighestDigitInNumber < intOfIndexInString)
                {
                    theHighestDigitInNumber = intOfIndexInString;
                }

                if(theLowestDigitInNumber > intOfIndexInString)
                {
                    theLowestDigitInNumber = intOfIndexInString;
                }
            }

            printTheStatistics(
                i_StringFromUser,
                howManyDigitsHaveRemainderZeroDividedByThree,
                howManyDigitsBiggerThenTheUnitDigit,
                theHighestDigitInNumber,
                theLowestDigitInNumber);
        }

        private static void printTheStatistics(
                                               string i_StringFromUser,
                                               int i_HowManyDigitsHaveRemainderZeroDividedByThree,
                                               int i_HowManyDigitsBiggerThenTheUnitDigit,
                                               int i_TheHighestDigitInNumber,
                                               int i_TheLowestDigitInNumber)
        { 
            string message = string.Format(
                @"The highest digit in the number {0} is {1} and the lowest digit is {2}
There are {3} digits with remainder 0 when divided by 3 in the number,
And there are {4} digits in the number that are bigger then the unit digit",
                i_StringFromUser,
                i_TheHighestDigitInNumber,
                i_TheLowestDigitInNumber,
                i_HowManyDigitsHaveRemainderZeroDividedByThree,
                i_HowManyDigitsBiggerThenTheUnitDigit);
            System.Console.WriteLine(message);
        }

        public static int ToInt(char i_Char)
        {
            return (int)(i_Char - '0');
        }
    }
}
