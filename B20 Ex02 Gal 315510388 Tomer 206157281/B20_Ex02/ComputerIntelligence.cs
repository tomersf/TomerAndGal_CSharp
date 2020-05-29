using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;

namespace B20_Ex02
{
    internal class ComputerIntelligence
    {
        private List<ComputerIntelligenceCell> m_OrderOfAppearanceArray;
        private List<ComputerIntelligenceCell> m_CounterOfAppearanceArray;
        private List<ComputerIntelligenceCell> m_RandomArray;

        public ComputerIntelligence(int i_Rows, int i_Cols)
        {
            m_OrderOfAppearanceArray = new List<ComputerIntelligenceCell>(i_Rows * i_Cols);
            m_CounterOfAppearanceArray = new List<ComputerIntelligenceCell>(i_Rows * i_Cols);
            m_RandomArray = new List<ComputerIntelligenceCell>(10);
            for (int i = 0; i < i_Rows; i++)
            {
                for (int j = 0; j < i_Cols; j++)
                {
                    ComputerIntelligenceCell newCellToAddToOrderAppearanceArray = new ComputerIntelligenceCell(0, i, j);
                    m_OrderOfAppearanceArray.Add(newCellToAddToOrderAppearanceArray);

                    ComputerIntelligenceCell newCellToAddToAppearanceArray = new ComputerIntelligenceCell(0, i, j);
                    m_CounterOfAppearanceArray.Add(newCellToAddToAppearanceArray);
                }
            }

            m_RandomArray = new List<ComputerIntelligenceCell>();
        }

        public ComputerIntelligenceCell GetIntelligenceCellFromTheComputer(ComputerIntelligenceCell i_RandomCellFromTheComputer, Board i_TheBoard)
        {
            m_RandomArray = new List<ComputerIntelligenceCell>();
            ComputerIntelligenceCell cellByPercentage = getCellByPercentage(i_RandomCellFromTheComputer, i_TheBoard);
            return cellByPercentage;
        }

        public void NewAppearanceOfCell(int i_Row, int i_Col)
        {
            addOneToTheCellInCounterArray(i_Row, i_Col);

            for (int i = 0; i < m_OrderOfAppearanceArray.Count; i++)
            {
                int rowOfCurrentCell = m_OrderOfAppearanceArray[i].Row;
                int colOfCurrentCell = m_OrderOfAppearanceArray[i].Col;
                if (rowOfCurrentCell != i_Row || colOfCurrentCell != i_Col)
                {
                    continue;
                }

                m_OrderOfAppearanceArray.RemoveAt(i);
                m_OrderOfAppearanceArray.Add(new ComputerIntelligenceCell(rowOfCurrentCell, colOfCurrentCell));
                break;
            }
        }

        private static int comparingByCountOfAppearance(ComputerIntelligenceCell i_FirstCell, ComputerIntelligenceCell i_SecondCell)
        {
            if (i_FirstCell.Score < i_SecondCell.Score)
            {
                return -1;
            }
            else if (i_FirstCell.Score == i_SecondCell.Score)
            {
                return 0;
            }

            return 1;
        }

        public void addOneToTheCellInCounterArray(int i_Row, int i_Col)
        {
            for (int i = 0; i < m_CounterOfAppearanceArray.Count; i++)
            {
                int rowConcurrentCell = m_CounterOfAppearanceArray[i].Row;
                int colOfCurrentCell = m_CounterOfAppearanceArray[i].Col;
                if (rowConcurrentCell == i_Row && colOfCurrentCell == i_Col)
                {
                    int score = m_CounterOfAppearanceArray[i].Score + 1;
                    m_CounterOfAppearanceArray[i] = new ComputerIntelligenceCell(score, i_Row, i_Col);
                    m_CounterOfAppearanceArray.Sort(comparingByCountOfAppearance);
                    break;
                }
            }
        }

        public void NewAppearanceOfCell(string i_CellAsString)
        {
            int colAsInt = i_CellAsString[0] - 'A';
            int rowAsInt = i_CellAsString[1] - '0' - 1;
            NewAppearanceOfCell(rowAsInt, colAsInt);
        }

