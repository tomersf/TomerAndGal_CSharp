using System;
using System.Collections.Generic;
using System.Security.AccessControl;
using System.Text;

namespace B20_Ex02
{
    internal struct Cell
    {
        private int m_Value;
        private bool m_Discovered;

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

        public bool Discovered
        {
            get
            {
                return m_Discovered;
            }

            set
            {
                m_Discovered = value;
            }
        }
    }
}