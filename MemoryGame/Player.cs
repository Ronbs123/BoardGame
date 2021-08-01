using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryGame
{
    internal class Player
    {
        private string m_Name;
        private byte m_Score;
        private Random m_randCardGenerater = new Random();

        internal Player(string i_name)
        {
            this.m_Name = i_name;
            this.m_Score = 0;
        }

        internal string Name
        {
            get { return m_Name; }
            set { m_Name = value; }
        }

        internal byte Score
        {
            get { return m_Score; }
            set { m_Score = value; }
        }

        /*computer AI for smarter moves - 
        shuffeling the first card and then in probability of 0.5% looking if another card like the first one already reveald
        for more difficult level or more easy - all needed is just change the probability
        */
        internal string ComputerFirstMove(Board i_Board) 
        {
            string chosen_CardName;
            int rowNum = m_randCardGenerater.Next((int)(i_Board.Hight));
            int colNum = m_randCardGenerater.Next((int)(i_Board.Width));
            chosen_CardName = (rowNum.ToString() + colNum.ToString());
            //// if the card is already chosen then randomize again
            while (i_Board.CurrentGameStateBoard[rowNum,colNum] != ' ')
            {
                rowNum = m_randCardGenerater.Next((int)(i_Board.Hight));
                colNum = m_randCardGenerater.Next((int)(i_Board.Width));
                chosen_CardName = (rowNum.ToString() + colNum.ToString());
            }

            return chosen_CardName;
        }

        internal string ComputerSecondMove(Board i_Board, string i_FirstSlot)
        {
            string chosen_CardName = string.Empty;
            byte firstCardRowNum;
            byte firstCardColNum;
            byte.TryParse(i_FirstSlot[0].ToString(), out firstCardRowNum);
            byte.TryParse(i_FirstSlot[1].ToString(), out firstCardColNum);
            byte[] index = { firstCardRowNum, firstCardColNum };
            char card = i_Board.ComputerMemory[index[0], index[1]];
            int rowNum = m_randCardGenerater.Next((int)(i_Board.Hight));
            int colNum = m_randCardGenerater.Next((int)(i_Board.Width));
            double probabilityOfSmartMove = m_randCardGenerater.NextDouble();
            if (probabilityOfSmartMove <= 1)
            {
                for (int i = 0; i < i_Board.Hight; i++)
                {
                    for (int j = 0; j < i_Board.Width; j++)
                    {
                        if (i == index[0] && j == index[1])
                        {
                            continue;
                        }

                        if (i_Board.ComputerMemory[i, j] == card)
                        {
                            chosen_CardName = $"{i}{j}";
                        }
                    }
                }
            }

            if(chosen_CardName.Equals(string.Empty)) 
            {
                chosen_CardName = (rowNum.ToString() + colNum.ToString());
                //// if the card is already chosen then randomize again
                while (i_Board.CurrentGameStateBoard[rowNum, colNum] != ' ' || chosen_CardName.Equals(i_FirstSlot))
                {
                    rowNum = m_randCardGenerater.Next((int)(i_Board.Hight));
                    colNum = m_randCardGenerater.Next((int)(i_Board.Width));
                    chosen_CardName = (rowNum.ToString() + colNum.ToString());
                }
            }
            
            return chosen_CardName;
        }
        //// End of computer AI
    }
}