        private ComputerIntelligenceCell getCellByPercentage(
            ComputerIntelligenceCell i_RandomCellFromTheComputer,
            Board i_TheBoard)
        {
            ComputerIntelligenceCell cellToReturn;

            if (checkIfTwoCellHaveSameValue(
                   i_RandomCellFromTheComputer,
                   eDistanceFromTheLastCellInTheArr.Zero,
                   i_TheBoard) == true)
            {
                cellToReturn = percentageCalculation(
                    i_RandomCellFromTheComputer,
                    90,
                    eDistanceFromTheLastCellInTheArr.Zero,
                    i_TheBoard);
            }
            else
            {
                if (checkIfTwoCellHaveSameValue(
                       i_RandomCellFromTheComputer,
                       eDistanceFromTheLastCellInTheArr.One,
                       i_TheBoard) == true)
                {
                    cellToReturn = percentageCalculation(
                        i_RandomCellFromTheComputer,
                        80,
                        eDistanceFromTheLastCellInTheArr.One,
                        i_TheBoard);
                }
                else if (checkIfTwoCellHaveSameValue(
                            i_RandomCellFromTheComputer,
                            eDistanceFromTheLastCellInTheArr.Two,
                            i_TheBoard) == true)
                {
                    cellToReturn = percentageCalculation(
                        i_RandomCellFromTheComputer,
                        70,
                        eDistanceFromTheLastCellInTheArr.Two,
                        i_TheBoard);
                }
                else
                {
                    cellToReturn = getRandomTurn(i_TheBoard);
                }
            }

            if (cellToReturn == i_RandomCellFromTheComputer
                   || i_TheBoard.Table[cellToReturn.Row, cellToReturn.Col].Discovered == true)
            {
                cellToReturn = getEmptyCellFromTheBorder(i_TheBoard);
            }

            if (cellToReturn == i_RandomCellFromTheComputer
               || i_TheBoard.Table[cellToReturn.Row, cellToReturn.Col].Discovered == true)
            {
                cellToReturn = getEmptyCellFromTheBeginningOfTheBoard(i_TheBoard);
            }

            return cellToReturn;
        }

        private bool checkIfTwoCellHaveSameValue(
            ComputerIntelligenceCell i_RandomCellFromTheComputer,
            eDistanceFromTheLastCellInTheArr i_Distance,
            Board i_Board)
        {
            bool sameValue = false;
            int rowNumberOfRandomCell = i_RandomCellFromTheComputer.Row;
            int colNumberOfRandomCell = i_RandomCellFromTheComputer.Col;

            int rowNumberOfCellOrderArr = m_OrderOfAppearanceArray[m_OrderOfAppearanceArray.Count - (int)i_Distance - 1].Row;
            int colNumberOfCellOrderArr = m_OrderOfAppearanceArray[m_OrderOfAppearanceArray.Count - (int)i_Distance - 1].Col;
            if (i_Board.Table[rowNumberOfRandomCell, colNumberOfRandomCell].Value
                == i_Board.Table[rowNumberOfCellOrderArr, colNumberOfCellOrderArr].Value)
            {
                if (rowNumberOfRandomCell != rowNumberOfCellOrderArr && colNumberOfRandomCell != colNumberOfCellOrderArr)
                {
                    sameValue = true;
                }
            }
            else
            {
                int rowNumberOfCellCounterArr = m_CounterOfAppearanceArray[m_OrderOfAppearanceArray.Count - (int)i_Distance - 1].Row;
                int colNumberOfCellCounterArr = m_CounterOfAppearanceArray[m_OrderOfAppearanceArray.Count - (int)i_Distance - 1].Col;
                if (i_Board.Table[rowNumberOfRandomCell, colNumberOfRandomCell].Value
                    == i_Board.Table[rowNumberOfCellCounterArr, colNumberOfCellCounterArr].Value)
                {
                    if (rowNumberOfRandomCell != rowNumberOfCellCounterArr && colNumberOfRandomCell != colNumberOfCellCounterArr)
                    {
                        sameValue = true;
                    }
                }
            }

            return sameValue;
        }

        public enum eDistanceFromTheLastCellInTheArr
        {
            Zero, One, Two
        }

