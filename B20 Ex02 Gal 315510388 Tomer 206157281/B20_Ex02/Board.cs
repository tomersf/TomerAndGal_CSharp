using System;
using System.Collections.Generic;
using System.Text;

namespace B20_Ex02
{
    internal class Board
    {
        private const int k_MaxAppearances = 2;
        private int m_Height;
        private int m_Width;
        private Cell[,] m_Board;

        public Cell[,] Table
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

        public int Height
        {
            get
            {
                return m_Height;
            }

            set
            {
                m_Height = value;
            }
        }

        public int Width
        {
            get
            {
                return m_Width;
            }

            set
            {
                m_Width = value;
            }
        }

        public void RemoveSignToCell(ComputerIntelligenceCell i_Cell)
        {
            m_Board[i_Cell.Row, i_Cell.Col].Discovered = false;
        }

        public void RemoveSignToCell(string i_NewCellToSign)
        {
            int colAsInt = i_NewCellToSign[0] - 'A';
            int rowAsInt = i_NewCellToSign[1] - '0' - 1;
            m_Board[rowAsInt, colAsInt].Discovered = false;
        }

        public void AddSignToCell(string i_NewCellToSign)
        {
            int colAsInt = i_NewCellToSign[0] - 'A';
            int rowAsInt = i_NewCellToSign[1] - '0' - 1;
            m_Board[rowAsInt, colAsInt].Discovered = true;
        }

        public void InitializeOfTheBoard()
        {
            int rangeOfRandomValues = (m_Height * m_Width) / 2;
            List<BucketListCell> valuesToInsertIntoBoard = new List<BucketListCell>(rangeOfRandomValues);
            for (int i = 0; i < rangeOfRandomValues; i++)
            {
                BucketListCell bucketListCellToAdd = new BucketListCell(i);
                valuesToInsertIntoBoard.Add(bucketListCellToAdd);
            }

            putRandomIntegersInBoardCells(valuesToInsertIntoBoard, rangeOfRandomValues);
        }

        private void putRandomIntegersInBoardCells(List<BucketListCell> i_ValuesToInsertIntoBoard, int i_RangeOfRandomValues)
        {
            Random randomVal = new Random();
            for (int i = 0; i < m_Height; i++)
            {
                for (int j = 0; j < m_Width; j++)
                {
                    int randomValue = randomVal.Next(i_RangeOfRandomValues);
                    while (i_ValuesToInsertIntoBoard[randomValue].Counter == k_MaxAppearances && i_ValuesToInsertIntoBoard.Count > 0)
                    {
                        i_ValuesToInsertIntoBoard.RemoveAt(randomValue);
                        i_RangeOfRandomValues--;
                        randomValue = randomVal.Next(i_RangeOfRandomValues);
                    }

                    int valueInCurrentCell = i_ValuesToInsertIntoBoard[randomValue].Value;
                    int counterInCurrentCell = i_ValuesToInsertIntoBoard[randomValue].Counter;
                    i_ValuesToInsertIntoBoard[randomValue] = new BucketListCell(valueInCurrentCell, counterInCurrentCell + 1);
                    m_Board[i, j].Value = i_ValuesToInsertIntoBoard[randomValue].Value;
                }
            }
        }

        public Board(int i_Height, int i_Width)
        {
            m_Width = i_Width;
            m_Height = i_Height;
            m_Board = new Cell[i_Height, i_Width];
        }

        public bool CheckIfTheCellIsValid(string i_CellNumberAsString)
        {
            bool validCell = false;
            if (i_CellNumberAsString.Length == 2)
            {
                int colAsInt = i_CellNumberAsString[0] - 'A';
                int rowAsInt = i_CellNumberAsString[1] - '0' - 1;
                if (IsValidCol(colAsInt) == true && IsValidRow(rowAsInt) == true)
                {
                    if (m_Board[rowAsInt, colAsInt].Discovered == false)
                    {
                        validCell = true;
                    }
                }
            }

            return validCell;
        }

        public bool IsValidCol(int i_ColNumber)
        {
            return i_ColNumber < m_Width && i_ColNumber >= 0;
        }

        public bool IsValidRow(int i_RowNumber)
        {
            return i_RowNumber >= 0 && i_RowNumber < m_Height;
        }
    }
}