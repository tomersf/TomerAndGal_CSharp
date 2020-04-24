using System;
using System.Text;

namespace B20_Ex01_2
{
    public class Program
    {
        public static void Main()
        {
            StringBuilder sandMachineBuilder = new StringBuilder();
            BuildSandMachine(ref sandMachineBuilder, 5, 0);
            Console.Write(sandMachineBuilder);
        }

        public static void BuildSandMachine(ref StringBuilder io_SandMachineBuilder, uint i_NumOfAsterisks, uint i_NumberOfSpaces)
        {
            if (i_NumOfAsterisks == 1)
            {
                string rowOfOneAsterisks = BuildStringOfOneRow(i_NumOfAsterisks, i_NumberOfSpaces);
                io_SandMachineBuilder.AppendLine(rowOfOneAsterisks);
            }
            else
            {
                io_SandMachineBuilder.AppendLine(BuildStringOfOneRow(i_NumOfAsterisks, i_NumberOfSpaces));
                BuildSandMachine(ref io_SandMachineBuilder, i_NumOfAsterisks - 2, i_NumberOfSpaces + 1);
                string oneRow = BuildStringOfOneRow(i_NumOfAsterisks, i_NumberOfSpaces);
                io_SandMachineBuilder.AppendLine(oneRow);
            }
        }

        public static string BuildStringOfOneRow(uint i_NumOfAsterisks, uint i_NumberOfSpaces)
        {
            StringBuilder stringBuilderOfOneRow = new StringBuilder();
            for (int i = 0; i < i_NumberOfSpaces; i++)
            {
                stringBuilderOfOneRow.Append(" ");
            }

            for (int i = 0; i < i_NumOfAsterisks; i++)
            {
                stringBuilderOfOneRow.Append("*");
            }

            return stringBuilderOfOneRow.ToString();
        }
    }
}