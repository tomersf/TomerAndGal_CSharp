using System;
using System.Text;

namespace Ex01_04
{ 
    public class Program 
    {
        public static void Main()
        {
            InitiateProgram();
        }

        public static void InitiateProgram()
        {
            string theString = getStringFromUser();
            bool isStringValid = CheckIfStringIsValid(theString);

            while (!isStringValid)
            {
                System.Console.WriteLine("Bad input! try again");
                theString = getStringFromUser();
                isStringValid = CheckIfStringIsValid(theString);
            }

            CheckIfStringIsPalindrome(theString);
            bool isStringAnInteger = CheckIfStringIsAnInteger(theString);
            if (isStringAnInteger)
            {
                checkRemainderByFiveOfString(theString);
            }
            else
            {
                PrintStringIsNotANumber(theString);
            }

            bool stringHaveOnlyLetters = CheckIfStringIsOnlyWithLetters(theString);
            int howManyUpperCaseLettersInString = 0;

            if (stringHaveOnlyLetters)
            {
                howManyUpperCaseLettersInString = CountUpperCaseLettersInAString(theString);
            }

            printHowManyUpperCaseLettersInTheString(howManyUpperCaseLettersInString, theString);
        } 

        private static string getStringFromUser()
        {
            System.Console.WriteLine("Please enter an 8 character string, and then press enter:");
            return System.Console.ReadLine();
        }

        public static bool CheckIfStringIsValid(string i_TheString)
        {
            int lengthOfString = i_TheString.Length;
            bool validStringLength = false;
            bool stringHaveLetter = false;
            bool stringHaveDigit = false;

            if (lengthOfString == 8)
            {
                validStringLength = true;
                StringBuilder stringByIndex = new StringBuilder();

                for (int i = 0; i < lengthOfString; i++)
                {
                    stringByIndex.Insert(0, i_TheString[i]);
                    bool isCharByIndexIsLetter = char.IsLetter(stringByIndex.ToString(), 0);
                    bool isCharByIndexIsDigit = char.IsDigit(stringByIndex.ToString(), 0);

                    if(!isCharByIndexIsLetter && !isCharByIndexIsDigit)
                    {
                        return false;
                    }

                    if(isCharByIndexIsDigit)
                    {
                        stringHaveDigit = true;
                    }

                    if(isCharByIndexIsLetter)
                    {
                        stringHaveLetter = true;
                    }
                    
                    stringByIndex.Remove(0, 1); // Reset the string
                }
            }

            return (!stringHaveLetter && validStringLength) || (!stringHaveDigit && validStringLength);
        }

        public static void CheckIfStringIsPalindrome(string i_TheString)
        {
            bool isTheStringPalindrome = true;
            int leftIndex = 0;
            int rightIndex = 7;
            StringBuilder theString = new StringBuilder(i_TheString);

            isTheStringPalindrome = IsStringPalindromeRec(theString, leftIndex, rightIndex, isTheStringPalindrome);

            if(isTheStringPalindrome)
            {
                System.Console.WriteLine("The string " + theString + " is palindrome.");
            }
            else
            {
                System.Console.WriteLine("The string " + theString + " is not palindrome.");
            }
        }

        public static bool IsStringPalindromeRec(StringBuilder i_TheString, int i_LengthIndex, int i_RightIndex, bool i_StringIsPalindrome)
        {
            if(i_LengthIndex < i_RightIndex)
            {
                if(i_TheString[i_LengthIndex] != i_TheString[i_RightIndex])
                {
                    i_StringIsPalindrome = false;
                }

                if(i_StringIsPalindrome)
                {
                    i_StringIsPalindrome = IsStringPalindromeRec(i_TheString, i_LengthIndex + 1, i_RightIndex - 1, i_StringIsPalindrome);
                }
            }

            return i_StringIsPalindrome;
        }

        public static bool CheckIfStringIsAnInteger(string i_TheString)
        {
            int stringValue;
            return int.TryParse(i_TheString, out stringValue);
        }

        private static void checkRemainderByFiveOfString(string i_TheString)
        {
            bool remainderIsZero = false;
            int remainderByFiveOfString = int.Parse(i_TheString);
            remainderByFiveOfString %= 5;

            if(remainderByFiveOfString == 0)
            {
                remainderIsZero = true;
            }

            if(remainderIsZero)
            {
                System.Console.WriteLine("The remainder of string value " + i_TheString + " divided by 5 is zero.");
            }
            else
            {
                System.Console.WriteLine("The remainder of string value " + i_TheString + " divided by 5 is not zero.");
            }
        }

        public static void PrintStringIsNotANumber(string i_TheString)
        {
            System.Console.WriteLine("The string " + i_TheString + " is not a number.");
        }

        public static bool CheckIfStringIsOnlyWithLetters(string i_TheString)
        {
            int stringLength = i_TheString.Length;
            bool stringHaveOnlyLetters = true;

            for(int i = 0; i < stringLength; i++)
            {
                bool isCharByIndexIsALetter = char.IsLetter(i_TheString, i);
                if(isCharByIndexIsALetter)
                {
                    continue;
                }
                
                stringHaveOnlyLetters = false;
                break;
            }

            return stringHaveOnlyLetters;
        }

        public static int CountUpperCaseLettersInAString(string i_TheString)
        {
            int stringLength = i_TheString.Length;
            int countHowManyUpperCaseLetter = 0;
            for(int i = 0; i < stringLength; i++)
            {
                bool isCharUpperCase = char.IsUpper(i_TheString, i);
                if(isCharUpperCase)
                {
                    countHowManyUpperCaseLetter++;
                }
            }

            return countHowManyUpperCaseLetter;
        } 

        private static void printHowManyUpperCaseLettersInTheString(int i_HowManyUpperCaseLettersInString, string i_TheString)
        {
            System.Console.WriteLine("there are " + i_HowManyUpperCaseLettersInString + " uppercase letters in the string " + i_TheString + ".");
        }
    }
}
