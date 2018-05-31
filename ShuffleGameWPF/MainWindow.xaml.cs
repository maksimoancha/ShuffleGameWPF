using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace ShuffleGameWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private KeyValuePair<int, int> _emptyPosition = new KeyValuePair<int, int>(3,3); //key=row, value=column
        private bool _isNewGame = false;
        DispatcherTimer _timer = new DispatcherTimer();
        Stopwatch sw = new Stopwatch();
        string currentTime = "--:--:--";
        private string _currNickName;

        public MainWindow()
        {
            InitializeComponent();
            _timer.Tick += new EventHandler(_timer_Tick);
            _timer.Interval = new TimeSpan(0, 0, 0, 0, 1);
        }
        public MainWindow(string currNickName)
        {
            InitializeComponent();
            _timer.Tick += new EventHandler(_timer_Tick);
            _timer.Interval = new TimeSpan(0, 0, 0, 0, 1);
            _currNickName = currNickName;
        }

        private void _timer_Tick(object sender, EventArgs e)
        {
            if (sw.IsRunning)
            {
                TimeSpan ts = sw.Elapsed;
                currentTime = String.Format("{0:00}:{1:00}:{2:00}",
                    ts.Hours, ts.Minutes, ts.Seconds);
                LblTimeSpan.Content = currentTime;
            }
        }

        private void MainBtn_Click(object sender, RoutedEventArgs eve)
        {
            var btn = sender as Button;

            if ((int) btn.GetValue(Grid.RowProperty) == _emptyPosition.Key) //if button and empty field stay in 1 row
            {
                if ((int) btn.GetValue(Grid.ColumnProperty) + 1 == _emptyPosition.Value ||
                    (int) btn.GetValue(Grid.ColumnProperty) - 1 == _emptyPosition.Value)
                {
                    var tempPosition = new KeyValuePair<int, int>((int)btn.GetValue(Grid.RowProperty), (int)btn.GetValue(Grid.ColumnProperty));
                    btn.SetValue(Grid.RowProperty, _emptyPosition.Key);
                    btn.SetValue(Grid.ColumnProperty, _emptyPosition.Value);
                    _emptyPosition = tempPosition;
                }
            }
            if ((int) btn.GetValue(Grid.ColumnProperty) == _emptyPosition.Value) //if button and empty field stay in 1 column
            {
                if ((int) btn.GetValue(Grid.RowProperty) + 1 == _emptyPosition.Key ||
                    (int) btn.GetValue(Grid.RowProperty) - 1 == _emptyPosition.Key)
                {
                    var tempPosition = new KeyValuePair<int, int>((int)btn.GetValue(Grid.RowProperty), (int)btn.GetValue(Grid.ColumnProperty));
                    btn.SetValue(Grid.RowProperty, _emptyPosition.Key);
                    btn.SetValue(Grid.ColumnProperty, _emptyPosition.Value);
                    _emptyPosition = tempPosition;
                }
            }
            if(_isNewGame)
                if (IsFinish())
                {
                    if (sw.IsRunning)
                    {
                        sw.Stop();
                    }

                    var currTime = TimeSpan.Parse(currentTime);
                    var tableTime =
                        TimeSpan.Parse(Db_Controller.GetPlayers().First(x => x.NickName == _currNickName).Result== "--:--:--" ?
                            "11:59:59":
                            Db_Controller.GetPlayers().First(x => x.NickName == _currNickName).Result);


                    if (tableTime.ToString() == "--:--:--" ||  TimeSpan.Compare(currTime, tableTime) == -1)
                    {
                        Db_Controller.UpdateResult(_currNickName,currentTime);
                    }
                    MessageBox.Show($"Congratulations!\n You Win!\n Your best time:{currentTime}");
                    _isNewGame = false;
                }
        }

        private void RandomShuffle()
        {
            List<Button> listButtons = new List<Button>();
            listButtons.Add(btn1);
            listButtons.Add(btn2);
            listButtons.Add(btn3);
            listButtons.Add(btn4);
            listButtons.Add(btn5);
            listButtons.Add(btn6);
            listButtons.Add(btn7);
            listButtons.Add(btn8);
            listButtons.Add(btn9);
            listButtons.Add(btn10);
            listButtons.Add(btn11);
            listButtons.Add(btn12);
            listButtons.Add(btn13);
            listButtons.Add(btn14);
            listButtons.Add(btn15);
            Random rnd = new Random();
            for (int i = 0; i < 1000; i++)
            {
                MainBtn_Click((listButtons[rnd.Next(14)] as object), null);
            }
        }

        private void ExitMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();

        }

        private void NewGameMenuItem_Click(object sender, RoutedEventArgs e)
        {
            sw.Reset();
            LblTimeSpan.Content = "00:00:00";
            _isNewGame = false;
            RandomShuffle();
            _isNewGame = true;
            _timer.Start(); //timer start
            sw.Start();     //stop watch start
        }

        private bool IsFinish()
        {
            if ((int) btn1.GetValue(Grid.RowProperty) == 0 && (int) btn1.GetValue(Grid.ColumnProperty) == 0 &&
                (int) btn2.GetValue(Grid.RowProperty) == 0 && (int) btn2.GetValue(Grid.ColumnProperty) == 1 &&
                (int) btn3.GetValue(Grid.RowProperty) == 0 && (int) btn3.GetValue(Grid.ColumnProperty) == 2 &&
                (int) btn4.GetValue(Grid.RowProperty) == 0 && (int) btn4.GetValue(Grid.ColumnProperty) == 3 &&

                (int) btn5.GetValue(Grid.RowProperty) == 1 && (int) btn5.GetValue(Grid.ColumnProperty) == 0 &&
                (int) btn6.GetValue(Grid.RowProperty) == 1 && (int) btn6.GetValue(Grid.ColumnProperty) == 1 &&
                (int) btn7.GetValue(Grid.RowProperty) == 1 && (int) btn7.GetValue(Grid.ColumnProperty) == 2 &&
                (int) btn8.GetValue(Grid.RowProperty) == 1 && (int) btn8.GetValue(Grid.ColumnProperty) == 3 &&

                (int) btn9.GetValue(Grid.RowProperty) == 2 && (int) btn9.GetValue(Grid.ColumnProperty) == 0 &&
                (int) btn10.GetValue(Grid.RowProperty) == 2 && (int) btn10.GetValue(Grid.ColumnProperty) == 1 &&
                (int) btn11.GetValue(Grid.RowProperty) == 2 && (int) btn11.GetValue(Grid.ColumnProperty) == 2 &&
                (int) btn12.GetValue(Grid.RowProperty) == 2 && (int) btn12.GetValue(Grid.ColumnProperty) == 3 &&

                (int) btn13.GetValue(Grid.RowProperty) == 3 && (int) btn13.GetValue(Grid.ColumnProperty) == 0 &&
                (int) btn14.GetValue(Grid.RowProperty) == 3 && (int) btn14.GetValue(Grid.ColumnProperty) == 1 &&
                (int) btn15.GetValue(Grid.RowProperty) == 3 && (int) btn15.GetValue(Grid.ColumnProperty) == 2
               )
                return true;
            else
                return false;
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            var resTableWindow = new TableRecordsWindow(Db_Controller.GetPlayers());
            resTableWindow.ShowDialog();
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(
                $"Your result is: {Db_Controller.GetPlayers().First(x => x.NickName == _currNickName).Result}");
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void LogOutMenuItem_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            var logWind = new LoginWindow();
            logWind.Show();
        }

        private void InfoMenuItem_Click(object sender, RoutedEventArgs e)
        {
            InfoWindow info = new InfoWindow();
            info.ShowDialog();
        }
    }
}
