using System;
using System.Text;

namespace B20_Ex01_4
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

            while(!isStringValid)
            {
                System.Console.WriteLine("Bad input! try again");
                theString = getStringFromUser();
                isStringValid = CheckIfStringIsValid(theString);
            }

            CheckIfStringIsPalindrome(theString);
            bool isStringAnInteger = CheckIfStringIsAnInteger(theString);
            if(isStringAnInteger)
            {
                checkRemainderByFiveOfString(theString);
            }
            else if(CheckIfStringIsOnlyWithLetters(theString) == true)
            {
                int howManyUpperCaseLettersInString = 0;
                howManyUpperCaseLettersInString = CountUpperCaseLettersInAString(theString);
                printHowManyUpperCaseLettersInTheString(howManyUpperCaseLettersInString, theString);
            }
        }

        private static string getStringFromUser()
        {
            System.Console.WriteLine("Please enter an 8 character string, and then press enter:");
            return System.Console.ReadLine();
        }

        public static bool CheckIfStringIsValid(string i_TheString)
        {
            int lengthOfString = i_TheString.Length;
            eKindOfTheChar kindOfCurrentChar = eKindOfTheChar.IsNotDigitOrLetter;
            eKindOfTheChar kindOfNextChar = eKindOfTheChar.IsNotDigitOrLetter;
            bool theStringHasValidLettersAndLength = true;
            if(lengthOfString != 8)
            {
                theStringHasValidLettersAndLength = false;
            }
            else
            {
                StringBuilder stringByIndex = new StringBuilder();
                for (int i = 0; i < lengthOfString && theStringHasValidLettersAndLength; i++)
                {
                    stringByIndex.Insert(0, i_TheString[i]);
                    bool isCharByIndexIsLetter = char.IsLetter(stringByIndex.ToString(), 0);
                    bool isCharByIndexIsDigit = char.IsDigit(stringByIndex.ToString(), 0);

                    if ((isCharByIndexIsLetter == false) && (isCharByIndexIsDigit == false))
                    {
                        kindOfCurrentChar = eKindOfTheChar.IsNotDigitOrLetter;
                    }
                    else
                    {
                        if (isCharByIndexIsDigit == true)
                        {
                            kindOfCurrentChar = eKindOfTheChar.IsDigit;
                        }

                        if (isCharByIndexIsLetter == true)
                        {
                            kindOfCurrentChar = eKindOfTheChar.IsLetter;
                        }
                    }
                    
                    stringByIndex.Remove(0, 1); // Reset the string
                    if (kindOfCurrentChar == eKindOfTheChar.IsNotDigitOrLetter || (i > 0 && kindOfCurrentChar != kindOfNextChar))
                    {
                        theStringHasValidLettersAndLength = false;
                    }

                    kindOfNextChar = kindOfCurrentChar;
                }
            }

            return theStringHasValidLettersAndLength;
        }

        public enum eKindOfTheChar
        {
          IsDigit, IsLetter, IsNotDigitOrLetter  
        }

        public static void CheckIfStringIsPalindrome(string i_TheString)
        {
            bool isTheStringPalindrome = true;
            const int k_LeftIndex = 0;
            const int k_RightIndex = 7;
            StringBuilder theString = new StringBuilder(i_TheString);

            isTheStringPalindrome = IsStringPalindromeRec(theString, k_LeftIndex, k_RightIndex, isTheStringPalindrome);
             string msg = string.Format(isTheStringPalindrome == true ? "The string {0} is palindrome." : "The string {0} is not palindrome.", theString);

            Console.WriteLine(msg);
        }

        public static bool IsStringPalindromeRec(StringBuilder i_TheString, int i_LengthIndex, int i_RightIndex, bool i_StringIsPalindrome)
        {
            if (i_LengthIndex < i_RightIndex)
            {
                if (i_TheString[i_LengthIndex] != i_TheString[i_RightIndex])
                {
                    i_StringIsPalindrome = false;
                }

                if (i_StringIsPalindrome == true)
                {
                    i_StringIsPalindrome = IsStringPalindromeRec(i_TheString, i_LengthIndex + 1, i_RightIndex - 1, true);
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
            bool remainderIsZero = true;
            int remainderByFiveOfString = int.Parse(i_TheString);
            remainderByFiveOfString %= 5;

            if (remainderByFiveOfString != 0)
            {
                remainderIsZero = false;
            }

            string msg = string.Format(remainderIsZero ? "The remainder of string value {0} divided by 5 is zero." : "The remainder of string value {0} divided by 5 is not zero.", i_TheString);

            Console.WriteLine(msg);
        }

        public static bool CheckIfStringIsOnlyWithLetters(string i_TheString)
        {
            int stringLength = i_TheString.Length;
            bool stringHaveOnlyLetters = true;

            for (int i = 0; i < stringLength; i++)
            {
                bool isCharByIndexIsALetter = char.IsLetter(i_TheString, i);
                if (isCharByIndexIsALetter)
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
            for (int i = 0; i < stringLength; i++)
            {
                bool isCharUpperCase = char.IsUpper(i_TheString, i);
                if (isCharUpperCase)
                {
                    countHowManyUpperCaseLetter++;
                }
            }

            return countHowManyUpperCaseLetter;
        }

        private static void printHowManyUpperCaseLettersInTheString(int i_HowManyUpperCaseLettersInString, string i_TheString)
        {
            string msg = string.Format(
                "there are {0} uppercase letters in the string {1}.",
                   i_HowManyUpperCaseLettersInString,
                   i_TheString);
           Console.WriteLine(msg);
        }
    }
}