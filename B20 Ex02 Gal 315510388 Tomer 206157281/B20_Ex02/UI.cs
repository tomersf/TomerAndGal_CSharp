using System;
using System.Collections.Generic;
using System.Security.Policy;
using System.Text;

namespace B20_Ex02
{
    internal class Ui<T>
    {
        private const int k_MinDimensionSize = 4;
        private const int k_MaxDimensionSize = 6;
        private const char k_ExitChar = 'Q';
        private GamePlay<T> m_MemoryGame;
        private ComputerIntelligence m_ArtificialIntelligence;
        private eWhichPlayerTurn m_WhichPlayerTurn = eWhichPlayerTurn.FirstPlayerTurn;
        private List<T> m_InputArray;

        public Ui(List<T> i_InputArray)
        {
            Console.WriteLine("Welcome to the memory game!");
            getDataFromTheUser(i_InputArray);
        }

        public void StartGame()
        {
            m_MemoryGame.TheBoard.InitializeOfTheBoard();
            beginningOfTheGame();
            declarationOfTheWinner();
            Console.WriteLine("For another game press 1,else press another input");
            string userChoice = Console.ReadLine();
            while(userChoice.Length == 1 && (userChoice[0] - '0') == 1)
            {
                GetDataForAnotherPlay();
                m_MemoryGame.TheBoard.InitializeOfTheBoard();
                beginningOfTheGame();
                declarationOfTheWinner();
                Console.WriteLine("For another game press 1,else press another input");
                userChoice = Console.ReadLine();
            }

            Console.WriteLine("Game Over");
        }

        private void beginningOfTheGame()
        {
            int sizeOfTheBoard = m_MemoryGame.TheBoard.Height * m_MemoryGame.TheBoard.Width;
            if (m_MemoryGame.KindOfTheGame == GamePlay<T>.eKindOfTheGame.PlayAgainstComputer)
            {
                m_ArtificialIntelligence = new ComputerIntelligence(m_MemoryGame.TheBoard.Height, m_MemoryGame.TheBoard.Width);
            }

            int halfOfBoardSize = sizeOfTheBoard / 2;
            while (m_MemoryGame.FirstPlayer.Score + m_MemoryGame.SecondPlayer.Score < halfOfBoardSize)
            {
                m_MemoryGame.PrintBoard();
                if (m_MemoryGame.KindOfTheGame == GamePlay<T>.eKindOfTheGame.PlayAgainstFriend)
                {
                    if (m_WhichPlayerTurn == eWhichPlayerTurn.FirstPlayerTurn)
                    {
                        Console.WriteLine("First player turn");
                        oneTurnOfPlayer(m_WhichPlayerTurn);
                    }
                    else
                    {
                        Console.WriteLine("Second player turn");
                        oneTurnOfPlayer(m_WhichPlayerTurn);
                    }
                }
                else
                {
                    if (m_WhichPlayerTurn == eWhichPlayerTurn.FirstPlayerTurn)
                    {
                        Console.WriteLine("First player turn");
                        oneTurnOfPlayer(m_WhichPlayerTurn);
                    }
                    else
                    {
                        ComputerIntelligenceCell randomCellFromTheComputer = getRandomCellFromTheComputer();
                        m_MemoryGame.TheBoard.Table[randomCellFromTheComputer.Row, randomCellFromTheComputer.Col].Discovered = true;
                        m_ArtificialIntelligence.NewAppearanceOfCell(randomCellFromTheComputer.Row, randomCellFromTheComputer.Col);
                        m_MemoryGame.PrintBoard();
                        System.Threading.Thread.Sleep(2000);
                        ComputerIntelligenceCell intelligenceCellFromTheComputer =
                            m_ArtificialIntelligence.GetIntelligenceCellFromTheComputer(randomCellFromTheComputer, m_MemoryGame.TheBoard);
                        m_MemoryGame.TheBoard.Table[intelligenceCellFromTheComputer.Row, intelligenceCellFromTheComputer.Col].Discovered = true;

                        if (differentDataInCells(randomCellFromTheComputer, intelligenceCellFromTheComputer) == true
                            || randomCellFromTheComputer == intelligenceCellFromTheComputer)
                        {
                            m_MemoryGame.PrintBoard();
                            System.Threading.Thread.Sleep(2000);
                            m_MemoryGame.TheBoard.RemoveSignToCell(randomCellFromTheComputer);
                            m_MemoryGame.TheBoard.RemoveSignToCell(intelligenceCellFromTheComputer);
                            m_ArtificialIntelligence.NewAppearanceOfCell(intelligenceCellFromTheComputer.Row, intelligenceCellFromTheComputer.Col);
                            m_WhichPlayerTurn = eWhichPlayerTurn.FirstPlayerTurn;
                            m_MemoryGame.PrintBoard();
                            System.Threading.Thread.Sleep(2000);
                        }
                        else
                        {
                            m_MemoryGame.SecondPlayer.Score++;
                            System.Threading.Thread.Sleep(2000);
                            m_MemoryGame.PrintBoard();
                        }
                    }
                }
            }
        }

