using System;
using System.Collections.Generic;
using System.Text;

namespace B20_Ex02
{
    public class Program
    {
        public static void Main()
        {
            List<char> inputArray;
            GamePlay<char>.InitializeDefaultDataArray(out inputArray);
            Ui<char> memoryGame = new Ui<char>(inputArray);
            memoryGame.StartGame();
        }
    }
}