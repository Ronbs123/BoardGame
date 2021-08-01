using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MemoryGame
{
    internal class Game
    {
        private bool m_AgainstFriend;
        private Player m_Player1;
        private Player m_Player2;
        private Board m_GameBoard;
        private string m_FirstPickedCardName;
        private byte m_CurrentTurn = 0; // 0 for first player turn and 1 for second player turn
        private bool m_IsFirstCardPick = true; // true for first pick and false for second pick

        internal Game(bool i_AgainstFriend, string i_Player1Name, string i_Player2Name, int i_BoardWidth, int i_BoardHight)
        {
            this.m_AgainstFriend = i_AgainstFriend;
            this.m_Player1 = new Player(i_Player1Name);
            this.m_Player2 = new Player(i_Player2Name);
            this.m_GameBoard = new Board(i_BoardWidth, i_BoardHight);
        }

        public bool IsAgainstFriend
        {
            get { return m_AgainstFriend; }
            set { m_AgainstFriend = value; }
        }

        public string FirstPickedCardName
        {
            get { return m_FirstPickedCardName; }
            set { m_FirstPickedCardName = value; }
        }

        public Player Player1
        {
            get { return m_Player1; }
        }

        public Player Player2
        {
            get { return m_Player2; }
        }

        public Board GameBoard
        {
            get { return m_GameBoard; }
        }

        public byte CurrentTurn
        {
            get { return m_CurrentTurn; }
            set { m_CurrentTurn = value; }
        }

        public bool IsFirstCardPick
        {
            get { return m_IsFirstCardPick; }
            set { m_IsFirstCardPick = value; }
        }

        public bool FlipCard(string i_CardName)
        {
            bool isGuessedCurrectley = false;
            byte rowNum;
            byte colNum;
            byte.TryParse(i_CardName[0].ToString(), out rowNum);
            byte.TryParse(i_CardName[1].ToString(), out colNum);
            bool ans = false;
            if (m_IsFirstCardPick)
            {
                m_FirstPickedCardName = i_CardName;
                IsFirstCardPick = false;
                m_GameBoard.ComputerMemory[rowNum, colNum] = m_GameBoard.SourceBoard[rowNum, colNum];
            }
            else
            {
                int rowNumOfFirstPick;
                int colNumOfFirstPick;
                int.TryParse(m_FirstPickedCardName[0].ToString(), out rowNumOfFirstPick);
                int.TryParse(m_FirstPickedCardName[1].ToString(), out colNumOfFirstPick);
                m_GameBoard.ComputerMemory[rowNum, colNum] = m_GameBoard.SourceBoard[rowNum, colNum];
                if (m_GameBoard.SourceBoard[rowNum, colNum].Equals(m_GameBoard.SourceBoard[rowNumOfFirstPick, colNumOfFirstPick]))
                {
                    m_GameBoard.CurrentGameStateBoard[rowNumOfFirstPick, colNumOfFirstPick] = m_GameBoard.SourceBoard[rowNumOfFirstPick, colNumOfFirstPick];
                    m_GameBoard.CurrentGameStateBoard[rowNum, colNum] = m_GameBoard.SourceBoard[rowNum, colNum];
                    if (CurrentTurn == 0)
                    {
                        Player1.Score++;
                    }
                    else
                    {
                        Player2.Score++;
                    }

                    ans = true;
                    isGuessedCurrectley = true;
                }

                IsFirstCardPick = true;
                if (!isGuessedCurrectley)
                {
                    CurrentTurn = (byte)(1 - CurrentTurn); //// making the turn to switch to the other player
                }
            }

            return ans;
        }
    }
}