        private void declarationOfTheWinner()
        {
            string msg;
            if (m_MemoryGame.FirstPlayer.Score > m_MemoryGame.SecondPlayer.Score)
            {
                msg = string.Format(@"{0} is the winner!!", m_MemoryGame.FirstPlayer.Name);
                Console.Write(msg);
            }
            else if (m_MemoryGame.FirstPlayer.Score < m_MemoryGame.SecondPlayer.Score)
            {
                msg = string.Format(@"{0} is the winner!!", m_MemoryGame.SecondPlayer.Name);
                Console.Write(msg);
            }
            else
            {
                Console.WriteLine("There is no winner");
            }

            if(m_MemoryGame.KindOfTheGame == GamePlay<T>.eKindOfTheGame.PlayAgainstFriend)
            {
                msg = string.Format(
@"Points of the first player:{0}
Points of the second player: {1} 
",
m_MemoryGame.FirstPlayer.Score,
m_MemoryGame.SecondPlayer.Score);
            }
            else
            {
                {
                    msg = string.Format(
@"Points of the first player:{0}
Points of the computer: {1} 
",
m_MemoryGame.FirstPlayer.Score,
m_MemoryGame.SecondPlayer.Score);
                }
            }

            Console.Write(msg);
        }

        private void oneTurnOfPlayer(eWhichPlayerTurn i_WhichPlayerTurn)
        {
            string firstCellNumberAsString;
            string secondCellNumberAsString;
            Console.WriteLine("Enter the first cell");
            firstCellNumberAsString = getCellFromTheUser();
            m_MemoryGame.TheBoard.AddSignToCell(firstCellNumberAsString);
            if (m_MemoryGame.KindOfTheGame == GamePlay<T>.eKindOfTheGame.PlayAgainstComputer)
            {
                m_ArtificialIntelligence.NewAppearanceOfCell(firstCellNumberAsString);
            }

            m_MemoryGame.PrintBoard();
            Console.WriteLine("Enter another cell");
            secondCellNumberAsString = getCellFromTheUser();
            m_MemoryGame.TheBoard.AddSignToCell(secondCellNumberAsString);
            if (m_MemoryGame.KindOfTheGame == GamePlay<T>.eKindOfTheGame.PlayAgainstComputer)
            {
                m_ArtificialIntelligence.NewAppearanceOfCell(secondCellNumberAsString);
            }

            if (differentDataInCells(firstCellNumberAsString, secondCellNumberAsString) == true)
            {
                m_MemoryGame.PrintBoard();
                System.Threading.Thread.Sleep(2000);
                m_MemoryGame.TheBoard.RemoveSignToCell(firstCellNumberAsString);
                m_MemoryGame.TheBoard.RemoveSignToCell(secondCellNumberAsString);
                m_MemoryGame.PrintBoard();
                if (m_WhichPlayerTurn == eWhichPlayerTurn.FirstPlayerTurn)
                {
                    m_WhichPlayerTurn = eWhichPlayerTurn.SecondPlayerTurn;
                }
                else
                {
                    m_WhichPlayerTurn = eWhichPlayerTurn.FirstPlayerTurn;
                }
            }
            else
            {
                m_MemoryGame.PrintBoard();
                if (i_WhichPlayerTurn == eWhichPlayerTurn.FirstPlayerTurn)
                {
                    m_MemoryGame.FirstPlayer.Score++;
                }
                else
                {
                    m_MemoryGame.SecondPlayer.Score++;
                }
            }
        }

