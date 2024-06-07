using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PlatformGame
{
    public partial class Form1 : Form
    {
        bool goLeft, goRight, jumping, isGameOver;

        int jumpSpeed;
        int force;
        int score = 0;
        int playerSpeed = 7;

        int horizontalSpeed = 5;
        int horizontalSpeed2 = 4;
        int verticalSpeed = 3;

        int enemy1Speed = 5;
        int enemy2Speed = 3;

        public Form1()
        {
            InitializeComponent();
        }

        private void mainGameTimerEvent(object sender, EventArgs e)
        {
            txtScore.Text = "Score: " + score;
            player.Top += jumpSpeed;

            if (goLeft == true)
            {
                player.Left -= playerSpeed;
            }

            if (goRight == true)
            {
                player.Left += playerSpeed;
            }

            if (jumping == true && force < 0)
            {
                jumping = false;
            }

            if (jumping == true)
            {
                jumpSpeed = -8;
                force -= 1;
            }
            else
            {
                jumpSpeed = 10;
            }

            horizontalPlatform.Left -= horizontalSpeed;
            if (horizontalPlatform.Left < 0 || horizontalPlatform.Left + horizontalPlatform.Width > pictureBox4.Left + pictureBox4.Width)
            {
                horizontalSpeed = -horizontalSpeed;
            }

            horizontalPlatform2.Left -= horizontalSpeed2;
            if (horizontalPlatform2.Left < pictureBox4.Left || horizontalPlatform2.Left + horizontalPlatform2.Width > pictureBox4.Left + pictureBox4.Width)
            {
                horizontalSpeed2 = -horizontalSpeed2;
            }

            verticalPlatform.Top += verticalSpeed;
            if (verticalPlatform.Top < 150 || verticalPlatform.Top > 595)
            {
                verticalSpeed = -verticalSpeed;
            }

            enemy1.Left -= enemy1Speed;
            if (enemy1.Left < pictureBox4.Left || enemy1.Left + enemy1.Width > pictureBox4.Left + pictureBox4.Width)
            {
                if (enemy1Speed > 0)
                {
                    enemy1.Image = Properties.Resources.enemy;
                }
                else 
                {
                    enemy1.Image = Properties.Resources.enemyleft;
                }
                enemy1Speed = -enemy1Speed;
            }

            enemy2.Left += enemy2Speed;
            if (enemy2.Left < pictureBox2.Left || enemy2.Left + enemy2.Width > pictureBox2.Left + pictureBox2.Width)
            {
                if (enemy2Speed < 0)
                {
                    enemy2.Image = Properties.Resources.enemy;
                }
                else
                {
                    enemy2.Image = Properties.Resources.enemyleft;
                }
                enemy2Speed = -enemy2Speed;
            }

            if (score == 30)
            {
                txtScore.Text = "Score: " + score + Environment.NewLine + "Go to the door";
                door.Image = Properties.Resources.dooropen;
            }
            else
            {
                txtScore.Text = "Score: " + score + Environment.NewLine + "Collect all the coins";
            }
            if (player.Bounds.IntersectsWith(door.Bounds) && score == 30)
            {
                timer1.Stop();
                isGameOver = true;
                txtScore.Text = "Score: " + score + Environment.NewLine + "Your quest is complete!";
            }
            if (player.Top + player.Height > this.ClientSize.Height + 50)
            {
                timer1.Stop();
                isGameOver = true;
                txtScore.Text = "Score: " + score + Environment.NewLine + "You fell to your death!";
            }

            foreach (Control x in this.Controls)
            {
                if (x is PictureBox)
                {
                    if ((string)x.Tag == "platform")
                    {
                        if (player.Bounds.IntersectsWith(x.Bounds))
                        {
                            force = 8;
                            player.Top = x.Top - player.Height;
                            if ((string)x.Name == "horizontalPlatform" && goLeft == false || (string)x.Name == "horizontalPlatform" && goRight == false)
                            {
                                player.Left -= horizontalSpeed;
                            }
                        }
                        x.BringToFront();
                    }
                    if ((string)x.Tag == "coin")
                    {
                        if (player.Bounds.IntersectsWith(x.Bounds) && x.Visible == true)
                        {
                            x.Visible = false;
                            score++;
                        }
                    }
                    if ((string)x.Tag == "enemy")
                    {
                        if (player.Bounds.IntersectsWith(x.Bounds))
                        {
                            timer1.Stop();
                            isGameOver = true;
                            txtScore.Text = "Score: " + score + Environment.NewLine + "You were killed in your journey!!";
                            break;
                        }
                    }
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void keyIsDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                player.Image = Properties.Resources.walkleft;
                goLeft = true;
            }
            if (e.KeyCode == Keys.Right)
            {
                goRight = true;
                player.Image = Properties.Resources.walk;
            }
            if (e.KeyCode == Keys.Space && jumping == false)
            {
                jumping = true;
            }
        }

        private void keyIsUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goLeft = false;
            }
            if (e.KeyCode == Keys.Right)
            {
                goRight = false;
            }
            if (jumping == true)
            {
                jumping = false;
            }
            if (e.KeyCode == Keys.Enter && isGameOver == true)
            {
                restartGame();
            }
        }

        private void restartGame() 
        {

            jumping = false;
            goLeft = false;
            goRight = false;
            isGameOver = false;
            score = 0;
            txtScore.Text = "Score: " + score;

            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && x.Visible == false)
                {
                    x.Visible = true;
                }
            }

            player.Left = 71;
            player.Top = 636;
            enemy1.Left = 488;
            enemy2.Left = 488;
            horizontalPlatform.Left = 163;
            verticalPlatform.Top = 559;
            timer1.Start();

        }
    }
}
