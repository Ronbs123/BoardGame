using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MemoryGame
{
    public partial class FormMenu : Form
    {
        private bool m_IsAgainstFriend = false;
        private string m_Player1Name = string.Empty;
        private string m_Player2Name = "- computer -";
        private int m_GameBoardWidth = 4;
        private int m_GameBoardhight = 4;

        public FormMenu()
        {
            InitializeComponent();     
        }

        private void gameMode_Click(object sender, EventArgs e)
        {
            m_IsAgainstFriend = !m_IsAgainstFriend;
            if (m_IsAgainstFriend)
            {
                this.GameModeButton.Text = "Against Computer";
                this.SecondPlayerName.Text = "Enter Name";
                this.SecondPlayerName.Enabled = true;
            }
            else
            {
                this.GameModeButton.Text = "Against A Friend";
                this.SecondPlayerName.Text = "- computer -";
                this.SecondPlayerName.Enabled = false;
            }
        }

        private void BoardSize_Click(object sender, EventArgs e)
        {
            m_GameBoardhight = (m_GameBoardhight + 1) % 7;
            if (m_GameBoardhight == 0)
            {
                m_GameBoardWidth = (m_GameBoardWidth + 1) % 7;
                if (m_GameBoardWidth < 4)
                {
                    m_GameBoardWidth += 4;
                }
            }

            if (m_GameBoardhight == 5 && m_GameBoardWidth == 5)
            {
                m_GameBoardhight = 6;
            }

            if (m_GameBoardhight < 4)
            {
                m_GameBoardhight += 4;
            }

            this.BoardSizeButton.Text = string.Format("{0} X {1}", m_GameBoardWidth, m_GameBoardhight);
        }

        private void Start_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void FirstPlayerName_TextChanged(object sender, EventArgs e)
        {
            m_Player1Name = this.FirstPlayerName.Text;
        }

        private void SecondPlayerName_TextChanged(object sender, EventArgs e)
        {
            m_Player2Name = this.SecondPlayerName.Text;
        }

        internal Game GetNewGame()
        {
            Game newGame = new Game(m_IsAgainstFriend, m_Player1Name, m_Player2Name, m_GameBoardWidth, m_GameBoardhight);
            return newGame;
        }
    }
}
