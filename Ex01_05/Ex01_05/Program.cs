using System;
using System.Collections.Generic;
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
            printLowestDigitAndHighestDigitInTheNumber(inputStringFromUser.ToString());



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

        private static void printLowestDigitAndHighestDigitInTheNumber(string i_StringFromUser)
        {
            int highestNumber;
            
        }
    }
}
