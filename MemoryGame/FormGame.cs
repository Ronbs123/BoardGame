using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MemoryGame
{
    public class FormGame : Form
    {
        private readonly Dictionary<char, Image> r_imagesByLetters = new Dictionary<char, Image>();
        private Game m_Game;

        internal FormGame(Game i_NewGame)
        {
            this.m_Game = i_NewGame;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.FormBorderStyle = FormBorderStyle.Fixed3D;
            this.Text = "Memory Game";
            for (int i = 0; i < m_Game.GameBoard.Hight; i++)
            {
                for (int j = 0; j < m_Game.GameBoard.Width; j++)
                {
                    Button button = new Button();
                    button.Size = new Size(100, 100);
                    button.Name = string.Format("{0}{1}", i, j);
                    button.Text = string.Empty;
                    button.BackColor = default(Color);
                    button.Location = new Point(i * 110 + 20, j * 110 + 20);
                    button.Margin = new Padding(20, 20, 20, 20);
                    button.Click += new EventHandler(Button_Click);
                    this.Controls.Add(button);
                }
            }

            /*
            Curren Player Label
            */
            Label CurrentPlayer = new Label();
            CurrentPlayer.AutoSize = true;
            CurrentPlayer.Location = new Point(20, m_Game.GameBoard.Width * 110 + 30);
            CurrentPlayer.Name = "CurrentPlayerName";
            if (m_Game.CurrentTurn == 0)
            {
                CurrentPlayer.Text = string.Format("Current player: {0}", m_Game.Player1.Name);
                CurrentPlayer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            }
            else
            {
                CurrentPlayer.Text = string.Format("Current player: {0}", m_Game.Player2.Name);
                CurrentPlayer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            }

            this.Controls.Add(CurrentPlayer);
            /*
            Player 1 score
            */
            Label Player1Score = new Label();
            Player1Score.AutoSize = true;
            Player1Score.Location = new Point(CurrentPlayer.Location.X, CurrentPlayer.Location.Y + 30);
            Player1Score.Name = "PlayerOneScore";
            Player1Score.Text = string.Format("{0} : 0 Pairs", m_Game.Player1.Name);
            Player1Score.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.Controls.Add(Player1Score);
            /*
            Player 2 score
            */
            Label Player2Score = new Label();
            Player2Score.Margin = new Padding(20, 20, 20, 20);
            Player2Score.AutoSize = true;
            Player2Score.Location = new Point(Player1Score.Location.X, Player1Score.Location.Y + 30);
            Player2Score.Name = "PlayerTwoScore";
            Player2Score.Text = string.Format("{0} : 0 Pairs", m_Game.Player2.Name);
            Player2Score.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.Controls.Add(Player2Score);
            this.MaximizeBox = false;
            /*
            initializing table size
            */
            this.AutoSize = true;
            this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            InitializeImagesForEachLetter();
        }

        private void InitializeImagesForEachLetter()
        {
            WebClient wc = new WebClient();
            char currentLetter = 'A';
            for (int i = 0; i < (m_Game.GameBoard.Width * m_Game.GameBoard.Hight) / 2; i++)
            {
                byte[] bytes = wc.DownloadData("https://picsum.photos/80");
                MemoryStream ms = new MemoryStream(bytes);
                Image imgFromWeb = Image.FromStream(ms);
                r_imagesByLetters.Add(currentLetter, imgFromWeb);
                currentLetter++;
            }
        }

        public void UpdateCurrentPlayerLabel()
        {
            if (m_Game.CurrentTurn == 0)
            {
                Label currentPlayerName = this.Controls.Find("CurrentPlayerName", true).FirstOrDefault() as Label;
                currentPlayerName.Text = string.Format("Current player: {0}", m_Game.Player1.Name);
                currentPlayerName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
                currentPlayerName.Update();
            }
            else
            {
                Label currentPlayerName = this.Controls.Find("CurrentPlayerName", true).FirstOrDefault() as Label;
                currentPlayerName.Text = string.Format("Current player: {0}", m_Game.Player2.Name);
                currentPlayerName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
                currentPlayerName.Update();
            }
        }

        public void UpdatePlayersScore()
        {
            /*
            Updating First Player's score
            */
            Label PlayerOneScore = this.Controls.Find("PlayerOneScore", true).FirstOrDefault() as Label;
            PlayerOneScore.Text = string.Format("{0}: {1} Pairs", m_Game.Player1.Name, m_Game.Player1.Score);
            PlayerOneScore.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            PlayerOneScore.Update();
            /*
            Updating Second Player's score
            */
            Label PlayerTwoScore = this.Controls.Find("PlayerTwoScore", true).FirstOrDefault() as Label;
            PlayerTwoScore.Text = string.Format("{0}: {1} Pairs", m_Game.Player2.Name, m_Game.Player2.Score);
            PlayerTwoScore.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            PlayerTwoScore.Update();
        }

        private void Button_Click(object sender, EventArgs e)
        {
            Button buttonClicked = sender as Button;
            string firstCardName = m_Game.FirstPickedCardName;
            if (m_Game.IsAgainstFriend || (!m_Game.IsAgainstFriend && m_Game.CurrentTurn == 0))
            {
                PlayerMove(buttonClicked, firstCardName);
            }

            if(!m_Game.IsAgainstFriend && m_Game.CurrentTurn == 1)
            {
                ComputerMove();
            }
        }

        private void PlayerMove(Button i_ButtonClicked, string i_FirstCardName)
        {
            string cardName = i_ButtonClicked.Name;
            byte rowNum;
            byte colNum;
            byte.TryParse(cardName[0].ToString(), out rowNum);
            byte.TryParse(cardName[1].ToString(), out colNum);
            if (m_Game.CurrentTurn == 0)
            {
                i_ButtonClicked.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            }
            else
            {
                i_ButtonClicked.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            }

            char cardLetter = m_Game.GameBoard.SourceBoard[rowNum, colNum];
            i_ButtonClicked.Image = r_imagesByLetters[cardLetter];
            i_ButtonClicked.Click -= Button_Click;
            i_ButtonClicked.Update();
            //// gets into the if statement only if two picks was not guessed succesfully - and flips the cards that been chosen back to hiden state
            bool isPairFound = this.m_Game.FlipCard(cardName);
            if (!isPairFound && this.m_Game.IsFirstCardPick)
            {
                System.Threading.Thread.Sleep(1000);
                Button button = this.Controls.Find(i_FirstCardName, true).FirstOrDefault() as Button;
                button.Image = null;
                button.BackColor = default(Color);
                button.Click += Button_Click;
                button.Update();
                i_ButtonClicked.Image = null;
                i_ButtonClicked.BackColor = default(Color);
                i_ButtonClicked.Click += Button_Click;
                i_ButtonClicked.Update();
                UpdateCurrentPlayerLabel();
            }
            else
            {
                //// gets into the if statement only if two picks was guessed succesfully - and disabeling the click on 2 chosen cards
                if (isPairFound && this.m_Game.IsFirstCardPick)
                {
                    InitializeBoardAfterSuccesfullTurn(i_FirstCardName);
                }
            }
        }

        private void ComputerMove()
        {
            while (m_Game.CurrentTurn == 1)
            {
                string firstCardName = m_Game.FirstPickedCardName;
                bool isPairFound;
                byte rowNum;
                byte colNum;
                /*
                Computer's first move
                */
                firstCardName = m_Game.Player2.ComputerFirstMove(m_Game.GameBoard);
                byte.TryParse(firstCardName[0].ToString(), out rowNum);
                byte.TryParse(firstCardName[1].ToString(), out colNum);
                Button button = this.Controls.Find(firstCardName, true).FirstOrDefault() as Button;
                button.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
                char cardLetter = m_Game.GameBoard.SourceBoard[rowNum, colNum];
                button.Image = r_imagesByLetters[cardLetter];
                button.Click -= Button_Click;
                button.Update();
                System.Threading.Thread.Sleep(1000);
                this.m_Game.FlipCard(firstCardName);
                /*
                Computer's Second move
                */
                string cardName = m_Game.Player2.ComputerSecondMove(m_Game.GameBoard, firstCardName);
                byte.TryParse(cardName[0].ToString(), out rowNum);
                byte.TryParse(cardName[1].ToString(), out colNum);
                Button button2 = this.Controls.Find(cardName, true).FirstOrDefault() as Button;
                button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
                cardLetter = m_Game.GameBoard.SourceBoard[rowNum, colNum];
                button2.Image = r_imagesByLetters[cardLetter];
                button2.Click -= Button_Click;
                button2.Update();
                isPairFound = this.m_Game.FlipCard(cardName);
                if (!isPairFound)
                {
                    System.Threading.Thread.Sleep(1000);
                    button.Image = null;
                    button.BackColor = default(Color);
                    button.Click += Button_Click;
                    button.Update();
                    button2.Image = null;
                    button2.BackColor = default(Color);
                    button2.Click += Button_Click;
                    button2.Update();
                    UpdateCurrentPlayerLabel();
                }
                else
                {
                    UpdateCurrentPlayerLabel();
                    UpdatePlayersScore();
                    if (m_Game.GameBoard.IsGameFinished(m_Game.Player1.Score, m_Game.Player2.Score))
                    {
                        InitializeBoardAfterSuccesfullTurn(firstCardName);
                    }

                    System.Threading.Thread.Sleep(1000);
                }
            }
        }

        private void InitializeBoardAfterSuccesfullTurn(string i_FirstCardName)
        {
            Button button = this.Controls.Find(i_FirstCardName, true).FirstOrDefault() as Button;
            button.Update();
            UpdateCurrentPlayerLabel();
            UpdatePlayersScore();
            if (m_Game.GameBoard.IsGameFinished(m_Game.Player1.Score, m_Game.Player2.Score))
            {
                if (m_Game.Player1.Score > m_Game.Player2.Score)
                {
                    MassageBoxEndGame(m_Game.Player1);
                }
                else if (m_Game.Player2.Score > m_Game.Player1.Score)
                {
                    MassageBoxEndGame(m_Game.Player2);
                }
                else
                {
                    MassageBoxEndGame(null);
                }
            }
        }

        private void MassageBoxEndGame(Player i_WinnerPlayer)
        {
            string message = i_WinnerPlayer == null
                                 ? "it was a tie"
                                 : $"{i_WinnerPlayer.Name} won with {i_WinnerPlayer.Score}";
            message = string.Format("{0} would you like to play again?", message);
            const string caption = "End Of Game";
            DialogResult result = MessageBox.Show(message, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                this.Close();
                this.Dispose();
                ResetForm();
            }
            else
            {
                System.Diagnostics.Process.GetCurrentProcess().Kill();
            }
        }

        private void ResetForm()
        {
            Game newGame = new Game(m_Game.IsAgainstFriend, m_Game.Player1.Name, m_Game.Player2.Name, m_Game.GameBoard.Hight, m_Game.GameBoard.Width);
            FormGame newGameForm = new FormGame(newGame);
            newGameForm.ShowDialog();
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }
    }
}