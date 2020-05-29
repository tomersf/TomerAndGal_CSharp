using System;
using System.Collections.Generic;
using System.Text;

namespace B20_Ex02
{
    internal class Player
    {
        private eKindOfThePlayer m_KindOfThePlayer;
        private int m_Score;
        private string m_Name;

        public Player(string i_Name, eKindOfThePlayer i_KindOfThePlayer)
        {
            m_Name = i_Name;
            m_KindOfThePlayer = i_KindOfThePlayer;
            m_Score = 0;
        }

        public eKindOfThePlayer KindOfThePlayer
        {
            get
            {
                return m_KindOfThePlayer;
            }

            set
            {
                m_KindOfThePlayer = value;
            }
        }

        public string Name
        {
            get
            {
                return m_Name;
            }

            set
            {
                m_Name = value;
            }
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

        public enum eKindOfThePlayer
        {
            Computer, User
        }
    }
}