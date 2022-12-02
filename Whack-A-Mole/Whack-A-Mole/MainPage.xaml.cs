using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Whack_A_Mole
{
    public partial class MainPage : ContentPage
    {
        Random _random;
        System.Timers.Timer _timer;
        int _MyInterval = 1000; // Milliseconds
        int _CountdownStart = 30;

    public MainPage()
        {
            InitializeComponent();
            _random = new Random();
            SetupTimers();
        }

        private void SetupTimers()
        {
            _timer = new System.Timers.Timer();
            _timer.Interval = _MyInterval;
            _timer.Elapsed += _timer_Elapsed;

            LblCountdown.Text = _CountdownStart.ToString();
        }

        private void _timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            Device.BeginInvokeOnMainThread(
                () =>
                {
                    MoveTheMole();
                }
                );
        }

        private void MoveTheMole()
        {
            int countdown = Convert.ToInt32(LblCountdown.Text);
            countdown--;
            LblCountdown.Text = countdown.ToString();

            if (countdown < 6 && countdown > 3)
            {
                LblCountdown.TextColor = Color.Yellow;
            }
            else if (countdown == 3)
            {
                LblCountdown.TextColor = Color.Orange;
            }
            else if (countdown < 3)
            {
                LblCountdown.TextColor = Color.Red;
            }
            else
            {
                LblCountdown.TextColor = Color.Black;
            }

            if (countdown <= 0)
            {
                EndScore();
                _timer.Stop();
                BtnMoveTest.Text = "Start Game";
            }



            int r, c, maxRow = 4, maxCol = 4;
            ImageButton currentImgBtn = ImgBtnMole4;

            // Generate random numbers for rows and columns.
            // Set the row property and column property on the image button.
            // Make the image visible.
            // Check which grid is visible.
            // Using this to set the max row and max column properties.
            // Get the image button.

            if (GridGame5.IsVisible == true)
            {
                maxRow = maxCol = 5;
                currentImgBtn = ImgBtnMole5;
            }

            // Generate random r and c.
            r = _random.Next(0, maxRow);
            c = _random.Next(0, maxCol);
            currentImgBtn.SetValue(Grid.RowProperty, r);
            currentImgBtn.SetValue(Grid.ColumnProperty, c);
            currentImgBtn.IsVisible = true;
        }

        private void ResetGame()
        {
            LblScore.Text = "0";
            ResetTimers();
        }

        private void ResetTimers()
        {
            LblCountdown.Text = _CountdownStart.ToString();
            LblCountdown.TextColor = Color.Black; 
        }

        private async void EndScore()
        {
            if(Convert.ToInt32(LblScore.Text) >= 100 && Convert.ToInt32(LblScore.Text) < 350)
            {
                await DisplayAlert("Great Job! Your Score: ", LblScore.Text, "OK");
            }
            else if(Convert.ToInt32(LblScore.Text) >= 350)
            {
                await DisplayAlert("Incredible! Your Score: ", LblScore.Text, "OK");
            }
            else if(Convert.ToInt32(LblScore.Text) <= 0)
            {
                await DisplayAlert("Unfortunate :( Your Score: ", LblScore.Text, "OK");
            }
            else
            {
                await DisplayAlert("Game Over! Your Score: ", LblScore.Text, "OK");
            }    
        }

        private void ImgBtnMole_Clicked(object sender, EventArgs e)
        {
            int score;
            if(GridGame5.IsVisible == true)
            {
                if (Convert.ToInt32(LblCountdown.Text) <= 15)
                {
                    score = Convert.ToInt32(LblScore.Text) + 30;
                }
                else
                {
                    score = Convert.ToInt32(LblScore.Text) + 15;
                }
            }
            else
            {
                if (Convert.ToInt32(LblCountdown.Text) <= 15)
                {
                    score = Convert.ToInt32(LblScore.Text) + 20;
                }
                else
                {
                    score = Convert.ToInt32(LblScore.Text) + 10;
                }
            }
            

            LblScore.Text = score.ToString();

            ImageButton ib = (ImageButton)sender;
            ib.IsVisible = false;
        }

        private void BtnSwitch_Clicked(object sender, EventArgs e)
        {
            GridGame4.IsVisible = !GridGame4.IsVisible;
            GridGame5.IsVisible = !GridGame5.IsVisible;

            ResetGame();
        }

        private async void BtnMoveTest_Clicked(object sender, EventArgs e)
        {
            // Start the game.
            if (BtnMoveTest.Text == "Start Game")
            {
                ResetTimers();
                BtnMoveTest.Text = "Stop Game";
                _timer.Start();
            }
            else if (BtnMoveTest.Text == "Stop Game")
            {
                bool response = await DisplayAlert("Stop?", "Would you like to stop your game?", "Yes", "No");
                if (response == true)
                {
                    EndScore();
                    ResetGame();

                    BtnMoveTest.Text = "Start Game";
                    _timer.Stop();
                }
            }
            // Was used to test the functionality.
            // MoveTheMole();
        }

        private async void BtnReset_Clicked(object sender, EventArgs e)
        {
            bool response = await DisplayAlert("Restart?", "Would you like to restart your game?", "Yes", "No");
            if (response == true)
            {
                ResetGame();
            }
        }
    }
}