        private bool differentDataInCells(string i_FirstCellNumberAsString, string i_SecondCellNumberAsString)
        {
            int colAsIntOfTheFirstCell = i_FirstCellNumberAsString[0] - 'A';
            int rowAsIntOfTheFirstCell = i_FirstCellNumberAsString[1] - '0' - 1;

            int colAsIntOfTheSecondCell = i_SecondCellNumberAsString[0] - 'A';
            int rowAsIntOfTheSecondCell = i_SecondCellNumberAsString[1] - '0' - 1;
            return m_MemoryGame.TheBoard.Table[rowAsIntOfTheFirstCell, colAsIntOfTheFirstCell].Value
                   != m_MemoryGame.TheBoard.Table[rowAsIntOfTheSecondCell, colAsIntOfTheSecondCell].Value;
        }

        private bool differentDataInCells(ComputerIntelligenceCell i_FirstCell, ComputerIntelligenceCell i_SecondCell)
        {
            int colAsIntOfTheFirstCell = i_FirstCell.Col;
            int rowAsIntOfTheFirstCell = i_FirstCell.Row;

            int colAsIntOfTheSecondCell = i_SecondCell.Col;
            int rowAsIntOfTheSecondCell = i_SecondCell.Row;
            return m_MemoryGame.TheBoard.Table[rowAsIntOfTheFirstCell, colAsIntOfTheFirstCell].Value
                   != m_MemoryGame.TheBoard.Table[rowAsIntOfTheSecondCell, colAsIntOfTheSecondCell].Value;
        }

        private ComputerIntelligenceCell getRandomCellFromTheComputer()
        {
            Random randomVal = new Random();
            m_ArtificialIntelligence.RandomArray = new List<ComputerIntelligenceCell>();
            for (int i = 0; i < m_MemoryGame.TheBoard.Height; i++)
            {
                for (int j = 0; j < m_MemoryGame.TheBoard.Width; j++)
                {
                    if (m_MemoryGame.TheBoard.Table[i, j].Discovered == false)
                    {
                        ComputerIntelligenceCell newCell = new ComputerIntelligenceCell(i, j);
                        m_ArtificialIntelligence.RandomArray.Add(newCell);
                    }
                }
            }

            if (m_ArtificialIntelligence.RandomArray.Capacity == 0)
            {
                declarationOfTheWinner();
                Environment.Exit(1);
            }

            int randomValue = randomVal.Next(m_ArtificialIntelligence.RandomArray.Count);

            return m_ArtificialIntelligence.RandomArray[randomValue];
        }

        private string getCellFromTheUser()
        {
            string cellNumberAsString = Console.ReadLine();
            while (m_MemoryGame.TheBoard.CheckIfTheCellIsValid(cellNumberAsString) == false)
            {
                if (cellNumberAsString.Length == 1 && cellNumberAsString[0] == k_ExitChar)
                {
                    Environment.Exit(1);
                }

                if (cellNumberAsString.Length != 2)
                {
                    Console.WriteLine("Invalid input,you need to enter one char and after it one number,Please enter again");
                }
                else
                {
                    int colAsInt = cellNumberAsString[0] - 'A';
                    int rowAsInt = cellNumberAsString[1] - '0' - 1;
                    if (m_MemoryGame.TheBoard.IsValidCol(colAsInt) && m_MemoryGame.TheBoard.IsValidRow(rowAsInt))
                    {
                        if (m_MemoryGame.TheBoard.Table[rowAsInt, colAsInt].Discovered == true)
                        {
                            Console.WriteLine("This cell is already taken,Please enter again");
                        }
                    }
                    else
                    {
                        Console.WriteLine("There is no such cell in the board,Please enter again");
                    }
                }

                cellNumberAsString = Console.ReadLine();
            }

            return cellNumberAsString;
        }

        public enum eWhichPlayerTurn
        {
            FirstPlayerTurn, SecondPlayerTurn
        }

        public void GetDataForAnotherPlay()
        {
            int height;
            int width;
            getDimensionFromTheUser(out height, out width);
            m_MemoryGame = new GamePlay<T>(
                m_MemoryGame.FirstPlayer.Name,
                m_MemoryGame.SecondPlayer.Name,
                GamePlay<T>.eKindOfTheGame.PlayAgainstFriend,
                height,
                width,
                m_InputArray);
        }

