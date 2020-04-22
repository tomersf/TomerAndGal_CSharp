using System;
using System.Text;

namespace B20_Ex01_2
{
    public class Program
    {
        public static void Main()
        {
            StringBuilder sandMachineBuilder = new StringBuilder();
            BuildSandMachine(sandMachineBuilder, 5, 0);
            Console.Write(sandMachineBuilder);
        }

        public static void BuildSandMachine(StringBuilder i_SandMachineBuilder, uint i_NumOfAsterisks, uint i_NumberOfSpaces)
        {
            if (i_NumOfAsterisks == 1)
            {
                string rowOfOneAsterisks = BuildStringOfOneRow(i_NumOfAsterisks, i_NumberOfSpaces);
                i_SandMachineBuilder.AppendLine(rowOfOneAsterisks);
            }
            else
            {
                i_SandMachineBuilder.AppendLine(BuildStringOfOneRow(i_NumOfAsterisks, i_NumberOfSpaces));
                BuildSandMachine(i_SandMachineBuilder, i_NumOfAsterisks - 2, i_NumberOfSpaces + 1);
                string oneRow = BuildStringOfOneRow(i_NumOfAsterisks, i_NumberOfSpaces);
                i_SandMachineBuilder.AppendLine(oneRow);
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