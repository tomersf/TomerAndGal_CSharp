using System;
using System.Collections.Generic;
using System.Text;

namespace B20_Ex02
{
    internal class BucketListCell
    {
        private int m_Value;
        private int m_Counter;

        public int Value
        {
            get
            {
                return m_Value;
            }

            set
            {
                m_Value = value;
            }
        }

        public int Counter
        {
            get
            {
                return m_Counter;
            }

            set
            {
                m_Counter = value;
            }
        }

        public BucketListCell(int i_Value)
        {
            m_Value = i_Value;
            m_Counter = 0;
        }

        public BucketListCell(int i_Value, int i_Counter)
        {
            m_Counter = i_Counter;
            m_Value = i_Value;
        }
    }
}