        private void getDataFromTheUser(List<T> i_InputArray)
        {
            m_InputArray = i_InputArray;
            string firstUserName = getTheNameOfTheUser();
            GamePlay<T>.eKindOfTheGame kindOfTheGame = getKindOfTheGameFromTheUser();
            string secondUserName;
            if (kindOfTheGame == GamePlay<T>.eKindOfTheGame.PlayAgainstFriend)
            {
                secondUserName = getTheNameOfTheUser();
            }
            else
            {
                secondUserName = "Computer";
            }

            int height;
            int width;
            getDimensionFromTheUser(out height, out width);
            if (kindOfTheGame == GamePlay<T>.eKindOfTheGame.PlayAgainstFriend)
            {
                m_MemoryGame = new GamePlay<T>(
                    firstUserName,
                    secondUserName,
                    GamePlay<T>.eKindOfTheGame.PlayAgainstFriend,
                    height,
                    width,
                    i_InputArray);
            }
            else
            {
                m_MemoryGame = new GamePlay<T>(
                    firstUserName,
                    secondUserName,
                    GamePlay<T>.eKindOfTheGame.PlayAgainstComputer,
                    height,
                    width,
                    i_InputArray);
            }
        }

        private void getDimensionFromTheUser(out int i_Height, out int i_Width)
        {
            Console.WriteLine("Please enter the height of the board");
            string heightAsString = Console.ReadLine();
            Console.WriteLine("Please enter the width of the board");
            string widthAsString = Console.ReadLine();

            bool isValidBoard = CheckBoardDimensions(heightAsString, widthAsString);
            while (isValidBoard == false)
            {
                Console.WriteLine("Invalid input,please enter again");
                Console.WriteLine("Please enter the height of the board");
                heightAsString = Console.ReadLine();
                Console.WriteLine("Please enter the width of the board");
                widthAsString = Console.ReadLine();
                isValidBoard = CheckBoardDimensions(heightAsString, widthAsString);
            }

            i_Height = int.Parse(heightAsString);
            i_Width = int.Parse(widthAsString);
        }

        private bool CheckBoardDimensions(string i_HeightAsString, string i_WidthAsString)
        {
            bool validHeightAndWidth = false;
            int heightAsNumber;
            int widthAsNumber;
            bool goodInputOfHeight = int.TryParse(i_HeightAsString, out heightAsNumber);
            bool goodInputOfWidth = int.TryParse(i_WidthAsString, out widthAsNumber);

            if (goodInputOfHeight == true && goodInputOfWidth == true)
            {
                if (CheckBoardSize(widthAsNumber, heightAsNumber) == true &&
                    isEvenBoard(widthAsNumber, heightAsNumber) == true)
                {
                    validHeightAndWidth = true;
                }
            }

            return validHeightAndWidth;
        }

        private bool isEvenBoard(int i_HeightAsNumber, int i_WidthAsNumber)
        {
            return (i_HeightAsNumber * i_WidthAsNumber) % 2 == 0;
        }

        private bool CheckBoardSize(int i_Width, int i_Height)
        {
            bool widthIsValid = false;
            bool heightIsValid = false;

            if (i_Width <= k_MaxDimensionSize && i_Width >= k_MinDimensionSize)
            {
                widthIsValid = true;
            }

            if (i_Height <= k_MaxDimensionSize && i_Height >= k_MinDimensionSize)
            {
                heightIsValid = true;
            }

            return widthIsValid && heightIsValid;
        }

        private string getTheNameOfTheUser()
        {
            Console.WriteLine("Please enter your name");
            string name = Console.ReadLine();
            while (string.IsNullOrEmpty(name))
            {
                Console.WriteLine("Invalid name,Please enter again");
                name = Console.ReadLine();
            }

            return name;
        }

        private GamePlay<T>.eKindOfTheGame getKindOfTheGameFromTheUser()
        {
            string msg = string.Format(@"Please choose the kind of game play : 
Press 0 to play against the computer
Press 1 to play against a friend
");
            Console.Write(msg);
            string numberForGetKindOfTheGame = Console.ReadLine();
            int numberInInt;
            bool goodInput = int.TryParse(numberForGetKindOfTheGame, out numberInInt);
            while (goodInput == false || ZeroOrOne(numberInInt) == false)
            {
                Console.Write("Invalid input,Please enter again");
                numberForGetKindOfTheGame = Console.ReadLine();
                goodInput = int.TryParse(numberForGetKindOfTheGame, out numberInInt);
            }

            return (GamePlay<T>.eKindOfTheGame)numberInInt;
        }

        public bool ZeroOrOne(int i_NumberInInt)
        {
            bool isZeroOrOne = i_NumberInInt == 0 || i_NumberInInt == 1;
            return isZeroOrOne;
        }
    }
}