        private ComputerIntelligenceCell getRandomTurn(Board i_Board)
        {
            Random randomVal = new Random();
            List<ComputerIntelligenceCell> newRandomArray = new List<ComputerIntelligenceCell>();
            for (int i = 0; i < i_Board.Height; i++)
            {
                for (int j = 0; j < i_Board.Width; j++)
                {
                    if (i_Board.Table[i, j].Discovered == false)
                    {
                        ComputerIntelligenceCell newCell = new ComputerIntelligenceCell(i, j);
                        newRandomArray.Add(newCell);
                    }
                }
            }

            int randomValue = randomVal.Next(newRandomArray.Count);
            return newRandomArray[randomValue];
        }

        private ComputerIntelligenceCell percentageCalculation(ComputerIntelligenceCell i_RandomCellFromTheComputer, int i_Percentage, eDistanceFromTheLastCellInTheArr i_DistanceFromTheLastCell, Board i_TheBoard)
        {
            int rowNumberOfCellOrderArr = m_OrderOfAppearanceArray[m_OrderOfAppearanceArray.Count - (int)i_DistanceFromTheLastCell - 1].Row;
            int colNumberOfCellOrderArr = m_OrderOfAppearanceArray[m_OrderOfAppearanceArray.Count - (int)i_DistanceFromTheLastCell - 1].Col;
            int scoreOfOfCellOrderArr = m_OrderOfAppearanceArray[m_OrderOfAppearanceArray.Count - (int)i_DistanceFromTheLastCell - 1].Score;
            m_RandomArray.Clear();
            for (int i = 0; i < i_Percentage / 10; i++)
            {
                ComputerIntelligenceCell newCellToAdd = new ComputerIntelligenceCell(scoreOfOfCellOrderArr, rowNumberOfCellOrderArr, colNumberOfCellOrderArr);
                m_RandomArray.Add(newCellToAdd);
            }

            for (int j = i_Percentage / 10; j < 10; j++)
            {
                ComputerIntelligenceCell cellToAdd = getRandomTurn(i_TheBoard);
                ComputerIntelligenceCell cellToAddToRandomArray = new ComputerIntelligenceCell(cellToAdd.Score, cellToAdd.Row, cellToAdd.Col);
                m_RandomArray.Add(cellToAddToRandomArray);
            }

            Random randomValue = new Random();
            int randomValueAsInt = randomValue.Next(m_RandomArray.Count);
            return m_RandomArray[randomValueAsInt];
        }

        private ComputerIntelligenceCell getEmptyCellFromTheBorder(Board i_TheBoard)
        {
            ComputerIntelligenceCell unsignedCellToReturn = new ComputerIntelligenceCell();
            for (int i = 0; i < i_TheBoard.Height; i++)
            {
                for (int j = 0; j < i_TheBoard.Width; j++)
                {
                    if (i_TheBoard.Table[i, j].Discovered == false)
                    {
                        unsignedCellToReturn = new ComputerIntelligenceCell(i, j);
                    }
                }
            }

            return unsignedCellToReturn;
        }

        private ComputerIntelligenceCell getEmptyCellFromTheBeginningOfTheBoard(Board i_TheBoard)
        {
            ComputerIntelligenceCell unsignedCellToReturn = new ComputerIntelligenceCell();
            for (int i = i_TheBoard.Height - 1; i >= 0; i--)
            {
                for (int j = i_TheBoard.Width - 1; j >= 0; j--)
                {
                    if (i_TheBoard.Table[i, j].Discovered == false)
                    {
                        unsignedCellToReturn = new ComputerIntelligenceCell(i, j);
                    }
                }
            }

            return unsignedCellToReturn;
        }

        public List<ComputerIntelligenceCell> OrderOfAppearanceArray
        {
            get
            {
                return m_OrderOfAppearanceArray;
            }

            set
            {
                m_OrderOfAppearanceArray = value;
            }
        }

        public List<ComputerIntelligenceCell> CounterOfAppearanceArray
        {
            get
            {
                return m_CounterOfAppearanceArray;
            }

            set
            {
                m_CounterOfAppearanceArray = value;
            }
        }

        public List<ComputerIntelligenceCell> RandomArray
        {
            get
            {
                return m_RandomArray;
            }

            set
            {
                m_RandomArray = value;
            }
        }
    }
}
