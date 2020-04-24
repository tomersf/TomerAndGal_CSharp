using System;
using System.Collections.Generic;
using System.Text;

namespace B20_Ex01_3
{
    public class Program
    {
        public static void Main()
        {
            uint highOfTheMachine = GetHighFromTheUser();
            StringBuilder sandMachineBuilder = new StringBuilder();
            B20_Ex01_2.Program.BuildSandMachine(ref sandMachineBuilder, highOfTheMachine, 0);
            Console.Write(sandMachineBuilder);
        }

        public static uint GetHighFromTheUser()
        {
            Console.WriteLine("Enter the high of the sand machine:");
            string machineHigh = Console.ReadLine();
            while (!IsValidInput(machineHigh))
            {
                Console.WriteLine("Invalid input,please enter again:");
                machineHigh = Console.ReadLine();
            }

            uint highInInt = uint.Parse(machineHigh);
            if (highInInt % 2 == 0)
            {
                highInInt += 1;
            }

            return highInInt;
        }

        public static bool IsValidInput(string i_MachineHigh)
        {
            bool validHigh = true;
            int highInInt;
            if (int.TryParse(i_MachineHigh, out highInInt) == false || (highInInt < 0))
            {
                validHigh = false;
            }

            return validHigh;
        }
    }
}