using System;
using System.Collections.Generic;
using System.Text;

namespace B20_Ex02
{
    internal struct ComputerIntelligenceCell
    {
        private int m_Score;
        private int m_RowNumber;
        private int m_ColNumber;

        public ComputerIntelligenceCell(int i_Score, int i_Row, int i_Col)
        {
            m_Score = i_Score;
            m_RowNumber = i_Row;
            m_ColNumber = i_Col;
        }

        public static bool operator ==(ComputerIntelligenceCell i_X, ComputerIntelligenceCell i_Y)
        {
            return i_X.Row == i_Y.Row && i_X.Col == i_Y.Row;
        }

        public static bool operator !=(ComputerIntelligenceCell i_X, ComputerIntelligenceCell i_Y)
        {
            return i_X.Row != i_Y.Row || i_X.Col != i_Y.Row;
        }

        public override bool Equals(object i_AnotherCell)
        {
            try
            {
                ComputerIntelligenceCell secondCell = (ComputerIntelligenceCell)i_AnotherCell;
                return m_RowNumber == secondCell.Row && m_ColNumber == secondCell.Col;
            }
            catch(InvalidCastException ex)
            {
                return false;
            }
        }
        
        public ComputerIntelligenceCell(int i_Row, int i_Col)
        {
            m_Score = 0;
            m_RowNumber = i_Row;
            m_ColNumber = i_Col;
        }

        public int Score
        {
            get
            {
                return m_Score;
            }

            set
            {
                m_Score = value;
            }
        }

        public int Row
        {
            get
            {
                return m_RowNumber;
            }

            set
            {
                m_RowNumber = value;
            }
        }

        public int Col
        {
            get
            {
                return m_ColNumber;
            }

            set
            {
                m_ColNumber = value;
            }
        }
    }
}