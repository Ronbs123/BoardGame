using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryGame
{
    internal class Board
    {
        private readonly char[,] r_SourceBoard;
        private readonly char[,] r_CurrentGameStateBoard;
        private readonly char[,] r_ComputerMemory;
        private int m_BoardWidth;
        private int m_BoardHight;
        private Random m_cardRandGenerater = new Random();

        internal Board(int i_boardHight, int i_boardWidth)
        {
            m_BoardWidth = i_boardWidth;
            m_BoardHight = i_boardHight;
            r_SourceBoard = new char[m_BoardHight, m_BoardWidth];
            r_CurrentGameStateBoard = new char[m_BoardHight, m_BoardWidth];
            r_ComputerMemory = new char[m_BoardHight, m_BoardWidth];
            InitializeBoards();
        }

        internal int Hight
        {
            get { return m_BoardHight; }
            set { m_BoardHight = value; }
        }

        internal int Width
        {
            get { return m_BoardWidth; }
            set { m_BoardWidth = value; }
        }

        internal char[,] SourceBoard
        {
            get { return r_SourceBoard; }
        }

        internal char[,] CurrentGameStateBoard
        {
            get { return r_CurrentGameStateBoard; }
        }

        internal char[,] GameBoard
        {
            get { return r_SourceBoard; }
        }

        internal char[,] ComputerMemory
        {
            get { return r_ComputerMemory; }
        }

        private void InitializeBoards()
        {
            for (int i = 0; i < m_BoardHight; i++)
            {
                for (int j = 0; j < m_BoardWidth; j++)
                {
                    r_CurrentGameStateBoard[i, j] = ' ';
                    r_ComputerMemory[i, j] = ' ';
                }
            }

            List<char> cards = new List<char>();
            char cardValue = 'A';
            byte cardIndex = 0;
            while (cardIndex < m_BoardWidth * m_BoardHight)
            {
                cards.Add(cardValue);
                cards.Add(cardValue);
                cardValue++;
                cardIndex += 2;
            }

            byte random_Location = 0;
            for (int i = 0; i < m_BoardHight; i++)
            {
                for (int j = 0; j < m_BoardWidth; j++)
                {
                    random_Location = (byte)m_cardRandGenerater.Next(cards.Count);
                    r_SourceBoard[i, j] = cards[random_Location];
                    cards.RemoveAt(random_Location);
                }
            }
        }

        internal bool IsGameFinished(byte i_Player1Score, byte i_Player2Score)
        {
            bool ans = false;
            if (((i_Player1Score + i_Player2Score) * 2) == m_BoardHight * m_BoardWidth)
            {
                ans = true;
            }

            return ans;
        }
    }
}