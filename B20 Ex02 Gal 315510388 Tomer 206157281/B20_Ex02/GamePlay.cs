using System;
using System.Collections.Generic;
using System.Text;

namespace B20_Ex02
{
    internal class GamePlay<T>
    {
        private const int k_MaxPairs = 18;
        private readonly List<T> r_RunTimeSizedValuesArray;
        private Player m_FirstPlayer;
        private Player m_SecondPlayer;
        private Board m_Board;
        private eKindOfTheGame m_KindOfTheGame;

        public static void InitializeDefaultDataArray(out List<char> i_InputArray)
        {
            i_InputArray = new List<char>(k_MaxPairs);
            for (int i = 0; i < 18; i++)
            {
                char charToAdd = (char)('A' + i);
                i_InputArray.Add(charToAdd);
            }
        }

        public eKindOfTheGame KindOfTheGame
        {
            get
            {
                return m_KindOfTheGame;
            }

            set
            {
                m_KindOfTheGame = value;
            }
        }

        public Board TheBoard
        {
            get
            {
                return m_Board;
            }

            set
            {
                m_Board = value;
            }
        }

        public Player FirstPlayer
        {
            get
            {
                return m_FirstPlayer;
            }

            set
            {
                m_FirstPlayer = value;
            }
        }

        public Player SecondPlayer
        {
            get
            {
                return m_SecondPlayer;
            }

            set
            {
                m_SecondPlayer = value;
            }
        }

        public GamePlay(
                        string i_FistUsereName,
                        string i_SecondUserName,
                        eKindOfTheGame i_KindOfTheGame,
                        int i_Height,
                        int i_Width,
                        List<T> i_InputArray)
        {
            m_KindOfTheGame = i_KindOfTheGame;
            m_FirstPlayer = new Player(i_FistUsereName, Player.eKindOfThePlayer.User);
            m_SecondPlayer = i_KindOfTheGame == eKindOfTheGame.PlayAgainstComputer ?
                                 new Player(i_SecondUserName, Player.eKindOfThePlayer.Computer) : new Player(i_SecondUserName, Player.eKindOfThePlayer.User);
            m_Board = new Board(i_Height, i_Width);
            int halfTheboardSize = (i_Height * i_Width) / 2;
            r_RunTimeSizedValuesArray = new List<T>(halfTheboardSize);
            r_RunTimeSizedValuesArray = i_InputArray;
        }

        public enum eKindOfTheGame
        {
            PlayAgainstComputer, PlayAgainstFriend
        }

        public void PrintBoard()
        {
            Ex02.ConsoleUtils.Screen.Clear();
            int numOfColumns = m_Board.Width;
            int numOfRows = m_Board.Height;
            char initialLetterToPrint = 'A';
            string threeSpaces = "   ";
            string fourEqualSigns = "====";
            StringBuilder theBoardAsStringBuilder = new StringBuilder();

            theBoardAsStringBuilder.Append(" ");
            appendFirstRowToBoardString(ref theBoardAsStringBuilder, numOfColumns, threeSpaces, initialLetterToPrint);
            theBoardAsStringBuilder.Append(Environment.NewLine);

            theBoardAsStringBuilder.Append("  =");
            appendEqualSignRowToBoardString(ref theBoardAsStringBuilder, numOfColumns, fourEqualSigns);
            theBoardAsStringBuilder.Append(Environment.NewLine);

            appendBodyOfBoardToStringBuilder(numOfRows, ref theBoardAsStringBuilder, numOfColumns, fourEqualSigns);
            Console.WriteLine(theBoardAsStringBuilder);
        }

        private void appendBodyOfBoardToStringBuilder(int i_NumOfRows, ref StringBuilder i_TheBoardAsStringBuilder, int i_NumOfColumns, string i_FourEqualSigns)
        {
            for (int i = 0; i < i_NumOfRows; i++)
            {
                int numericIndex = i + 1;
                i_TheBoardAsStringBuilder.Append(numericIndex);
                i_TheBoardAsStringBuilder.Append(" |");
                for (int j = 0; j < i_NumOfColumns; j++)
                {
                    if (m_Board.Table[i, j].Discovered == true)
                    {
                        T theContentInCell = r_RunTimeSizedValuesArray[m_Board.Table[i, j].Value];
                        i_TheBoardAsStringBuilder.Append(" ");
                        i_TheBoardAsStringBuilder.Append(theContentInCell);
                    }
                    else
                    {
                        i_TheBoardAsStringBuilder.Append("  ");
                    }

                    i_TheBoardAsStringBuilder.Append(" |");
                }

                i_TheBoardAsStringBuilder.Append(Environment.NewLine);
                i_TheBoardAsStringBuilder.Append("  =");
                appendEqualSignRowToBoardString(ref i_TheBoardAsStringBuilder, i_NumOfColumns, i_FourEqualSigns);
                i_TheBoardAsStringBuilder.Append(Environment.NewLine);
            }
        }

        private void appendFirstRowToBoardString(
            ref StringBuilder i_TheBoard,
            int i_NumOfColumns,
            string i_ThreeSpaces,
            char i_InitialLetterToPrint)
        {
            for (int j = 0; j < i_NumOfColumns; j++)
            {
                i_TheBoard.Append(i_ThreeSpaces);
                i_TheBoard.Append(i_InitialLetterToPrint);
                i_InitialLetterToPrint++;
            }
        }

        private void appendEqualSignRowToBoardString(ref StringBuilder i_TheBoardAsStringBuilder, int i_NumOfColumns, string i_FourEqualSigns)
        {
            for (int j = 0; j < i_NumOfColumns; j++)
            {
                i_TheBoardAsStringBuilder.Append(i_FourEqualSigns);
            }
        }
    }
}